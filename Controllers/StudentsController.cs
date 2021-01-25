using System;
using System.Collections.Generic;
using apbd_int_cw5.Models;
using apbd_int_cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_int_cw5.Controllers
{

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentDbInterface studentsDB = new StudentDbService();

        [HttpGet]
        public IActionResult GetStudents()

        {
            List<Student> list = studentsDB.getStudentsFromDb();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Models.Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound($"Nie znalezeiono studenta od id {id}");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            return Ok($"Aktualizacja studenta o id {id} dokończona");
        }


        [HttpDelete("{id}")]
        public IActionResult Deletetudent(int id)
        {
            return Ok($"Usuwanie studenta o id {id} ukończone");
        }

        [HttpGet("getSemester/{id}")]
        public IActionResult GetSemestrByIndex(string id)
        {
            Semester sem = studentsDB.getSemester(id);
            return Ok(sem);
        }

    }
}
