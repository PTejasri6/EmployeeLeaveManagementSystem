using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.DAL.Models;

public partial class LeaveType
{
    public int LeaveTypeId { get; set; }

    public string LeaveTypeName { get; set; } = null!;

    public int MaximumLeaves { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
