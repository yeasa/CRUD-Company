using System;

namespace crudProject.Models
{
    // DTO
    public class DepartmentModel
    {
        public Guid departmentid { get; set; }

        public string? departmentName { get; set; }

        public int? managerId { get; set; }
    };
}
