using ProjectManagerLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary
{
    // A compilation of global configurations
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        // Selects which storage method to use. 
        public static void InitializeConnections(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                // TODO: Create the SQL connection.
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseType.TextFile)
            {
                // Do something else
            }
        }

        // Returns the connectionstring stored in App.Config by referencing the name that was chosen
        // to identify the string. 
        public static string GetConnectionStringFromAppConfigByName(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        // Returns the value stored in App.Config by referencing the key that was defined
        // to unlock the value. The value can be anything like the default filePath or greaterWins (see file for example).
        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
