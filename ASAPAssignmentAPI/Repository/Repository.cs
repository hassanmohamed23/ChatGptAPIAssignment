using ChatGPTAssignmentAPI.Models;
using DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public class Repository :IRepository
    {
        MyDbContext context;
      
        public Repository(MyDbContext _context)
        {
            context = _context;

        }

        //Get By Lamada Experssion
        public async Task<Student> FindById(int RegistrationNumber)
        {
            var student= await context.students.FirstOrDefaultAsync(i => i.RegistrationNumber == RegistrationNumber);
            return student;
        }

        //Add new student
        public async Task<Student> Add(Student student)
        {
            await context.students.AddAsync(student);          
            return student;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }



    }
}
