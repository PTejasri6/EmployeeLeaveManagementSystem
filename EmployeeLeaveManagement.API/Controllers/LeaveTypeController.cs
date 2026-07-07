using EmployeeLeaveManagement.DAL.Models;
using EmployeeLeaveManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeLeaveManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeaveTypeController : Controller
    {
        private readonly LeaveTypeRepository repository;
        public LeaveTypeController(LeaveTypeRepository lerepository)
        {
            repository = lerepository;
        }


        [HttpGet]
        public JsonResult GetAllLeaveTypes()
        {
            try
            {
                var leaveTypes = repository.GetAllLeaveTypes();


                if (leaveTypes == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Leave types not found",
                        data = (object)null
                    });
                }
                return Json(new
                {
                    success = true,
                    data = leaveTypes
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "unable to get Leave types ",
                    data = (object)null
                });
            }
        }


        [HttpGet]
        public JsonResult GetLeaveTypeById(int id)
        {
            try
            {
                var leaveType = repository.GetLeaveTypeById(id);


                if (leaveType == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Leave type not found",
                        data = (object)null

                    });
                }


                return Json(new
                {
                    success = true,
                    data = leaveType
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get leave type",
                    data = (object)null
                });
            }
        }

    }
}