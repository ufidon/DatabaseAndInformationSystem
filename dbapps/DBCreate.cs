using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SQLite;

using System.IO;

namespace DbApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Friends";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Friends(Id INTEGER PRIMARY KEY, 
                                    Name TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Tom')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Rebecca')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jim')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Robert')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Julian')";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
