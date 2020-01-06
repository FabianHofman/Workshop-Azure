# Prerequisites
Om deze demo te volgen is het belangrijk dat je eerst een Azure account gemaakt hebt en de studenten license gekoppeld hebt aan dit account.
Als je dit nog niet gedaan hebt, maak dan even een account aan op https://azure.microsoft.com/en-us/.

Om 100$ gratis credit te krijgen, koppel je account hier: https://azure.microsoft.com/en-us/free/students/

Zodra dit gedaan is, kan je verder met de rest van de demo!

# Database
Het aanmaken van de database duurt het langste. Gemiddeld duurt 15 tot 30 minuten. 

Ga naar https://portal.azure.com/ (en log hier in). Je komt nu uit op een overzicht pagina met bovenin 'Azure-services'. Klik vervolgens op SQL-databases. 

![Screenshot azure sql server location](/img/azure-db-service.png)

Je komt hierbij op een nieuwe pagina uit met een overzicht van alle SQL-databases (mits je die al hebt gemaakt, anders staat er niks).
Als je nog geen database gemaakt hebt. Kan je bovenin op 'Toevoegen' of 'Add' klikken. Vervolgens kom je uit op een stappenplan om een SQL-database aan te maken.

**Let goed op, vanaf hier gaat het geld kosten!**

Zorg ervoor dat bij 'Abbonement staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

Hierna maak je een 'Resourcegroep' aan. Dit kan je noemen zoals je zelf wilt. Een 'Resourcegroep' is een collectie van services die relateerd aan elkaar zijn. 

Vervolgens gaan we de 'Databasenaam' invullen. Hier mag je ook weer invullen wat je wilt.

Vervolgens gaan we een 'Server' aanmaken. Druk onder het select veld op 'Nieuwe aanmaken'. Er zal rechts in het scherm een formulier openen. Volg hierbij de volgende instellingen
- Servernaam: deze moet uniek zijn, kies dus een naam waarvan jij zeker bent dat die nog niet bestaat.
- Aanmeldgegevens voor de serverbeheerder: Geef hier een naam naar keuze.
- Wachtwoord: Sterk wachtwoord naar keuze
- Wachtwoord bevestigen: Zelfde als hierboven is ingetypt.
- Locatie: (Europa) West-Europa 

![Screenshot azure sql server login](/img/azure-db-servername.png)

Je kan 'SQL elastic pool' op nee laten. 

Vervolgens gaan we de 'Berekening en opslag instellen'. Klik hier op 'Database configureren'. Je komt hierbij weer in een nieuw scherm uit. Hierin komt het echte kostenplaatje naar voren. Aan de rechterkant zie je de inschatten staan wat de kosten zijn. 

Voor deze server kiezen we 'Algemeen Gebruik'. Om niet teveel te betalen, gebruiken we een 'Serverloos' server. In plaats per maand, betaal je hier per seconde dat je de database gebruikt. 

Om de database een beetje snel te houden, kan je de minimum en maxium aantal vCores aanpassen. Je mag hier zelf aanpassen wat je wil, maar let natuurlijk wel op het kostenplaatje. De maximale database groote kan je aanpassen bij data max size. Voor nu laten we deze op '2gb' staan. Voor je AirBNB database kan je deze naar 4gb zetten. 

Wanneer alles aangepast is, kan je onderin op 'Toepassen' klikken. Vervolgens klikken we op 'Beoordelen en maken'. Controleer hier nog 1 keer de gegevens en selecteer vervolgens 'Maken'. De database wordt nu aangemaakt en toegevoegd aan een server.

Natuurlijk willen we nog wat data hebben in de database. Om dit te doen kunnen we de 'Microsoft SQL Server Management Studio' (SSMS) openen en verbinden met de server. Hiervoor heb je de volgende gegevens nodig die eerder ingevuld zijn:
- Servernaam
- Gebruikersnaam
- Wachtwoord

Als SSMS vraagt om je Azure account, dan kan je gewoon inloggen. Zodra dit succesvol gedaan is, zie je links de server staan. Gewoon queries op uitvoeren zoals je gewend bent en de database erop zetten.

