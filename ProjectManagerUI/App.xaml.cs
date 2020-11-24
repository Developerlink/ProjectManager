
using ProjectManagerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

    }

    //x TODO: 0) Design and mockup data classes and their relations.
    //x TODO: 1) Create Models folder and DataAccess folder in library.
    //x TODO: 2) In DataAccess: Create IDataConnection interface and SqlConnector class.
    //x TODO: 3) In SqlConnector: Get Dapper from Nuget manager.
    //x TODO: Add using statement to Dapper.
    //x TODO: Add connectionstring info in app.config
    //x TODO: Example: <add name ="TournamentsSql" 
    //x TODO: connectionString="Server=MININT-PGF1701;Database=TournamentsDB;Trusted_Connection=True;" 
    //x TODO: providerName="System.Date.SqlClient"/>
    //x TODO: 4) In Models: Create the designed data classes needed.
    //x TODO: 5) Create the form(s) needed in this app. 
    //x TODO: 6) Create the database and tables in the sql database. 
    //x TODO: 6a) Connect to database:
    //x TODO: 6a) Create Enums.cs and add the different types of connections that I want to support (Sql, SQLite etc.)
    //x TODO: 6a) Create the static Globalconfig.cs, the IDataConnection prop Connection, the InitializeConnection method and the
    //x TODO: 6a) ConnectionString method.
    //x TODO: 6a) Create the const string prop db in SqlConnector and use that in the using statement for every 
    //x TODO: 6a) new SqlConnection(GlobalConfig.Connection(db))
    //x TODO: 6a) That's it. 
    //x TODO: 6b) Make the queries:
    //x TODO: 6b) ad using system.data 
    //x TODO: 6b) Make the stored procedures in MS-SQL Server



    // TODO: 7) Add business and interaction logic to each form, starting with create, read, update, delete.
    // TODO: 8) Test and debug everything.
    // TODO: 9) Refactor and retest.
    // TODO: 10) Polish aesthetics of GUI. 
    // TODO: 11) Create Problem window, view/search problem window 
}
