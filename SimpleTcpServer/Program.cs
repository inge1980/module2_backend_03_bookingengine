// Use:
// curl "http://localhost:5050"
using System.Net.Sockets;
using System.Text;

var listener = new TcpListener(System.Net.IPAddress.Loopback, 5050);

listener.Start();

using var client = await listener.AcceptTcpClientAsync();
using var stream = client.GetStream();

var buffer = new byte[8192];
var readBytes = await stream.ReadAsync(buffer, 0, buffer.Length);
var rawRequest = Encoding.UTF8.GetString(buffer, 0, readBytes);

string[] responses = ["Kilroy was here.", "Stop snooping!"];

var rand = Random.Shared.Next() % responses.Length;

var responseMessage = responses[rand];

var response = $"""
HTTP/1.1 200 OK
Content-Type: text/plain
Content-Length: {Encoding.UTF8.GetByteCount(responseMessage)}

{responseMessage}
""";

var responseAsBytes = Encoding.UTF8.GetBytes(response);

await stream.WriteAsync(responseAsBytes);
Console.WriteLine($"Størrelse på request: {readBytes}");
Console.WriteLine(rawRequest);
