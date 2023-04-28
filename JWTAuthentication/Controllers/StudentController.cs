using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentService Service;
        public StudentController(StudentService _service)
        {
            Service = _service;
        }
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            return Service.GetStudents();
        }

        [HttpGet("{studentId}")]
        public ActionResult<Student> GetStudent(string Id)
        {
            var student = Service.GetStudent(Id);
            return student;
        }
        [HttpPost]
        public ActionResult<Student> Add(Student student)
        {
            Service.AddStudent(student);
            return student;
        }


    }
}