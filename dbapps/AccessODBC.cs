using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Odbc;

namespace DbApp04
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Dsn=MS Access Database;dbq=wigcompany.accdb;defaultdir=D:\Users\hh\source\repos\dbs;driverid=25;fil=MS Access;maxbuffersize=2048;pagetimeout=5;uid=admin";
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
                    Console.WriteLine(reader[0].ToString()+" " + reader[1].ToString());
                }

                // Call Close when done reading.
                reader.Close();
                connection.Close();
            }
            catch(OdbcException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
