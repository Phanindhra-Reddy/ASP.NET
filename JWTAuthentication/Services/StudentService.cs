using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using MongoDB.Driver;

namespace JWTAuthentication.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> students;

        public StudentService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("HyphenDb"));
            var database = client.GetDatabase("HyphenDb");
            students = database.GetCollection<Student>("Students");
        }

        public List<Student> GetStudents() => students.Find(student => true).ToList();
        public Student GetStudent(string id) => students.Find<Student>(student => student.studentId == id).FirstOrDefault();
        public Student AddStudent(Student student)
        {
            students.InsertOne(student);
            return student;
        }

    }
}