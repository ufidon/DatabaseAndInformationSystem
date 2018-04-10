using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

namespace DbApp03
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=wigcompany.accdb";
            string queryString = @"select fname, lname from employee";
            try
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand(queryString, connection);

                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader[0].ToString()+" "+reader[1].ToString());
                }
                reader.Close();
                connection.Close();
            }
            catch(OleDbException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
