using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using EmployeeLeaveManagement.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace EmployeeLeaveManagement.DAL.Repositories
{
    public class LeaveRequestRepository
    {
        private readonly EmployeeLeaveManagementDbContext _context;
        public LeaveRequestRepository(EmployeeLeaveManagementDbContext context)
        {
            _context = context;
        }

        public int ApplyLeave(LeaveRequest leaveRequest)
        {
            int result = 0;

            try
            {
                SqlParameter employeeId = new SqlParameter("@EmployeeId", leaveRequest.EmployeeId);
                SqlParameter leaveTypeId = new SqlParameter("@LeaveTypeId", leaveRequest.LeaveTypeId);
                SqlParameter fromDate = new SqlParameter("@FromDate", leaveRequest.FromDate);
                SqlParameter toDate = new SqlParameter("@ToDate", leaveRequest.ToDate);
                SqlParameter reason = new SqlParameter("@Reason", leaveRequest.Reason);


                var returnValue = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_ApplyLeave {employeeId},{leaveTypeId},{fromDate},{toDate},{reason}")
                    .ToList();

                result = returnValue.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }


        // Update leave status (Approve / Reject)

        public int UpdateLeaveStatus(int leaveRequestId, string status)
        {
            int result = 0;

            try
            {
                SqlParameter requestId = new SqlParameter("@LeaveRequestId", leaveRequestId);
                SqlParameter leaveStatus = new SqlParameter("@Status", status);


                var returnValue = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_UpdateLeaveStatus {requestId},{leaveStatus}")
                    .ToList();


                result = returnValue.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }



        // cancel leave request 

        public int CancelLeaveRequest(int leaveRequestId, int employeeid)
        {
            int result = 0;

            try
            {
                SqlParameter requestId = new SqlParameter("@LeaveRequestId", leaveRequestId);
                SqlParameter id = new SqlParameter("@EmployeeId", employeeid);


                var returnValue = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_CancelLeaveRequest {requestId},{id}")
                    .ToList();


                result = returnValue.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }

        


        // Get leaves by employee id

        public List<LeaveRequest> GetLeavesByEmployeeId(int employeeId)
        {
            try
            {
                return _context.LeaveRequests
                               .Include(l => l.LeaveType)
                               .Where(l => l.EmployeeId == employeeId)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Get all leave requests (Admin)

        public List<LeaveRequest> GetAllLeaveRequests()
        {
            try
            {
                return _context.LeaveRequests
                        .Include(l => l.Employee)
                        .Include(l => l.LeaveType)
                        .ToList();
                               
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
