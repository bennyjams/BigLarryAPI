using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using calhfaapi.Models;


using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace calhfaapi.Services
{

 public class Covid19Service
 {

  private readonly HttpClient client = new HttpClient();

  public async Task<Stats> GetWorldStats()
  {
   var worldStatTask = client.GetStreamAsync("https://api.covid19api.com/world/total");

   var worldStat = await JsonSerializer.DeserializeAsync<Stats>(await worldStatTask);

   return worldStat;


  }

  public int GetFromSQL(IConfiguration conf)
  {
    string connectionString = conf.GetConnectionString("DefaultConnectionString");
    SqlConnection cnn = new SqlConnection(connectionString);

    cnn.Open();
            
    SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM LoanStatus WHERE StatusCode >= 410", cnn);
    var count = (int)com.ExecuteScalar();

    cnn.Close();
    
    return count;
  }

 }

}