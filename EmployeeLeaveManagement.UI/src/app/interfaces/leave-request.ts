export interface ILeaveRequest
{
  leaveRequestId: number;
  employeeId: number;
  leaveTypeId: number;
  fromDate: Date;
  toDate: Date;
  reason: string;
  appliedDate: Date;
  status: string;
}
