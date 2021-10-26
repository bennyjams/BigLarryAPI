using System.Text.Json.Serialization;

namespace calhfaapi.Models
{
 public class Stats
 {
  [JsonPropertyName("TotalConfirmed")]
  public double Confirmed { get; set; }

  [JsonPropertyName("TotalDeaths")]
  public long Deaths { get; set; }


  [JsonPropertyName("TotalRecovered")]
  public int Recovered { get; set; }
 }

}