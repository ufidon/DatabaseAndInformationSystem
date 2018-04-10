using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DbApp01
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "server=localhost;user=cs440;database=wigcompany;port=3306;password=******";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                string sql = "SELECT FName, lname FROM employee WHERE country='USA'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }

                /*
                string sql = "INSERT INTO `employee` VALUES ('111111111','Andrew','','Johnson','M','1911-12-29','1111111111','johnson@facebook.com','1111','Raleigh','North Carolina','USA','11111')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                 */

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
