using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

using System.IO;
// http://zetcode.com/db/sqlitecsharp/trans/

namespace DbApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = "URI=file:test.db";

            SQLiteConnection con = null;
            SQLiteTransaction tr = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(cs);

                con.Open();

                tr = con.BeginTransaction();
                cmd = con.CreateCommand();

                cmd.Transaction = tr;
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
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jane')";
                cmd.ExecuteNonQuery();

                tr.Commit();

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                if (tr != null)
                {
                    try
                    {
                        tr.Rollback();

                    }
                    catch (SQLiteException ex2)
                    {

                        Console.WriteLine("Transaction rollback failed.");
                        Console.WriteLine("Error: {0}", ex2.ToString());

                    }
                    finally
                    {
                        tr.Dispose();
                    }
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (tr != null)
                {
                    tr.Dispose();
                }

                if (con != null)
                {
                    try
                    {
                        con.Close();

                    }
                    catch (SQLiteException ex)
                    {

                        Console.WriteLine("Closing connection failed.");
                        Console.WriteLine("Error: {0}", ex.ToString());

                    }
                    finally
                    {
                        con.Dispose();
                    }
                }
            }
        }
    }
}
