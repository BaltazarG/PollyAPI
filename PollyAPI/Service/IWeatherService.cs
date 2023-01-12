using PollyAPI.Models;

namespace PollyAPI.Service
{
    public interface IWeatherService
    {
        public Task<IEnumerable<Weather>> GetWeather();
    }
}
