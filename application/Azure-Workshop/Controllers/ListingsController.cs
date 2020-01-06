using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisWorkshop.Models;
using RedisWorkshop.Services;

namespace RedisWorkshop.Controllers {
    public class ListingsController : Controller {

        private readonly AirBNBDatabaseContext _context;
        private readonly ListingsCachingService _listingCachingService;

        public ListingsController(AirBNBDatabaseContext context, ListingsCachingService listingsCachingService) {
            _context = context;
            _listingCachingService = listingsCachingService;
        }
        // GET: Listings
        public async Task<IActionResult> Index() {
            var stopwatch = Stopwatch.StartNew();
            if (_listingCachingService.CachedAvailable()) {
                var cachedItems = await _listingCachingService.GetCachedListings();
                stopwatch.Stop();
                Console.WriteLine("Totals time elapsed CACHE: " + stopwatch.Elapsed);

                ViewData["Amount"] = cachedItems.Count;
                ViewData["Source"] = "CACHE";
                ViewData["Time"] = stopwatch.ElapsedMilliseconds;

                return View(cachedItems.Take(10));
            }

            var listings = await _context.Listings.Select(x => new
                ListingsInfo(x.Id, x.ListingUrl, x.Name, x.Latitude, x.Longitude)).ToListAsync();

            stopwatch.Stop();
            Console.WriteLine("Totals time elapsed DB: " + stopwatch.Elapsed);

            _listingCachingService.SetCachedListings(listings);

            ViewData["Amount"] = listings.Count;
            ViewData["Source"] = "DATABASE";
            ViewData["Time"] = stopwatch.ElapsedMilliseconds;

            return View(listings.Take(10));
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int id) {
            var stopwatch = Stopwatch.StartNew();
            if (_listingCachingService.CachedAvailable()) {
                var result = await _listingCachingService.GetCachedListing(id);

                stopwatch.Stop();
                Console.WriteLine("Details time elapsed CACHE: " + stopwatch.Elapsed);

                ViewData["Source"] = "CACHE";
                ViewData["Time"] = stopwatch.ElapsedMilliseconds;

                if (result != null) return View(result);
            }

            var listing = await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);

            stopwatch.Stop();
            Console.WriteLine("Details time elapsed DB: " + stopwatch.Elapsed);

            if (listing == null) return NotFound();

            ViewData["Source"] = "DATABASE";
            ViewData["Time"] = stopwatch.ElapsedMilliseconds;

            return View(new ListingsInfo(listing.Id, listing.ListingUrl, listing.Name, listing.Latitude,
                listing.Longitude));
        }
    }
}