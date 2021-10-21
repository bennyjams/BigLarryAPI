using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;


//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
//using System.Data.SqlClient, Version=0.0.0.0, Culture=neutral, PublicKeyTokey=b03f5f7f11d50a3a;
//using System.Data.SqlTypes;

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


//using Belgrade.SqlClient;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("Pizza")]    //Controller")]

    public class PizzaController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public PizzaController(IConfiguration config)
        {
            this.configuration = config;
        }

        //GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            PizzaService.GetAll();

        //GET by Id action
        [HttpGet("{id}")]
        public int Get(int id)//ActionResult<Pizza> Get (int id)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection cnn = new SqlConnection(connectionString);

            cnn.Open();
            SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM LoanStatus", cnn);
            var count = (int)com.ExecuteScalar();
            
            //ViewData["TotalData"] = count;
            //Pizza ret = new Pizza((count + 2), "database pizza", false);

            cnn.Close();
            
            return count;

            //connectionString = @"Data Source= ;Initial Catalog= ;
            //   User ID= ;Password= ";

            /*
            var piz = PizzaService.Get(id);
            if (piz == null)
                return NotFound();
            return piz;
            */
        }

        //POST action

        //PUT action

        //DELETE action
    }

}