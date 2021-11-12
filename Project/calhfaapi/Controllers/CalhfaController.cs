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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace calhfaapi.Controller
{
 [Route("api/[controller]/[action]")]
 [ApiController]
 public class CalhfaController : ControllerBase
 {
  private readonly CalHFAService _myobject;
  private readonly IConfiguration _configuration;

  public CalhfaController(CalHFAService obj, IConfiguration con)
  {
   //allot this obj to out internal object of service class
   _myobject = obj;
   _configuration = con;

  }

  [HttpGet]
    public String Get(int id)
    {
      CalHFAService service = new CalHFAService(_configuration);
      return _myobject.Get();

    }
  }
}