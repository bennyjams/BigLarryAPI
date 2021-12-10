Overview of BigLarry's API for CalHFA

Setup:

1 - change connection string (in appsettings.json) to relevant Connection String:

2 - in VSCode, open a terminal window and execute the command: dotnet run

3 - GET "<hostname>/api/Calhfa/get" for the raw json, 
	or"<hostname>/swagger/index.html" for swagger

The API, when prompted with a GET command, will begin executing the GET() program
in the calhfaServices.cs file. This program will query the SQL database through 
the Connection String (located in the appsettings.json file), sending a raw SQL 
command that returns 8 items, 4 loan counts and their subsequent dates. The API 
will then assign those values to variables in an object named Output (in the 
Output.cs file) and serialize that object using JsonSerializer, then return the 
data requested in json format.

In Output, the loan counts are of type int, and the dates are of type String, 
so they can be converted to a chosen format more easily. The date format is 
located in a string variable below the SQL command and can be changed freely; 
the default we're using is MM/dd/yyyy

Connection String is located in appsettings.json at line 11
for more information on Connection Strings: https://www.connectionstrings.com/

SQL command is located in calhfaServices.cs at line 22



raw SQL command, but formatted in a more readable fashion:

SELECT
	(
	SELECT COUNT(ls.LoanID)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 410
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 1
	) AS ComplianceLoansInLine,
	(
	SELECT MIN(StatusDate)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 410
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 1
	) AS ComplianceDate,
	(
	SELECT COUNT(ls.LoanID)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 422
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 1
	) AS SuspenseLoansInLine,
	(
	SELECT MIN(StatusDate)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 422
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 1
	) AS SuspenseDate,
	(
	SELECT COUNT(ls.LoanID)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 510
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 2
	) AS PostClosingLoansInLine,
	(
	SELECT MIN(StatusDate)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 510
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 2
	) AS PostClosingDate,
	(
	SELECT COUNT(ls.LoanID)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 522
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 2
	) AS PostClosingSuspenseLoansInLine,
	(
	SELECT MIN(StatusDate)
		FROM (((Loan l 
				INNER JOIN LoanStatus ls ON l.LoanID = ls.LoanID)
				INNER JOIN LoanType lt ON lt.LoanTypeID = l.LoanTypeID)
				INNER JOIN (
					SELECT ls2.LoanID AS loan, MAX(ls2.StatusSequence) AS status
					FROM LoanStatus ls2
					GROUP BY LoanID
			) AS subgroup ON subgroup.loan = l.LoanID)
		WHERE ls.StatusCode = 522
		AND subgroup.status = ls.StatusSequence
		AND lt.LoanCategoryID = 2
	) AS PostClosingSuspenseDate







