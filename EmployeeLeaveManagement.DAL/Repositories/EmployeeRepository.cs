using EmployeeLeaveManagement.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeLeaveManagement.DAL.Repositories
{
    public class EmployeeRepository
    {
        private readonly EmployeeLeaveManagementDbContext _context;

        public EmployeeRepository(EmployeeLeaveManagementDbContext context)
        {
            _context = context;
        }



        // Get all employees data


        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }


        // Get  empluyees data by id

        public Employee GetEmployeeById(int id)
        {
            Employee e = new Employee();
            try
            {
                e = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == id);

            }
            catch (Exception)
            {
                e = null;
            }
            return e;
        }



        //Add Employee detilas

        public int AddEmployee(Employee employee)
        {
            int result = 0;

            try
            {
                SqlParameter firstName = new SqlParameter("@FirstName", employee.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", employee.LastName);
                SqlParameter phone = new SqlParameter("@Phone", employee.Phone);
                SqlParameter gender = new SqlParameter("@Gender", employee.Gender);
                SqlParameter dob = new SqlParameter("@DateOfBirth", employee.DateOfBirth);
                SqlParameter joiningDate = new SqlParameter("@JoiningDate", employee.JoiningDate);
                SqlParameter departmentId = new SqlParameter("@DepartmentId", employee.DepartmentId);


                var output = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_AddEmployee {firstName},{lastName},{phone},{gender},{dob},{joiningDate},{departmentId}")
                    .ToList();


                result = output.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }




        //Update employee 


        public int UpdateEmployee(Employee employee)
        {
            int result = 0;

            try
            {
                SqlParameter employeeId = new SqlParameter("@EmployeeId", employee.EmployeeId);
                SqlParameter firstName = new SqlParameter("@FirstName", employee.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", employee.LastName);
                SqlParameter phone = new SqlParameter("@Phone", employee.Phone);
                SqlParameter gender = new SqlParameter("@Gender", employee.Gender);
                SqlParameter dateOfBirth = new SqlParameter("@DateOfBirth", employee.DateOfBirth);
                SqlParameter joiningDate = new SqlParameter("@JoiningDate", employee.JoiningDate);
                SqlParameter departmentId = new SqlParameter("@DepartmentId", employee.DepartmentId);


                var returnValue = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_UpdateEmployee {employeeId},{firstName},{lastName},{phone},{gender},{dateOfBirth},{joiningDate},{departmentId}")
                    .ToList();


                result = returnValue.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }

        //Delete employee by id 
        
        public int DeleteEmployee(int employeeId)
        {
            int result = 0;

            try
            {
                SqlParameter id = new SqlParameter("@EmployeeId", employeeId);

                var returnValue = _context.Database
                    .SqlQuery<int>(
                    $"EXEC sp_DeleteEmployee {id}")
                    .ToList();

                result = returnValue.FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }

            return result;
        }


        public List<object> GetLeaveBalance(int employeeId)
        {
            var leaveBalance = _context.LeaveTypes
                .Select(lt => new
                {
                    LeaveType = lt.LeaveTypeName,
                    TotalLeaves = lt.MaximumLeaves,
                    UsedLeaves = _context.LeaveRequests
                        .Count(lr => lr.EmployeeId == employeeId
                                  && lr.LeaveTypeId == lt.LeaveTypeId
                                  && lr.Status == "Approved"),
                    RemainingLeaves = lt.MaximumLeaves - _context.LeaveRequests
                        .Count(lr => lr.EmployeeId == employeeId
                                  && lr.LeaveTypeId == lt.LeaveTypeId
                                  && lr.Status == "Approved")
                })
                .Cast<object>()
                .ToList();

            return leaveBalance;
        }
    }
    }

