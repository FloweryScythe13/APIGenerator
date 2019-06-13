using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace API_Generator
{
    public class DataProvider
    {
        private string ConnectionString { get; }

        private string AccessToken { get; }

        private SqlCredential Credential { get; }

        private bool FireInfoMessageEventOnUserErrors { get; }

        private ISite Site { get; }

        private bool StatisticsEnabled { get; }

        private SqlConnection SqlConnection => new SqlConnection
        {
            ConnectionString = ConnectionString,
            AccessToken = AccessToken,
            Credential = Credential,
            FireInfoMessageEventOnUserErrors = FireInfoMessageEventOnUserErrors,
            Site = Site,
            StatisticsEnabled = StatisticsEnabled,
        };

        public DataProvider(string connectionString = null, string accessToken = null, SqlCredential credential = null, bool fireInfoMessageEventOnUserErrors = false, ISite site = null, bool statisticsEnabled = false)
        {
            ConnectionString = connectionString;
            AccessToken = accessToken;
            Credential = credential;
            FireInfoMessageEventOnUserErrors = fireInfoMessageEventOnUserErrors;
            Site = site;
            StatisticsEnabled = statisticsEnabled;
        }
        
        public IEnumerable<Column> GetColumns(string table)
        {
            return Execute(sqlConnection => GetColumns(sqlConnection, table));
        }


        public IEnumerable<Table> GetTables()
        {
            return Execute(GetTables);
        }
        
        private static IEnumerable<Column> GetColumns(SqlConnection sqlConnection, string table)
        {
            var columns = new List<Column>();

            using (var reader = new SqlCommand($"select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{table}'", sqlConnection).ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(new Column(reader.GetString(3), reader.GetString(6), reader.GetString(7)));
                }
            }

            return columns;
        }

        private static IEnumerable<Table> GetTables(SqlConnection sqlConnection)
        {
            var tables = new List<Table>();

            foreach (DataRow row in sqlConnection.GetSchema("Tables").Rows)
            {
                var name = (string) row[2];

                tables.Add(new Table(name, GetColumns(sqlConnection, name).ToArray()));
            }

            return tables;
        }

        private T Execute<T>(Func<SqlConnection, T> func)
        {
            using (var sqlConnection = SqlConnection)
            {
                sqlConnection.Open();

                return func.Invoke(sqlConnection);
            }
        }
    }
}