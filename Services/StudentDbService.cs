using apbd_int_cw5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Services
{
        public class StudentDbService : IStudentDbInterface
        {

            public List<Student> getStudentsFromDb()
            {
                List<Student> StudentsList = new List<Student>();
                using (var client = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS))
                using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = SqlUtils.SqlUtils.DB_SELECT_ALL_FROM_STUDENT;
                    client.Open();
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                    var st = new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        IndexNumber = dr["IndexNumber"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        IdEnrollment = (int)dr["IdEnrollment"]
                    };
                    StudentsList.Add(st);
                    }
                    client.Close();
                }
                return StudentsList;
            }

            public Semester getSemester(string id)
            {
                var sem = new Semester();
                using (var client = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS))
                using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = SqlUtils.SqlUtils.DB_SELECT_ALL_FROM_ENROLMENT_BY_SEMESTER_AND_STUDIES_ID;
                    command.Parameters.AddWithValue("@id", id);
                    client.Open();
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        sem.SemesterNum = (int)dr["Semester"];
                    }
                    client.Close();
                }
                return sem;
            }
        }
}
