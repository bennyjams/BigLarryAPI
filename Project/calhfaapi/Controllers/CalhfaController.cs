using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using calhfaapi.Services;
using calhfaapi.Models;


using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace calhfaapi.Controller
{
 [Route("api/[controller]/[action]")]
 [ApiController]
 public class CalhfaController : ControllerBase
 {

  private readonly Covid19Service _myobject;
  private readonly IConfiguration configuration;

  public CalhfaController(Covid19Service obj, IConfiguration config)
  {

   //allot this obj to out internal object of service class
   _myobject = obj;

   //initializes the config for the sql
   this.configuration = config;

  }

  public class output
  {
      public int x   {get;set;}
      public int y   {get;set;}
      public int z   {get;set;}
      public String hello {get;set;}
      public String world {get;set;}
  }

  [HttpGet]

  //public async Task<ActionResult<Stats>> sequence()
  public String Get(int id)
  {
    //return await _myobject.GetWorldStats();

    ///*
    string connectionString = configuration.GetConnectionString("DefaultConnectionString");
    SqlConnection cnn = new SqlConnection(connectionString);

    cnn.Open();
            
    SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM LoanStatus WHERE StatusCode >= 410", cnn);
    var count = (int)com.ExecuteScalar();

    SqlCommand com2 = new SqlCommand("SELECT COUNT(*) FROM LoanStatus WHERE StatusCode < 410", cnn);
    var count2 = (int)com2.ExecuteScalar();

    SqlCommand com3 = new SqlCommand("SELECT COUNT(*) FROM LoanStatus", cnn);
    var count3 = (int)com3.ExecuteScalar();

    cnn.Close();

    
    output o = new output
    {
        x = count,
        y = count2,
        z = count3,
        hello = "Hey CalHFA reps, how are you today?",
        world = "Team BigLarry, using ASP.NET Core 5.0 and refrence a (currently local) instance of SQL Server"
    };
    
    //String ret = "first number: " + count + ", second number: " + count2;

    string ret = JsonSerializer.Serialize(o);

    return ret;
    /* v CTRL CLICK v */
    //https://localhost:5001/api/Calhfa/get

    /* 
      Our current connection string 
      "Data Source=.;Initial Catalog=biglarrydb;Integrated Security=True"
    */
  }

 }
}