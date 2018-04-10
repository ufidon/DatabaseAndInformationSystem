using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Odbc;

namespace DbApp05
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Dsn=SQLite3 Datasource;database=wigcompany.db;stepapi=0;syncpragma=NORMAL;notxn=0;timeout=100000;shortnames=0;longnames=0;nocreat=0;nowchar=0;fksupport=1;oemcp=0;bigint=0;jdconv=0";
            string queryString = @"SELECT fname, lname FROM employee";
            try
            {
                OdbcConnection connection = new OdbcConnection(connectionString);
                OdbcCommand command = new OdbcCommand(queryString, connection);

                connection.Open();

                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                }

                // Call Close when done reading.
                reader.Close();
                connection.Close();
            }
            catch (OdbcException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
