using System.Net;
using System.Text;

public class SimpleHttpServer : IDisposable
{
    private readonly HttpListener _listener = new();

    public SimpleHttpServer() => ListenAsync();

    private async void ListenAsync()
    {
        // Angi hvilken URI serveren skal lytte på
        _listener.Prefixes.Add("http://localhost:9001/");
       
        // Start lytting
        _listener.Start();
       
        // Vent på en forespørsel
        var context = await _listener.GetContextAsync();
       
        // Les informasjon om forespørselen
        Console.WriteLine(context.Request.HttpMethod);
        Console.WriteLine(context.Request.RawUrl);
        string responseMessage = $"You asked for the following resource: {context.Request.RawUrl}";
       
        // Konfigurer og send respons
        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(responseMessage);
        context.Response.StatusCode = (int)HttpStatusCode.OK;
       
        using var outputStream = context.Response.OutputStream;
        using var streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(responseMessage);
    }
   
    public void Dispose() => _listener.Close();
}