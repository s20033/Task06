using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Task06.Models;

namespace Task06.Services
{
    public class SqlDbService :IDbService 

    {
        private string ConnString = "Data Source=db-mssql16;Initial Catalog=s20033;Integrated Security=True";

        public Students GetStudentByIndex(string index)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand command = new SqlCommand())
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "select count(1) " +
                "from student " +
                "where IndexNumber = @index";
                command.Parameters.AddWithValue("index", index);
                var count = int.Parse(command.ExecuteScalar().ToString());
                if (count > 0)
                {
                    return new Students();
                }

                return null;
            }
        }
        public IEnumerable<Students> GetStudents()
        {
            //...
            throw new NotImplementedException();
        }

        public void SaveLogData(string data)
        {

        }

        IEnumerable<Students> IDbService.GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
}
