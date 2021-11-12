using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using calhfaapi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace calhfaapi.Services
{
 public class CalHFAService
 {
         private IConfiguration Configuration;
         public CalHFAService(IConfiguration con) {
           Configuration = con;
         }

    public String Get() {
 
    Output o = new Output();

 //using connection String
    using (SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("DefaultConnectionString")) )
    {
      SqlCommand com = new SqlCommand("SELECT (SELECT COUNT(*) FROM Loan) AS ComplianceLoansInLine, (SELECT MAX(StatusDate) FROM LoanStatus) AS ComplianceDate, (SELECT COUNT(*) FROM LoanType) AS SuspenseLoansInLine, (SELECT MIN(StatusDate) FROM LoanStatus) AS SuspenseDate, (SELECT COUNT(*) FROM LoanStatus) AS PostClosingLoansInLine, (SELECT MAX(ReservDateTime) FROM Loan) AS PostClosingDate, (SELECT COUNT(*) FROM LoanCategory) AS PostClosingSuspenseLoansInLine, (SELECT MAX(ReservDateTime) FROM Loan) AS PostClosingSuspenseDate", cnn);//oof

      cnn.Open();

      SqlDataReader reader = com.ExecuteReader();

      while(reader.Read())
      {
        o.ComplianceLoansInLine = (int)reader[0];
        o.ComplianceDate = (DateTime)reader[1];
        o.SuspenseLoansInLine = (int)reader[2];
        o.SuspenseDate = (DateTime)reader[3];
        o.PostClosingLoansInLine = (int)reader[4];
        o.PostClosingDate = (DateTime)reader[5];
        o.PostClosingSuspenseLoansInLine = (int)reader[6];
        o.PostClosingSuspenseDate = (DateTime)reader[7];
      }
    }
    string ret = JsonSerializer.Serialize(o);
    return ret;

    /* v CTRL CLICK v */
    //https://localhost:5001/api/Calhfa/get
    
    /******
    Need to figure out how to get date only fron 'date' datatype, as it gives date and time 
    --using a string is a possible option
    ******/

    /* 
      Our current connection string 
      "Data Source=.;Initial Catalog=biglarrydb;Integrated Security=True"
                                     ^ your local database's name
    */
  }

 }

}