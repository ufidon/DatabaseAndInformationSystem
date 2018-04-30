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

                string sql = @"select fname || ' ' || lname as `full name`, country, Bdate  from employee limit 5";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["full name"] + " is from " + reader["country"] 
                        + ", born on "+ DateTime.Parse(reader["Bdate"].ToString()));

                reader.Close();


                sql = @"insert into shipper values(77,'UPS','7777777777')";
                command = new SQLiteCommand(sql, m_dbConnection);
                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows.ToString() + " rows affected!");


                sql = @"update shipper set PhoneNum='8888888888' where id=77";
                command = new SQLiteCommand(sql, m_dbConnection);
                rows = command.ExecuteNonQuery();
                Console.WriteLine(rows.ToString() + " rows affected!");


                sql = @"select *  from shipper";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("ID: " + reader["id"].ToString() + " is " + reader["Name"]
                        + " with phone number:  " + reader["PhoneNum"]);

                reader.Close();

                m_dbConnection.Close();



            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
