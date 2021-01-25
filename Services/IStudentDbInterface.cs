using apbd_int_cw5.Models;
using System.Collections.Generic;

namespace apbd_int_cw5.Services
{
    interface IStudentDbInterface
    {
        List<Student> GetStudentsFromDb();
        Semester GetSemester(string id);

        bool TrueStudent(string indexNumber);
    }
}
