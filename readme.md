# Prerequisites
Om deze demo te volgen is het belangrijk dat je eerst een Azure account gemaakt hebt en de studenten license gekoppeld hebt aan dit account.
Als je dit nog niet gedaan hebt, maak dan even een account aan op https://azure.microsoft.com/en-us/.

Om 100$ gratis credit te krijgen, koppel je account hier: https://azure.microsoft.com/en-us/free/students/

Daarnaast draait de applicatie op dotnet 3.1 (Niet de preview). Deze kan je downloaden van de microsoft site. 

# Database
Ga naar https://portal.azure.com/ (en log hier in). Je komt nu uit op een overzicht pagina met bovenin `Azure-services`. Klik vervolgens op SQL-servers. 

![Screenshot azure sql server location](./img/azure-db-service.png)

Je komt hierbij op een nieuwe pagina uit met een overzicht van alle SQL-servers (mits je die al hebt gemaakt, anders staat er niks).
Bovenin kan je op `Add` klikken. Je komt dan op een pagina om een sql server aan te makne.

**Let goed op, vanaf hier gaat het geld kosten!**

Zorg ervoor dat bij subscription staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

Als eerste maak je een `resource group` aan. Dit kan je noemen zoals je zelf wilt. Een `resourcegroup` is een collectie van services die relateerd aan elkaar zijn. (Je SQL server, Redis cache en web api)

Vervolgens gaan we de `server details` invullen. 
- **Server name**: De server name moet uniek zijn omdat deze gebruikt word om te connecten naar je sql server.
- **Location**: Zet deze naar: `(Europa) West-Europa`
- **Server admin login**: De username van de server admin. Dit is de root. 
- **Password**: Een wachtwoord naar keuze. Verlies deze niet!

![Screenshot azure sql server login](./img/azure-db-overview.png)
Je instellingen zouden er nu zo uit moeten zien. 

Je komt terug op het `basics` scherm. 

Je klikt dan op het tabje `networking`. Hier zet je onder `firewall rules` de optie `Allow Azure services and resources to access this server` naar `yes`. Hierdoor kan je connecten naar je sql server. 

Vervolgens klikken we op het tabje `additional settings`. Hier zet je `Enable advanced data security` naar `not now`. Dit voorkomt extra kosten. 

Wanneer je alles ingestelt hebt, ga je naar het tabje "review + create". Controleer hier nog 1 keer de gegevens en klik je vervolgens `create`. De sql server word aangemaakt. Hier gaan we later databases aan toevoegen.  

![Screenshot azure sql server review](./img/azure-db-review.png)

Natuurlijk willen we nog wat data hebben in de database. Om dit te doen kunnen we de `Microsoft SQL Server Management Studio` (SSMS) openen en verbinden met de server. Hiervoor heb je de volgende gegevens nodig die eerder ingevuld zijn:
- **Server name**
- **Server admin login**
- **Password**

![Screenshot azure sql server review](./img/azure-db-ssms-login.png)

Als je de servernaam vergeten bent, kan je bovenin in de zoekbalk van azure naar sql servers zoeken. Hier open je de aangemaakte sql server. Rechtsbovenin zie je de server admin en server name van je sql server. 

# Redis Cache
Ga naar https://portal.azure.com/ (en log hier in). Je komt nu uit op een overzicht pagina met bovenin `Azure-services`. Klik vervolgens op `Create a resrouce`. Zoek hier naar `Redis-cache` en klik vervolgens op het eerste resultaat. Klik op dan op `create`. 

![Screenshot azure sql server review](./img/azure-redis-search.png)

**Let goed op, dit kost geld!**
Zorg ervoor dat bij `subscription` staat **Azure for Students**. 

- Voer een `DNS name` in naar keuze. Deze naam moet net zoals je SQL server uniek zijn, kies dus een naam waarvan jij zeker bent dat die nog niet bestaat.
- Bij `resource group` selecteer je de aangemaakte groep van de sql server/database.
- Bij `location` selecteer je West-Europa.
- Bij `pricing tier` willen we iets meer informatie over de prijzen. Klik hier op `view full pricing details`. 

Een redis-cache is best prijzig. De meest goedkope optie is `c0 Basic 250mb cache`. Deze optie kost ongeveer 14 euro per maand. Je klikt hierna op `create`. 

# Importeren database
We beginnen met het downloaden van de deze github repo. Hierin vind je de applicatie en de database. In `SSMS` importeer je in je azure sql server de database. Dit kan je doen doormiddel een rechter muisklik te doen op databses en dan `Import Data-tier application` te selecteren. Tijdens het importeren vraagt SSMS wat voor soort database je wilt aanmaken. Je selecteert dan de standaard instellingen. 

