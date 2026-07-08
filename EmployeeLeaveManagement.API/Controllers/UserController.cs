using EmployeeLeaveManagement.DAL.Models;
using EmployeeLeaveManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserRepository repository;


        public UserController(UserRepository userRepository)
        {
            repository = userRepository;
        }



        [HttpGet]
        public JsonResult Login(string email, string password)
        {
            try
            {
                var user = repository.Login(email, password);


                if (user == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid email or password",
                        data = (object)null
                    });
                }


                
                   return Json(new
                   {
                       success = true,
                       message = "Login successful",
                       data = new
                       {
                           user.UserId,
                           user.EmployeeId,
                           user.Email,
                           Role = user.Role.RoleName,
                           Name = user.Employee.FirstName + " " + user.Employee.LastName
                       }
                 
            });

            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to login",
                    data = (object)null
                });
            }
        }

    }
}