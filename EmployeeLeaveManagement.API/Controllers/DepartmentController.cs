using Microsoft.AspNetCore.Mvc;
using EmployeeLeaveManagement.DAL.Models;
using EmployeeLeaveManagement.DAL.Repositories;
using System.Linq.Expressions;

namespace EmployeeLeaveManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepository repository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            repository = departmentRepository;
        }

        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            try
            {
                var departments = repository.GetAllDepartments();
                if (departments == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Departments not found",
                        data = (object)null
                    });
                }
                return Json(new
                {
                    success = true,
                    data = departments
            }); 
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get departments",
                    data = (object)null
                });
            
            }
        }


        [HttpGet]

        public JsonResult GetDepartmentById(int id)
        {
            try
            {
                var department = repository.GetDepartmentById(id);
                if (department == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Department not found",
                        data = (object)null
                    });
                }
                return Json(new
                {
                    success = true,
                    data = department
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get department",
                    data = (object)null
                });

            }
        }

    }
}
