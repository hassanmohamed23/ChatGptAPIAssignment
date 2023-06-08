using ChatGPTAssignmentAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenAI_API.Moderation;
using Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGPTAssignmentAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IRepository StudentRepo;
        private readonly IConfiguration _configuration;
        public StudentController(IRepository _StudentRepo)
        {
            StudentRepo = _StudentRepo;

        }
        // return student by registration number 
        [HttpGet]
        [Route("getById")]
        public async Task<double> GetById([FromQuery] int registrationNumber)
        {
            
            var student= await StudentRepo.FindById(registrationNumber);
            return student.Grade;
        }

        // Add new Student  
        [HttpPost]
        [Route("Add")]
        public async Task<Student> AddStudent(Student student)
        {
                    
           var result = await StudentRepo.Add(student);
           await StudentRepo.SaveChanges();
            return result;
        }

    }
}
