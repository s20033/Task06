using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task06.Exceptions;
using Task06.Models;
using Task06.Services;

namespace Task06.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IDbService _dbService;
        private string ConnString = "Data Source=db-mssql;Initial Catalog = s20033; Integrated Security = True";
        //[3.1]


        public StudentsController(IDbService database)
        {
            this._dbService = database;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var result = new List<Students>();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand command = new SqlCommand())
            { //[3.2]
                command.Connection = con;
                command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                            "from Student s " +
                                            "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                            "join Studies st on st.IdStudy = e.IdStudy; ";

                con.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var std = new Students
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        IdEnrollment = new Enrollment
                        {
                            Semester = (int)dr["Semester"],
                            Studies = new Study { NameofStudy = dr["Studies"].ToString() }
                        }


                    };
                    result.Add(std);
                }
                return Ok(result);
            }
            //[3.3]
        }
        [HttpGet("{indexNumber}")]// localhost:54010/api/students/s235
        public IActionResult GetStudent(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (var command = new SqlCommand())
            {
                command.Connection = con;
                command.CommandText = "select semester from student, enrollment, studies where student.idenrollment = enrollment.idenrollment and studies.idstudy = enrollment.idstudy and indexNumber=" + "'" + @indexNumber + "'";
                command.Parameters.AddWithValue("IndexNumber", indexNumber);
                con.Open();
                var dr = command.ExecuteReader();
                if (dr.Read())
                {
                    var en = new Enrollment();
                    var sem = en.Semester = (int)(dr["Semester"]);
                    return Ok("Student with Index Number " + indexNumber + " is on " + sem + " semester");
                }
                else
                {
                    return Ok("There is no student with Index Number " + indexNumber + " Please enter other Index number. ");
                }
            }

        }
        // [3.4/3.5]

        [HttpPost]
        public IActionResult CreateStudent([FromServices]IDbService service, Students newStudent)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = con;
                command.CommandText = "insert into student(IndexNumber, FirstName, LastName,BirthDate, IdEnrollment) values (@par1, @par2, @par3,@par4,@par5)";

                command.Parameters.AddWithValue("s789", newStudent.IndexNumber);
                command.Parameters.AddWithValue("Snoopy", newStudent.FirstName);
                command.Parameters.AddWithValue("Dogg", newStudent.LastName);
                command.Parameters.AddWithValue("1980-10-09", newStudent.BirthDate);
                command.Parameters.AddWithValue("3", newStudent.IdEnrollment);

                con.Open();
                int affectedRows = command.ExecuteNonQuery();
            }

            return Ok(newStudent);
        }
       
       
            

        }
    


    }

  