using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;



namespace EmployeeLeaveManagement.DAL.Repositories
{
    public class LeaveTypeRepository
    {
        private readonly EmployeeLeaveManagementDbContext _context;
        public LeaveTypeRepository(EmployeeLeaveManagementDbContext context)
        {
            _context = context;
        }


        // Get all leave types for dropdown

        public List<LeaveType> GetAllLeaveTypes()
        {
            try
            {
                return _context.LeaveTypes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LeaveType? GetLeaveTypeById(int id)
        {
            try
            {
                return _context.LeaveTypes
                               .FirstOrDefault(l => l.LeaveTypeId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
