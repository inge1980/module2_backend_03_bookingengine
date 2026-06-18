// bash terminal
// dotnet run
// http://localhost:6969/index.html
// http://localhost:6969/b.txt
using SimpleHttpServer.Models;

Console.WriteLine("Hello, World!");


var server = new WebServer("http://localhost:6969/");

try
{
   server.StartAsync();
   Console.WriteLine("Press any key to stop the server...");
   Console.ReadLine(); 
}
finally
{
    server.Stop();
}