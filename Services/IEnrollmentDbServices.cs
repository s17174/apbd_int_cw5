using apbd_int_cw5.Requests;
using apbd_int_cw5.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Services
{
   
        public interface IEnrollmentDbServices
        {
            EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

            PromoteStudentsResponse PromoteStudents(PromoteStudentRequest request);
        }
    
}
