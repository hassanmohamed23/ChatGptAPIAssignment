using ChatGPTAssignmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public interface IRepository
    {
        Task<Student> FindById(int RegistrationNumber);
        Task<Student> Add(Student student);
        Task SaveChanges();

    }
}