**Belangrijk**: ga naar https://portal.azure.com/ en klik onder 'Navigeren' op 'Resourcegroepen'. Klik vervolgens op de resourcegroep die je aangemaakt hebt hierboven. Klik hier op de SQL-server. Ga in het linker menu naar 'Firewalls en virtuele netwerken'. Zet 'Toestaan dat Azure-services en -resources toegang tot deze server te krijgen' aan en druk op opslaan. 

# Redis Cache
Ga naar https://portal.azure.com/ (en log hier in). Je komt nu uit op een overzicht pagina met bovenin 'Azure-services'. Klik vervolgens op 'Een resource maken'. Zoek hier naar 'Redis-cache' en klik vervolgens op het eerste resultaat. Klik op 'Maken'. 

**Let goed op, vanaf hier gaat het geld kosten!**
Zorg ervoor dat bij 'Abonnement' staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

Voer een 'DNS-naam' in naar keuze. Deze naam moet uniek zijn, kies dus een naam waarvan jij zeker bent dat die nog niet bestaat.
Bij 'Resourcegroep' kan je gebruik maken van de eerder aangemaakte groep.
Onze 'Locatie' is West-Europa, vul dit hier in.
Bij 'Prijscategorie' willen we iets meer informatie over de prijzen. Klik hier op 'Volledige prijsgegevens weergeven'.
Zoals te zien is, zijn deze services best prijzig. Als je helemaal naar onder scrollt zie je hier alle basic services staan. Deze kosten al een heel stuk minder.
Vanaf hier moet je de keuze maken of je de 250MB of de 1GB wil (Dit ligt er natuurlijk aan wat je allemaal wilt opslaan).
Selecteer jou keuze in het dropdown menu. Klik vervolgens op 'Maken'.

# IdentityServer
Open SSMS en maak een connectie naar je lokale SQL-server. Zoek naar de database die je gebruikt voor IdentityServer. Klik met je rechtermuisknop hierop en hover over 'Tasks'. Klik vervolgens op 'Deploy Database to Microsoft Azure SQL Database'. Klik op 'Next'. Klik vervolgens op 'Connect...' aan de rechter kant van het scherm. Connect hier met je Azure SQL server (We houden voor de database settings even het normale plan aan dit wijzigen we later zodat het minder gaat kosten). Klik vervolgens op 'Next >'. Controleer de gegevens en klik op 'Finish'. 

