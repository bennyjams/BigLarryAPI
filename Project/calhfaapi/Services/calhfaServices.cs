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
      SqlCommand com = new SqlCommand("SELECT ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( 	SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 	GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingSuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2) AS PostClosingSuspenseDate", cnn);//oof

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