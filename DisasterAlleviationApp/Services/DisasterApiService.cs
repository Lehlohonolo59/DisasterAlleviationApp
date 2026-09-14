using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Services
{
    // Service responsible for communicating with external public disaster endpoints
    public class DisasterApiService
    {
        private readonly HttpClient _httpClient;

        // Injecting HttpClient using dependency injection principles
        public DisasterApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method that fetches active global natural disaster events asynchronously
        public async Task<List<DisasterEvent>> GetActiveDisastersAsync()
        {
            // Public NASA EONET endpoint tracking active global events (wildfires, storms, floods, etc.)
            string apiUrl = "https://eonet.gsfc.nasa.gov/api/v3/events?status=open";

            try
            {
                // Send an asynchronous GET request to the public API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the JSON response string from the stream
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Configure options to handle case-insensitive property matching during deserialization
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Deserialize the JSON string into our strongly typed C# model hierarchy
                    var result = JsonSerializer.Deserialize<DisasterApiResponse>(jsonResponse, options);

                    // Return the list of events, or an empty list if null
                    return result?.Events ?? new List<DisasterEvent>();
                }
            }
            catch (Exception ex)
            {
                // In a production app, you would log this exception (e.g., using ILogger)
                Console.WriteLine($"Error fetching disaster data: {ex.Message}");
            }

            return new List<DisasterEvent>();
        }
    }
}