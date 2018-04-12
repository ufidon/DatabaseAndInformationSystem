using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Data;
using System.Data.SQLite;

namespace DbApp09
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

                string nrows = null;

                try
                {
                    Console.Write("Enter rows to fetch: ");
                    nrows = Console.ReadLine();
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.ToString());
                }

                SQLiteCommand cmd = m_dbConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM employee LIMIT @ssn";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@ssn", Int32.Parse(nrows));

                int cols = 0;
                int rows = 0;

                SQLiteDataReader rdr = cmd.ExecuteReader();

                // Get schema
                DataTable schemaTable = rdr.GetSchemaTable();

                foreach (DataRow row in schemaTable.Rows)
                {
                    foreach (DataColumn col in schemaTable.Columns)
                        Console.WriteLine(col.ColumnName + " = " + row[col]);
                    Console.WriteLine();
                }

                cols = rdr.FieldCount;
                Console.WriteLine("The statement has affected {0} rows", rdr.RecordsAffected);
                rows = 0;

                // Get column headers
                Console.WriteLine(String.Format("{0, -3} {1, -8} {2, 8}",
                        rdr.GetName(0), rdr.GetName(1), rdr.GetName(2)));
                while (rdr.Read())
                {
                    rows++;
                    Console.WriteLine(String.Format("{0, -3} {1, -8} {2, 8}",
                        rdr.GetString(0), rdr.GetString(1), rdr.GetString(2)));
                }

                Console.WriteLine("The query fetched {0} rows", rows);
                Console.WriteLine("Each row has {0} cols", cols);

                // Get table name from sqlite_master
                string stm = @"SELECT name FROM sqlite_master WHERE type='table' ORDER BY name";

                cmd = new SQLiteCommand(stm, m_dbConnection);
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr.GetString(0));
                    }
                }




                m_dbConnection.Close();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
