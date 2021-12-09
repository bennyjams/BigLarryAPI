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

    String commandWorking   = "SELECT ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( 	SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 	GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingSuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2) AS PostClosingSuspenseDate";

    String commandNoCounts  = "SELECT ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( 	SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 9999991 ) AS ComplianceLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 	GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 99999999991 ) AS SuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 422 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 999999992 ) AS PostClosingLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 510 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 29999999 ) AS PostClosingSuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2) AS PostClosingSuspenseDate";

    String commandNothing   = "SELECT ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( 	SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 999999410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 	GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 9999999999410 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 499999999999999922 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 4999999999999922 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 5999999999999999999999910 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 5999999999910 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID ) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 999999999522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingSuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS trick, MAX(ls2.StatusSequence) AS treat FROM LoanStatus ls2 GROUP BY LoanID) AS yeet ON yeet.trick = l.LoanID) WHERE ls.StatusCode = 999999999999999522 AND yeet.treat = ls.StatusSequence AND lt.LoanCategoryID = 2) AS PostClosingSuspenseDate";

    try 
    {
      SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("DefaultConnectionString"));
      //SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("FailingConnectionString"));

      using ( cnn )
      {
        SqlCommand com = new SqlCommand( commandWorking, cnn);//oof

        cnn.Open();

        SqlDataReader reader = com.ExecuteReader();

        DateTime ifZeroLoans = DateTime.Now;

        //iterate through the items in the reader, order same as output object
        //if no loans fit a category, date will return null, so we replace with current date

        while(reader.Read())
        {
          o.ComplianceLoansInLine = (int)reader[0];
          if(reader.IsDBNull(1)) {
              o.PostClosingSuspenseDate = ifZeroLoans.ToString("MM/dd/yyyy");
          }
          else {
              DateTime dt1 = (DateTime)reader[1];
              o.ComplianceDate = dt1.ToString("MM/dd/yyyy");
          }

          o.SuspenseLoansInLine = (int)reader[2];
          if(reader.IsDBNull(3)) {
              o.PostClosingSuspenseDate = ifZeroLoans.ToString("MM/dd/yyyy");
          }
          else {
              DateTime dt2 = (DateTime)reader[3];
              o.SuspenseDate = dt2.ToString("MM/dd/yyyy");
          }

          o.PostClosingLoansInLine = (int)reader[4];
          if(reader.IsDBNull(5)) {
              o.PostClosingSuspenseDate = ifZeroLoans.ToString("MM/dd/yyyy");
          }
          else {
              DateTime dt3 = (DateTime)reader[5];
              o.PostClosingDate = dt3.ToString("MM/dd/yyyy");
          }
          
          o.PostClosingSuspenseLoansInLine = (int)reader[6];
          if(reader.IsDBNull(7)) {
              o.PostClosingSuspenseDate = ifZeroLoans.ToString("MM/dd/yyyy");
          }
          else {
              DateTime dt4 =(DateTime)reader[7];
              o.PostClosingSuspenseDate = dt4.ToString("MM/dd/yyyy");
          }
        }//end of while
      }//end of using

      string ret = JsonSerializer.Serialize(o);
      return ret;
    }//end of try
    catch (Exception ex)
    {
      if(ex is SqlException)
      {
        o.ComplianceLoansInLine = -1;
        o.ComplianceDate = "SQL Connection Failure";
        o.SuspenseLoansInLine = -1;
        o.SuspenseDate = "SQL Connection Failure";
        o.PostClosingLoansInLine = -1;
        o.PostClosingDate = "SQL Connection Failure";
        o.PostClosingSuspenseLoansInLine = -1;
        o.PostClosingSuspenseDate = "SQL Connection Failure";

        string ret2 = JsonSerializer.Serialize(o);
        return ret2;
      }
      else
      {
        o.ComplianceLoansInLine = -2;
        o.ComplianceDate = "Unknown Failure";
        o.SuspenseLoansInLine = -2;
        o.SuspenseDate = "Unknown Failure";
        o.PostClosingLoansInLine = -2;
        o.PostClosingDate = "Unknown Failure";
        o.PostClosingSuspenseLoansInLine = -2;
        o.PostClosingSuspenseDate = "Unknown Failure";

        string ret3 = JsonSerializer.Serialize(o);
        return ret3;
      }
    }

    /* v CTRL CLICK v */
    //https://localhost:5001/api/Calhfa/get
    //
    // v for swagger v
    //https://localhost:5001/swagger/index.html

    /* 
      Our current connection string 
      "Data Source=.;Initial Catalog=biglarrydb;Integrated Security=True"
                                     ^ your local database's name
    */
  }

 }

}