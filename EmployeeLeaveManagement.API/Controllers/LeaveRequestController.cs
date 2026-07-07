using EmployeeLeaveManagement.DAL.Models;
using EmployeeLeaveManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace EmployeeLeaveManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly LeaveRequestRepository repository;

        public LeaveRequestController(LeaveRequestRepository leaveRequestRepository)
        {
            repository = leaveRequestRepository;
        }

        // Apply Leave
        [HttpPost]
        public JsonResult ApplyLeave(LeaveRequest leaveRequest)
        {
            try
            {
                int result = repository.ApplyLeave(leaveRequest);

                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Leave applied successfully";
                        break;
                    case -1:
                        message = "Employee not found";
                        break;
                    case -2:
                        message = "Invalid leave type";
                        break;
                    case -3:
                        message = "Invalid leave dates";
                        break;
                    case -4:
                        message = "Cannot apply leave for past dates";
                        break;
                    case -5:
                        message = "Reason is required";
                        break;
                    case -6:
                        message = "Leave already exists for selected dates";
                        break;
                    case -7:
                        message = "Insufficient leave balance";
                        break;
                    default:
                        message = "Something went wrong";
                        break;
                }

                return Json(new
                {
                    result,
                    message
                });
            }
            catch
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }



        // Approve / Reject Leave
        [HttpPut]
        public JsonResult UpdateLeaveStatus(int leaveRequestId, string status)
        {
            try
            {
                int result = repository.UpdateLeaveStatus(leaveRequestId, status);

                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Leave status updated successfully";
                        break;
                    case -1:
                        message = "Leave request not found";
                        break;
                    case -2:
                        message = "Invalid status";
                        break;
                    case -3:
                        message = "Leave request already processed";
                        break;
                    default:
                        message = "Something went wrong";
                        break;
                }

                return Json(new
                {
                    result,
                    message
                });
            }
            catch
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }

        // Cancel Leave
        [HttpPut]
        public JsonResult CancelLeaveRequest(int leaveRequestId, int employeeId)
        {
            try
            {
                int result = repository.CancelLeaveRequest(leaveRequestId, employeeId);

                string message = "";

                switch (result)
                {
                    case 1:
                        message = "Leave cancelled successfully";
                        break;
                    case -1:
                        message = "Employee not found";
                        break;
                    case -2:
                        message = "Leave request not found";
                        break;
                    case -3:
                        message = "Leave does not belong to this employee";
                        break;
                    case -4:
                        message = "Only pending leave can be cancelled";
                        break;
                    default:
                        message = "Something went wrong";
                        break;
                }

                return Json(new
                {
                    result,
                    message
                });
            }
            catch
            {
                return Json(new
                {
                    result = -99,
                    message = "Server error"
                });
            }
        }

        // Employee Leave History
        [HttpGet]
        public JsonResult GetLeavesByEmployeeId(int employeeId)
        {
            try
            {
                var leaves = repository.GetLeavesByEmployeeId(employeeId);

                if (leaves == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No leave requests found",
                        data = (object)null
                    });
                }

                return Json(new
                {
                    success = true,
                    data = leaves
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get leave requests",
                    data = (object)null
                });
            }
        }

        // Admin - View All Leave Requests
        [HttpGet]
        public JsonResult GetAllLeaveRequests()
        {
            try
            {
                var leaves = repository.GetAllLeaveRequests();

                if (leaves == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No leave requests found",
                        data = (object)null
                    });
                }

                return Json(new
                {
                    success = true,
                    data = leaves
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to get leave requests",
                    data = (object)null
                });
            }
        }
    }
}