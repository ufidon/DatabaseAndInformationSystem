using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SQLite;

namespace DbApp02
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection m_dbConnection;
            string connectionString = @"Data Source=wigcompany.db; 
                                        Version=3; FailIfMissing=True; Foreign Keys=True;";
            try
            {
                m_dbConnection = new SQLiteConnection(connectionString);
                m_dbConnection.Open();

                /* create/drop table, insert/update/delete records
                string sql = @"create table ...";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                */

                string sql = @"select fname || ' ' || lname as `full name`, country  from employee limit 5";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["full name"] + " is from " + reader["country"]);

                m_dbConnection.Close();
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
