// localhost:6969/
using System.Net;
using System.Text;

namespace SimpleHttpServer.Models;


public class WebServer
{
    private HttpListener _listener;
    private string _folder = "./wwwroot";

    public WebServer(string uriPrefix, string? rootFolder = null)
    {
        _listener = new();
        _listener.Prefixes.Add(uriPrefix);

        if (!string.IsNullOrWhiteSpace(rootFolder)) _folder = rootFolder;
    }

    public async Task StartAsync()
    {
        _listener.Start();

        while (true)
        {
            try
            {
                var context = await _listener.GetContextAsync();
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        await ProcessGetRequestAsync(context);
                        break;
                    case "POST":
                        await ProcessPostRequestAsync(context);
                        break;
                    default:
                        break;
                }
            }
            catch (HttpListenerException){break;}
            catch (ArgumentException){break;}
        }
    }

    public void Stop() => _listener.Stop();

    private async Task ProcessGetRequestAsync(HttpListenerContext context)
    {
        try
        {
            var fileName = Path.GetFileName(context.Request.RawUrl);
            var filePath = Path.Combine(_folder, fileName!);

            byte[] responseMessage;

            if (!File.Exists(filePath))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                responseMessage = Encoding.UTF8.GetBytes($"Sorry, {fileName} is not available on this server...");
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                responseMessage = await File.ReadAllBytesAsync(filePath);
            }

            context.Response.ContentLength64 = responseMessage.Length;
            context.Response.ContentType = GetMIMEStringForFile(filePath);
            context.Response.ContentEncoding = Encoding.UTF8;

            using var output = context.Response.OutputStream;
            await output.WriteAsync(responseMessage);
        }
        catch (ArgumentException)
        {
            throw;
        }
    }

    private async Task ProcessPostRequestAsync(HttpListenerContext context)
    {
        try
        {
            var request = context.Request;
            var response = context.Response;

            byte[] responseMessage;

            if (!request.HasEntityBody)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                responseMessage = Encoding.UTF8.GetBytes("Cannot process request, missing request body.");
            }
            else
            {
                var fileName = request.Headers["x-filename"] ?? $"data-{DateTime.UtcNow:t}.txt";

                using var memStream = new MemoryStream();
                
                await request.InputStream.CopyToAsync(memStream);

                var filePath = Path.Combine(_folder, fileName);

                await File.WriteAllBytesAsync(filePath, memStream.ToArray());

                response.StatusCode = (int)HttpStatusCode.Created;
                responseMessage = Encoding.UTF8.GetBytes($"'message':'successfully saved file.','path':'{fileName}'");
                response.ContentType = "application/json";
            }
            response.ContentLength64 = responseMessage.Length;
            using var output = response.OutputStream;
            await output.WriteAsync(responseMessage);
        }
        catch (ArgumentException)
        {
            throw;
        }
    }

    private string GetMIMEStringForFile(string fileName) => fileName.Split(".").Last() switch
    {
        "html" => "text/html",
        "txt" => "text/plain",
        _ => "text/plain"
    };
}