using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.DAL.Repositories
{
    public class DepartmentRepository
    {
        private readonly EmployeeLeaveManagementDbContext _context;
        public DepartmentRepository(EmployeeLeaveManagementDbContext context)
        {
            _context = context;
        }

        // Get all departments for dropdown

        public List<Department> GetAllDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Get department by id

        public Department? GetDepartmentById(int id)
        {
            try
            {
                return _context.Departments
                               .FirstOrDefault(d => d.DepartmentId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
