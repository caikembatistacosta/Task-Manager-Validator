using System.Text.Json;
namespace Entities
{
    public class ErrorsDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string ToJsonSerealize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
