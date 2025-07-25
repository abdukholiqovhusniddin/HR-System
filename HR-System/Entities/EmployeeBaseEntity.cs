﻿namespace HR_System.Entities;
public abstract class EmployeeBaseEntity: BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}