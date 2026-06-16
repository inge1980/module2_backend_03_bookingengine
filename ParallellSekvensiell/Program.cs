using ParallellSekvensiell.Services;

// lag en client som brukes overalt i applikasjonen
var client = new HttpClient();

// Lag instanser av både parallell og sekvensiell service
var parallelService = new ParallellHttpService(client);
var sequentialService = new SequentialHttpService(client);

// Definer en liste med endpoints
List<string> endpoints = [
    "https://icanhazdadjoke.com/",
    "https://official-joke-api.appspot.com/random_joke",
    "https://api.chucknorris.io/jokes/random"
];

await sequentialService.SequentialGetAsync(endpoints);

await parallelService.ParallellGetAsync(endpoints);