**We veranderen deze instellingen later! Doe je dit niet, ben je binnen een week door je tegoed heen. Als je deze nu aanpast, duurt het importeren ruim een halfuur**

Nu word er een database gedeployd online met alle gegevens van de insideAirBNB database. Dit duurt ongeveer 5 minuten dus je kan even wachten. 

Nu de sql database gedeployd is, gaan we de subscription aanpassen. Ga naar https://portal.azure.com/. Klik vervolgens op `SQL-databases` onder Azure services. Je ziet hier de AirBNB database staan, klik hier op. Rechtsbovenin staat `Pricing tier`. Klik hierop. 

![Screenshot azure sql server review](./img/azure-db-tier.png)

Je komt op een nieuw scherm terecht. Hier kan je de samenvoeging van de server veranderen. Bovenaan staan 3 paketten: `Basic`, `Standard` en `Premium`. Rechts van premium staat `vCore-based purschasing options`. Klik hierop. Er komen nieuwe opties op het scherm. `General Purpose` is standaard geselecteert. Om niet teveel te betalen, gebruiken we een `serverless` server. In plaats per maand, betaal je hier per seconde dat je de database gebruikt. 

Om de database een beetje snel te houden, kan je het minimum en maxium aantal vCores aanpassen. Je kan hier zelf aanpassen hoe snel je de database wilt hebben, maar let natuurlijk wel op kosten. Minimaal 1 vCore en maximaal 2 vcores is voldoende voor airBNB aangezien we gebruik maken van caching en minimaal aantal gebruikers hebben. De maximale database groote kan je aanpassen bij data max size. Voor nu zetten we deze op `4gb`.

Wanneer alles aangepast is, kan je onderin op `apply` klikken. De database subscription wordt omgezet. Dit duurt een kleine minuut. Als je dan de pagina van je AirBNB database refresht, zie je dat de pricing tier aangepast is. 

![Screenshot azure sql server login](./img/azure-db-system.png)
De instellingen van je subscription. 

# Deploy API
Open vervolgens `Visual Studio 2019` met daarin de solution die je gedownload hebt van de github repo. We moeten hier eerst een aantal dingen aanpassen voordat we kunnen publishen. In de appsettings moeten we de connection string aanpassen naar de Azure SQL database. Deze connection string is te vinden op https://portal.azure.com/. Ga dan naar SQL-databases en klik op de aangemaakte AirBNB database. Rechtsbovenin staat het veld `Connection strings`. Klik hier op `Show database connection strings`. Hier kan je verschillende connection strings kopieren. Kopieër de ADO.NET connection string en vul deze in in de `appsettings.json` van de applicatie. Vergeet niet om het gedeelte `{your_password}` in de string te veranderen naar je wachtwoord van de SQL-server. 

![Screenshot azure sql server login](./img/azure-db-connection-string.png)

Als laatste moeten we Redis nog koppelen. Ga naar https://portal.azure.com/ en zoek naar `Resource groups`. Klik vervolgens op de resource group die je aangemaakt hebt hierboven. Klik hier op de `Azure Cache for Redis` service. Rechtsbovenin staat het veld `keys`. Klik hier op `show access keys...`. Rechts word een menu geopend met daarin de verbindingsreeks. Kopieër de connection string onder `Primary connection string (StackExchange.Redis)`. Vervolgens vervang de string die in `appsettings.json` staat met deze string.


Klik vervolgens met je rechtermuisknop op de IdentityServer project. Selecteer `Publish...`. Er wordt nu een nieuw scherm geopend. Klik in dit scherm op `Start`. In dit nieuwe scherm selecteer je `App Service` en zorg je er voor dat `Create New` geselecteerd is. 


**Let goed op, vanaf hier gaat het geld kosten!**
Zorg ervoor dat bij `Abonnement` staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

De naam wordt automatisch voor je ingevuld. Je kan deze aanhouden of veranderen (de keuze is aan jou). Vervolgens zorg je er voor dat je de juiste `Resourcegroep` selecteert. Bij `Hosting Plan` gaan we een hele andere maken. Klik hier op `New...`. Zorg ervoor dat de instellingen er al volgt uit zien:
- Hosting Plan: ingevuld laten zoals die is of zelf een naam geven, dit maakt niet uit
- Location: West Europe
- Size: Free

Druk vervolgens op `OK`. Zoals je ziet is de Hosting Plan aangepast naar (West Europe, F1).
De `Application Insights` kan gewoon op None blijven staan. Klik vervolgens op `Create`.

Klik vervolgens op `Publish`.

(Sla de URL even op of zorg ervoor dat je die in ieder geval terug kan vinden, die hebben we zo nodig namelijk)
