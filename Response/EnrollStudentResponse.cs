using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Response
{
        public class EnrollStudentResponse
        {
            public string LastName { get; set; }
            public int Semester { get; set; }
            public string StartDate { get; set; }
            public string Studies { get; set; }
        }
    }