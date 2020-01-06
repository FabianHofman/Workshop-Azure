using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWorkshop.Services {
    public class ListingsCachingService {
        private readonly IDatabase _cache; //Importeer hier "StackExchange.Redis.IDatabase" wanneer dat gevraagd wordt
        private const string LISTINGS_KEY_NAME = "listings";
        private const int CACHE_EXPIRE_MIN = 5;

        public ListingsCachingService(IConnectionMultiplexer connectionMultiplexer) {
            _cache = connectionMultiplexer.GetDatabase();
        }

        public bool CachedAvailable() {
            return _cache.SetLength(LISTINGS_KEY_NAME) > 0;
        }

        public void SetCachedListings(List<ListingsInfo> listings) {
            var serialisedItems = listings.Select(value => (RedisValue)JsonConvert.SerializeObject(value)).ToArray();
            _cache.SetAddAsync(LISTINGS_KEY_NAME, serialisedItems);
            _cache.KeyExpire(LISTINGS_KEY_NAME, DateTime.Now.AddMinutes(CACHE_EXPIRE_MIN));
        }

        public async Task<List<ListingsInfo>> GetCachedListings() {
            var cached = await _cache.SetMembersAsync(LISTINGS_KEY_NAME);
            var parsed = cached.Select(value => {
                var deserialized = JsonConvert.DeserializeObject(value, typeof(ListingsInfo));
                return (ListingsInfo)deserialized;
            }).ToList();
            return parsed;
        }

        public async Task<ListingsInfo> GetCachedListing(int id) {
            var cached = await _cache.SetMembersAsync(LISTINGS_KEY_NAME);
            var results = cached
                .Select(value => (ListingsInfo)JsonConvert.DeserializeObject(value, typeof(ListingsInfo)))
                .Where(x => x.Id == id).ToList();
            return results.Count < 1 ? null : results.First();
        }

    }
}