using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCurrencyDesktopApp_my_wpf_proj
{
    public class WeatherForecast
    {
        [JsonProperty("Headline")]
        public Headline Headline { get; set; }

        [JsonProperty("DailyForecasts")]
        public List<DailyForecast> DailyForecasts { get; set; }
    }

    public class Headline
    {
        [JsonProperty("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("EffectiveEpochDate")]
        public long EffectiveEpochDate { get; set; }

        [JsonProperty("Severity")]
        public int Severity { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("EndEpochDate")]
        public long EndEpochDate { get; set; }

        [JsonProperty("MobileLink")]
        public string MobileLink { get; set; }

        [JsonProperty("Link")]
        public string Link { get; set; }
    }

    public class TemperatureRange
    {
        [JsonProperty("Value")]
        public double Value { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("UnitType")]
        public int UnitType { get; set; }
    }

    public class Temperature
    {
        [JsonProperty("Minimum")]
        public TemperatureRange Minimum { get; set; }

        [JsonProperty("Maximum")]
        public TemperatureRange Maximum { get; set; }
    }

    public class DayNight
    {
        [JsonProperty("Icon")]
        public int Icon { get; set; }

        [JsonProperty("IconPhrase")]
        public string IconPhrase { get; set; }

        [JsonProperty("HasPrecipitation")]
        public bool HasPrecipitation { get; set; }

        [JsonProperty("PrecipitationType")]
        public string PrecipitationType { get; set; }

        [JsonProperty("PrecipitationIntensity")]
        public string PrecipitationIntensity { get; set; }
    }

    public class DailyForecast
    {
        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("EpochDate")]
        public long EpochDate { get; set; }

        [JsonProperty("Temperature")]
        public Temperature Temperature { get; set; }

        [JsonProperty("Day")]
        public DayNight Day { get; set; }

        [JsonProperty("Night")]
        public DayNight Night { get; set; }

        [JsonProperty("Sources")]
        public List<string> Sources { get; set; }

        [JsonProperty("MobileLink")]
        public string MobileLink { get; set; }

        [JsonProperty("Link")]
        public string Link { get; set; }
    }

}