(Dit kan enige momenten duren, dus kijk gerust even naar dit [filmpje](https://www.youtube.com/watch?v=dQw4w9WgXcQ))

Ga naar https://portal.azure.com/ (en log hier in). Je komt nu uit op een overzicht pagina met bovenin 'Azure-services'. Klik vervolgens op SQL-databases. Je ziet hier de IdentityServer database staan, klik hier op.
Bovenin aan de rechterkant staat 'Prijscategorie: Basis'. Klik hier op Basis om het plan aan te passen. Klik bovenin op 'Op vCore gebaseerde aanschafopties'. Kies hier voor 'Algemeen gebruik'. Hier komt weer het hoge kostenplaatje te staan als bij de eerste database. Klik vervolgens op 'Serverloos' (de rest van de instellingen mag je ook aanpassen, maar is in principe niet nodig). Klik hierna op 'Toepassen'.

Open vervolgens 'Visual Studio 2019 Preview' met daarin de solution. We moeten hier eerst een aantal dingen aanpassen voordat we kunnen publishen. In de appsettings moeten we de connection string aanpassen naar de Azure SQL database. Deze connection string is te vinden op https://portal.azure.com/. Navigeer hier naar SQL-databases en klik op de aangemaakte IdentityServer database. Rechtsbovenin staat het veld 'Verbindingsreeksen'. Klik hier op 'Databaseverbindingsreeksen tonen'. Hier komt nu de connection string te staan. Kopieër deze en vervang deze in de appsettings. Vergeet niet om je wachtwoord in te vullen van de SQL-server. 

Klik vervolgens met je rechtermuisknop op de IdentityServer project. Selecteer 'Publish...'. Er wordt nu een nieuw scherm geopend. Klik in dit scherm op 'Start'. In dit nieuwe scherm selecteer je 'App Service' en zorg je er voor dat 'Create New' geselecteerd is. 

**Let goed op, vanaf hier gaat het geld kosten!**
Zorg ervoor dat bij 'Abonnement' staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

De naam wordt automatisch voor je ingevuld. Je kan deze aanhouden of veranderen (de keuze is aan jou). Vervolgens zorg je er voor dat je de juiste 'Resourcegroep' selecteert. Bij 'Hosting Plan' gaan we een hele andere maken. Klik hier op 'New...'. Zorg ervoor dat de instellingen er al volgt uit zien:
- Hosting Plan: ingevuld laten zoals die is of zelf een naam geven, dit maakt niet uit
- Location: West Europe
- Size: Free

Druk vervolgens op 'OK'. Zoals je ziet is de Hosting Plan aangepast naar (West Europe, F1).
De 'Application Insights' kan gewoon op None blijven staan. Klik vervolgens op 'Create'.

Klik vervolgens op 'Publish'.

(Sla de URL even op of zorg ervoor dat je die in ieder geval terug kan vinden, die hebben we zo nodig namelijk)

# Web API

Open 'Visual Studio 2019 Preview' met daarin de solution. We moeten hier eerst een aantal dingen aanpassen voordat we kunnen publishen. In de appsettings moeten we de connection string aanpassen naar de Azure SQL database. Deze connection string is te vinden op https://portal.azure.com/. Navigeer hier naar SQL-databases en klik op de aangemaakte InsideAirBNB database. Rechtsbovenin staat het veld 'Verbindingsreeksen'. Klik hier op 'Databaseverbindingsreeksen tonen'. Hier komt nu de connection string te staan. Kopieër deze en vervang deze in de appsettings. Vergeet niet om je wachtwoord in te vullen van de SQL-server. 

Hierna moeten we nog wat aanpassingen maken in `Startup.cs`. onder `AddJwtBearer` moeten we de Authority aanpassen. Hier moet de link komen die we hierboven gekregen hebben. 

Als laatste moeten we Redis nog koppelen. Ga naar https://portal.azure.com/ en klik onder 'Navigeren' op 'Resourcegroepen'. Klik vervolgens op de resourcegroep die je aangemaakt hebt hierboven. Klik hier op de Azure Cache for Redis. Rechtsbovenin staat het veld 'Sleutels', klik hier op 'Toegangstoetsen weergeven...'. Rechts word een menu geopend met daarin de verbindingsreeks. Kopieër hier de 'Primaire verbindingsreeks (StackExchange.Redis)'. Vervolgens gaan we naar `Startup.cs` en zoeken we daar de verbinding met Redis. Vervang de string hier met de string die we net gekopieerd hebben.

Klik vervolgens met je rechtermuisknop op de IdentityServer project. Selecteer 'Publish...'. Er wordt nu een nieuw scherm geopend. Klik in dit scherm op 'Start'. In dit nieuwe scherm selecteer je 'App Service' en zorg je er voor dat 'Create New' geselecteerd is. 

**Let goed op, vanaf hier gaat het geld kosten!**
Zorg ervoor dat bij 'Abonnement' staat **Azure for Students**. Als die optie niet aanwezig is, moet je nog even goed kijken of je jou studenten account gekoppeld hebt aan Azure.

De naam wordt automatisch voor je ingevuld. Je kan deze aanhouden of veranderen (de keuze is aan jou). Vervolgens zorg je er voor dat je de juiste 'Resourcegroep' selecteert. Bij 'Hosting Plan' willen we dezelfde gebruiken als bij de IdentityServer. Selecteer deze optie. Druk vervolgens op 'OK'. Zoals je ziet is de Hosting Plan aangepast naar (West Europe, F1).
De 'Application Insights' kan gewoon op None blijven staan. Klik vervolgens op 'Create'.

Klik vervolgens op 'Publish'.