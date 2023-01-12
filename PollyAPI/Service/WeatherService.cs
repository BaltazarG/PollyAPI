using Polly;
using PollyAPI.Models;

namespace PollyAPI.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Weather>> GetWeather()
        {
            try
            {
                IEnumerable<Weather> weathers = new List<Weather>();
                var retries = 1;

                var policyResult = await Policy.Handle<Exception>().WaitAndRetryAsync(
                        retryCount: 4,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, timeSpan, context) =>
                        {
                            retries++;
                        }).ExecuteAndCaptureAsync(async () =>
                        {
                            weathers = await _httpClient.GetFromJsonAsync<IEnumerable<Weather>>($"https://localhost:7146/weatherforecast");
                        });

                Console.WriteLine($"Result: {policyResult.Outcome.ToString()}");

                return weathers;

            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
