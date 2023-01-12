namespace PollyAPI.Models
{
    public class Weather
    {
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; } = string.Empty;
        public DateTime date { get; set; }

    }
}
