using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveManagement.DAL.Models;
 
namespace EmployeeLeaveManagement.DAL.Repositories
{
    public class UserRepository
    {
        private readonly EmployeeLeaveManagementDbContext _context;
        public UserRepository(EmployeeLeaveManagementDbContext context)
        {
            _context = context;
        }


        // Login user
        public User? Login(string email, string password)
        {
            try
            {
                return _context.Users
    .Include(u => u.Role)
    .Include(u => u.Employee)
    .FirstOrDefault(u => u.Email == email && u.Password == password);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
