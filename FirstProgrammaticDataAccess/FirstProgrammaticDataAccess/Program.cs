using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirstProgrammaticDataAccess
{
    class Program
    {
        static void Main()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("select ani.CommonName,");
            sb.Append("ani.Gender,ani.Species,");
            sb.Append("ani.AcquiredDate,");
            sb.Append("h.Name, h.[Description] ");
            sb.Append("from Animals ani join Habitat h ");
            sb.Append("on ani.HabitatId = h.HabitatId ");
            sb.Append("order by ani.CommonName");
            var query = sb.ToString();

            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                "Data Source=alienware-pc\\sqlexpress;Initial Catalog=Zoolandia;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            SqlDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand(query, conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (rdr.Read())
                {
                    Console.WriteLine("Animal Name: " + rdr[0]+ "  Habitat: " + rdr[4]);
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
                Console.ReadLine();

            }
        }
    }
}
  