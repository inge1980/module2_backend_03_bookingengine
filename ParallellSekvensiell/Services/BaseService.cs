namespace ParallellSekvensiell.Services;

public class BaseService(HttpClient client)
{
    // Når vi tar inn et object i primærkonstructoren, 
    // trenger vi ikke lage egne felt for den. 
    // Hvis ikke vi ABSOLUTT MÅ styre access osv. 
    // private HttpClient _client = client;

    public async Task GetFromEndpointAsync(string endpoint)
    {
        try
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(endpoint);  
            response.EnsureSuccessStatusCode(); // Alle 2xx er suksess, ellers kaster den en exception.
            var content = await response.Content.ReadAsStringAsync(); // Innholdet i response som en string
            Console.WriteLine($"Response from {endpoint}: {content}");
        }
        catch (InvalidOperationException invEx)
        {
            // Denne kan oppstå hvis HttpClient er i en ugyldig tilstand, for eksempel hvis den allerede er blitt avhendet.
            Console.WriteLine($"Invalid operation when trying to fetch data from {endpoint}: {invEx.Message}");
            throw;
        }
        catch (HttpRequestException httpEx)
        {
            // Denne kan oppstå hvis det er et problem med selve HTTP-forespørselen, for eksempel nettverksfeil eller hvis serveren returnerer en feilstatuskode.
            Console.WriteLine($"Error fetching data from {endpoint}: {httpEx.Message}");
            throw;
        }
        catch (OperationCanceledException opEx)
        {
            // Denne kan oppstå hvis forespørselen blir avbrutt, for eksempel på grunn av en timeout.
            Console.WriteLine($"Request canceled when fetching data from {endpoint}: {opEx.Message}");
            throw;
        }
        catch (UriFormatException uriEx)
        {
            // Denne kan oppstå hvis endpoint-URL-en er i et ugyldig format.
            Console.WriteLine($"Invalid URI format for endpoint {endpoint}: {uriEx.Message}");
            throw;
        }
        
    }
}