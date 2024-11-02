using MongoDB.Bson;

namespace PUT_Backend
{
    public class WeatherForecast
    {
        public ObjectId _id { get; set; }
        
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
