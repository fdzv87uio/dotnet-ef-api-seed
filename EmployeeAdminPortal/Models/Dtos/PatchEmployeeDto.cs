﻿namespace EmployeeAdminPortal.Models.Dtos
{
    public class PatchEmployeeDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public decimal? Salary { get; set; }
    }
}
