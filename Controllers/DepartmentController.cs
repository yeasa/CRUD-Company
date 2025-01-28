using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using crudProject.Models;

namespace crudProject.Controllers
{
    [ApiController]
    [Route("api/departments/")]
    public class DepartmentController : ControllerBase
    {
        public static List<Department> departments = new List<Department>();

        [HttpGet]
        public IActionResult GetDepartments([FromQuery] string searchValue = "")
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchDepartment = departments.Where(department => department.departmentName == searchValue);
                return Ok(searchDepartment);
            }
            return Ok(departments);
        }

        [HttpPost]
        public IActionResult PostDepartments([FromBody] Department DepartmentData)
        {
            var newDepartment = new Department
            {
                departmentid = Guid.NewGuid(),
                departmentName = DepartmentData.departmentName,
                managerId = DepartmentData.managerId
            };
            departments.Add(newDepartment);
            return Created($"/api/departments/{newDepartment.departmentid}", newDepartment);
        }

        [HttpPut]
        public IActionResult PutDepartments(Guid departmentId, [FromBody] Department departmentData)
        {
            var foundDepartment = departments.FirstOrDefault(department => department.departmentid == departmentId);
            if (foundDepartment == null)
            {
                return NotFound($"Department not found with ID {departmentId}");
            }

            foundDepartment.departmentName = departmentData.departmentName;
            foundDepartment.managerId = departmentData.managerId;

            return NoContent();
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            var foundDepartment = departments.FirstOrDefault(department => department.departmentid == departmentId);
            if (foundDepartment == null)
            {
                return NotFound($"Department not found with ID {departmentId}");
            }

            departments.Remove(foundDepartment);
            return NoContent();
        }
    }
}
