using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public int EmployeeId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
