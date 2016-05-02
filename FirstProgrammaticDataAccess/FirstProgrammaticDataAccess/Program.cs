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
            sb.Append("Select ani.CommonName,");
            sb.Append("ani.Gender, ani.Species,");
            sb.Append("ani.AcquiredDate,");
            sb.Append("h.Name, h.[Description] ");
            sb.Append("from Animals ani join Habitat h ");
            sb.Append("on ani.HabitatId = h.HabitatId ");
            sb.Append("order by ani.CommonName");
            var query = sb.ToString();
            using (SqlConnection connection = new SqlConnection("Data Source=alienware-pc\\sqlexpress;Initial Catalog=Zoolandia;Integrated Security=True;Pooling=False"))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Animal Name: " + reader[0] + "  Habitat: " + reader[4]);
                        }
                    }
                    Console.ReadLine();

                }
            }
        }
    }
}
       

  