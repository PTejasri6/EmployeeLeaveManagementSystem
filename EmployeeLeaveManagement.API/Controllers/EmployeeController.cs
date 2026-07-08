using EmployeeLeaveManagement.DAL.Models;
using EmployeeLeaveManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            try
            {
                var employees = _employeeRepository.GetAllEmployees();

                if (employees == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Employee not found",
                        data = (object)null
                    });
                }
                return Json(new
                {
                    success = true,
                    data = employees
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get employees",
                    data = (object)null
                });
            }
        }


        [HttpGet]
        public JsonResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);

                if (employee == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Employee not found",
                        data = (object)null
                    });
                }


                return Json(new
                {
                    success = true,
                    data = employee
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get employee",
                    data = (object)null
                });
            }
        }


        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            try
            {
                int result = _employeeRepository.AddEmployee(employee);

                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Employee added successfully";
                        break;

                    case -1:
                        message = "First name is required";
                        break;

                    case -2:
                        message = "Last name is required";
                        break;

                    case -3:
                        message = "Invalid phone number";
                        break;

                    case -4:
                        message = "Phone number already exists";
                        break;

                    case -5:
                        message = "Invalid gender";
                        break;

                    case -6:
                        message = "Invalid date of birth or joining date";
                        break;

                    case -7:
                        message = "Department does not exist";
                        break;

                    case -8:
                        message = "Employee must be above 18 years";
                        break;

                    case -99:
                        message = "Something went wrong";
                        break;

                    default:
                        message = "Unable to add employee";
                        break;
                }


                return Json(new
                {
                    result = result,
                    message = message
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }



        [HttpPut]
        public JsonResult UpdateEmployee(Employee employee)
        {
            try
            {
                int result = _employeeRepository.UpdateEmployee(employee);
                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Employee updated successfully";
                        break;

                    case -1:
                        message = "Employee does not exists";
                        break;

                    case -2:
                        message = "First name is required";
                        break;

                    case -3:
                        message = "Last name is required";
                        break;

                    case -4:
                        message = "Invalid phone number";
                        break;

                    case -5:
                        message = "Phone number already exists";
                        break;

                    case -6:
                        message = "Invalid gender";
                        break;

                    case -7:
                        message = "Invalid date of birth or joining date";
                        break;

                    case -8:
                        message = "Department does not exist";
                        break;

                    case -9:
                        message = "Employee must be above 18 years";
                        break;

                    case -99:
                        message = "Something went wrong";
                        break;

                    default:
                        message = "Unable to update employee";
                        break;
                }
                return Json(new
                {
                    result = result,
                    message = message
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }




        [HttpDelete]
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                int result = _employeeRepository.DeleteEmployee(id);
                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Employee deleted successfully";
                        break;

                    case -1:
                        message = "Employee does not exists";
                        break;

                    case -99:
                        message = "Something went wrong";
                        break;

                    default:
                        message = "Unable to delete employee";
                        break;
                }
                return Json(new
                {
                    result = result,
                    message = message
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }
        [HttpGet]
        public JsonResult GetLeaveBalance(int employeeId)
        {
            try
            {
                var balance = _employeeRepository.GetLeaveBalance(employeeId);

                return Json(new
                {
                    success = true,
                    data = balance
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get leave balance",
                    data = (object)null
                });
            }
        }
    }
}