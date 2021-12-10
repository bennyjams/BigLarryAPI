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
    public CalHFAService(IConfiguration con) 
    {
      Configuration = con;
    }

    public String Get() 
    {
      Output o = new Output();

      String commandWorking   = "SELECT ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( 	SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 410 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 	GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 410 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS ComplianceDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 422 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 422 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 1 ) AS SuspenseDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 510 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 510 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingDate, ( SELECT COUNT(ls.LoanID) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID ) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 522 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 2 ) AS PostClosingSuspenseLoansInLine, ( SELECT MIN(StatusDate) FROM (((Loan l  INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID) INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID) INNER JOIN ( SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status FROM LoanStatus ls2 GROUP BY LoanID) AS subgroup ON subgroup.loan = l.LoanID) WHERE ls.StatusCode = 522 AND subgroup.status = ls.StatusSequence AND lt.LoanCategoryID = 2) AS PostClosingSuspenseDate";
      //a better formatted version of this query is in the readme.txt file in this project

      try 
      {
        //SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("DefaultConnectionString"));
        SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("EmptyConnectionString"));
        //SqlConnection cnn = new SqlConnection(Configuration.GetConnectionString("FailingConnectionString"));

        using ( cnn )
        {
          SqlCommand com = new SqlCommand( commandWorking, cnn);

          cnn.Open();

          SqlDataReader reader = com.ExecuteReader();

          DateTime ifZeroLoans = DateTime.Now;

          //iterate through the items in the reader, order same as output object
          //if no loans fit a category, date will return null, so we replace with current date

          String dateFormat = "MM/dd/yyyy";

          while(reader.Read())
          {
            o.ComplianceLoansInLine = (int)reader[0];
            if( o.ComplianceLoansInLine == 0 )
            {//if no loans fit the category, default to the current date
                o.ComplianceDate = ifZeroLoans.ToString(dateFormat);
            }
            else
            {//otherwise assign the sql output to a datetime variable and format properly
                DateTime dt1 = (DateTime)reader[1];
                o.ComplianceDate = dt1.ToString(dateFormat);
            }

            o.SuspenseLoansInLine = (int)reader[2];
            if( o.SuspenseLoansInLine == 0 )
            {
                o.SuspenseDate = ifZeroLoans.ToString(dateFormat);
            }
            else 
            {
                DateTime dt2 = (DateTime)reader[3];
                o.SuspenseDate = dt2.ToString(dateFormat);
            }

            o.PostClosingLoansInLine = (int)reader[4];
            if( o.PostClosingLoansInLine == 0 ) 
            {
                o.PostClosingDate = ifZeroLoans.ToString(dateFormat);
            }
            else 
            {
                DateTime dt3 = (DateTime)reader[5];
                o.PostClosingDate = dt3.ToString(dateFormat);
            }
            
            o.PostClosingSuspenseLoansInLine = (int)reader[6];
            if( o.PostClosingSuspenseLoansInLine == 0 ) 
            {
                o.PostClosingSuspenseDate = ifZeroLoans.ToString(dateFormat);
            }
            else 
            {
                DateTime dt4 =(DateTime)reader[7];
                o.PostClosingSuspenseDate = dt4.ToString(dateFormat);
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