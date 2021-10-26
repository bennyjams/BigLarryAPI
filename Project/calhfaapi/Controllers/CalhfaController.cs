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
 public class output
 {
     public int x   {get;set;}
     public int y   {get;set;}
 }

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

    cnn.Close();

    
    output o = new output
    {
        x = count,
        y = count2
    };
    
    //String ret = "first number: " + count + ", second number: " + count2;
    string jsonString = JsonSerializer.Serialize(o);

    return jsonString;
    //*/
    //return _myobject.GetFromSQL(configuration);
  }

 }
}