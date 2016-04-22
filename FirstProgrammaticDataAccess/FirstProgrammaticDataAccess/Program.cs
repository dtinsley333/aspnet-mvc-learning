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
        static void Main(string[] args)
        {
        }

        private SqlConnection Connection;
        private SqlCommand Command;
        private SqlDataReader Reader;
        private string results;

        //Create a Connection object.
        SqlConnection Connecton= New SqlConnection("Initial Catalog=Northwind;Data Source=localhost;Integrated Security=SSPI;');
    }
}
