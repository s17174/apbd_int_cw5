using apbd_int_cw5.Models;
using apbd_int_cw5.Requests;
using apbd_int_cw5.Response;
using System;
using System.Data;
using System.Data.SqlClient;

namespace apbd_int_cw5.Services
{
    public class EnrollmentDbServices : IEnrollmentDbServices
        {
          
            public EnrollStudentResponse EnrollStudent(EnrollStudentRequest req)
            {
                EnrollStudentResponse resp = null;
                Enrollment respEnrollment = new Enrollment();
                SqlTransaction transaction = null;

                using (var con = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS))
                using (var com = new SqlCommand())
                {
                    com.CommandText = SqlUtils.SqlUtils.DB_GET_STUDY_ID_BY_STUDY_NAME;
                    com.Parameters.AddWithValue("StudyName", req.Studies);
                    com.Transaction = transaction;

                    com.Connection = con;
                    con.Open();
                    transaction = con.BeginTransaction();

                    com.Transaction = transaction;
                    SqlDataReader dr = com.ExecuteReader();
                    if (!dr.Read())
                    {

                        dr.Close();
                        transaction.Rollback();
                        dr.Dispose();
                        throw new ArgumentException("Studies not found");
                    }
                    int idStudy = (int)dr["IdStudy"];
                    dr.Close();



                    int idEnrollment = 0;

                    com.CommandText = SqlUtils.SqlUtils.DB_SELECT_ALL_FROM_ENROLMENT_BY_SEMESTER_AND_STUDIES_ID;
                    com.Parameters.AddWithValue("IdStudy", idStudy);
                    com.Transaction = transaction;
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        idEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    }
                    else
                    {
                        dr.Close();

                        com.CommandText = SqlUtils.SqlUtils.DB_SELECT_MAX_ENROLLMENT;
                        com.Transaction = transaction;
                        dr = com.ExecuteReader();

                        idEnrollment = Int32.Parse(dr["EnrolmentMaxId"].ToString());
                        DateTime todayDate = DateTime.Today;
                        com.CommandText = SqlUtils.SqlUtils.DB_INSERT_ENROLMENT;
                        com.Parameters.AddWithValue("@TodayDate", todayDate);
                        com.Parameters.AddWithValue("@EnrolmentId", idEnrollment);
                        com.ExecuteNonQuery();
                    }
                    dr.Close();

                    com.CommandText = SqlUtils.SqlUtils.DB_SELECT_ALL_FROM_STUDENTS_BY_INDEX_NUMBER;
                    com.Parameters.AddWithValue("IndexNumber", req.IndexNumber);
                    com.Transaction = transaction;
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        transaction.Rollback();
                        dr.Dispose();
                        throw new InvalidOperationException("Niewlasciwy numer studenta");
                    }
                    dr.Close();

                    com.CommandText = SqlUtils.SqlUtils.DB_INSERT_STUDENT;
                    com.Parameters.AddWithValue("FirstName", req.FirstName);
                    com.Parameters.AddWithValue("LastName", req.LastName);
                    com.Parameters.AddWithValue("BirthDate", req.BirthDate);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    com.Transaction = transaction;
                    com.ExecuteNonQuery();
                    dr.Close();

                    com.CommandText = SqlUtils.SqlUtils.DB_SELECT_ALL_ENROLL_WITH_ID;
                    com.Transaction = transaction;
                    dr = com.ExecuteReader();
                    dr.Read();

                resp = new EnrollStudentResponse
                {
                    LastName = req.LastName,
                    Semester = 1,
                    Studies = req.Studies,
                    StartDate = dr["StartDate"].ToString()
                };

                dr.Dispose();
                    transaction.Commit();
                }
                return resp;
            }


            public PromoteStudentsResponse PromoteStudents(PromoteStudentRequest req)
            {
                Enrollment respEnrollment = new Enrollment();
                var resp = new PromoteStudentsResponse();

                SqlCommand com = new SqlCommand();
                using (SqlConnection con = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS))
                {
                    con.Open();
                    var transaction = con.BeginTransaction();
                    com.Connection = con;
                    com.Transaction = transaction;
                    com.CommandText = SqlUtils.SqlUtils.DB_QUERRY_GET_ENROLMENT_BY_NAME_AND_STUDY_ID;
                    com.Parameters.AddWithValue("studyName", req.Studies);
                    com.Parameters.AddWithValue("semesterNumber", req.Semester);

                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        dr.Close();

                        transaction.Rollback();

                    }
                    else
                    {
                        dr.Close();
                        using (SqlConnection conn = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("PromoteRequest", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@studies", req.Studies));
                            cmd.Parameters.Add(new SqlParameter("@semester", req.Semester));
                            cmd.ExecuteReader();
                            conn.Close();
                        }
                        resp.Semester = req.Semester + 1;
                        resp.StartDate = DateTime.Now.ToString("dd.MM.yyyy");
                        resp.StudiesName = req.Studies;
                        transaction.Commit();
                    }
                }
                return resp;
            }
        }
}
