using apbd_int_cw5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Services
{
    interface IStudentDbInterface
    {
        List<Student> getStudentsFromDb();
        Semester getSemester(string id);
    }
}
