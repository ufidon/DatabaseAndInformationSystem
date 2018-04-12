using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data;
using System.Data.SQLite;

namespace DbApp08
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

                sqlite> update product set image=readfile('brownwig.jpg') where upc=100;
                sqlite> select writefile('brown.jpg',image) from product where upc=100;
                */

                byte[] data = null;

                try
                {
                    data = File.ReadAllBytes("brown.jpg");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                SQLiteCommand command = new SQLiteCommand(m_dbConnection);

                // Insert image
                command.CommandText = "UPDATE product SET image=@img WHERE upc=101";
                command.Prepare();

                command.Parameters.Add("@img", DbType.Binary, data.Length);
                command.Parameters["@img"].Value = data;
                command.ExecuteNonQuery();

                // Retrieve image
                command.CommandText = "SELECT image FROM product WHERE upc=101";
                data = (byte[])command.ExecuteScalar();

                try
                {
                    if (data != null)
                    {
                        File.WriteAllBytes("brown1.jpg", data);
                    }
                    else
                    {
                        Console.WriteLine("Binary data not read");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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
