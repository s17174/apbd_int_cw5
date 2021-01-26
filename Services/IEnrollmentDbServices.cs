using apbd_int_cw5.Models;
using apbd_int_cw5.Requests;
using apbd_int_cw5.Response;

namespace apbd_int_cw5.Services
{

    public interface IEnrollmentDbServices
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentRequest request);

        Student GetStudentPassword(string index);
    }
    
}
