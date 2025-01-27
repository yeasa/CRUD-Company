

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/",()=>{
    var response = new {message = "this is a json object "};
    return Results.Ok(response);
});

List<Department> departments = new List<Department>();

// read a departmennt GET: /api/departments
app.MapGet("/api/departments",()=>{
    return Results.Ok(departments);
});

// create a departmennt POST: /api/departments
app.MapPost("/api/departments",([FromBody] Department departmentData)=>{
    var newDepartment= new Department{
        departmentid= Guid.NewGuid(),
        departmentName= departmentData.departmentName,
        managerId=departmentData.managerId
    };
    departments.Add(newDepartment);
    return Results.Created($"/api/departments/{newDepartment.departmentid}",departments);
    
});

// delete a departmennt GET: /api/departments
app.MapDelete("/api/departments{departmentid}",(Guid depadepartmentid)=>{
    var foundDepartment = departments.FirstOrDefault(Department => Department.departmentid == depadepartmentid);
    if (foundDepartment==null){
        return Results.NotFound($"department not found with {depadepartmentid} department id");
    };
    departments.Remove(foundDepartment);
    return Results.NoContent();
});

app.MapPut("/api/departments",()=>{
    var foundDepartment = departments.FirstOrDefault(Department => Department.departmentid==Guid.Parse("123e4567-e89b-12d3-a456-426614174000"));
    if (foundDepartment==null){
        return Results.NotFound($"department not found with {Guid.Parse("123e4567-e89b-12d3-a456-426614174000")} department id");
    };
    foundDepartment.departmentName="salls";
    return Results.NoContent();
});


app.Run();

// DTO
public record Department
{
public Guid departmentid{ get; set;}
public string? departmentName{get; set;}
public int? managerId{get; set;}
};


// crud
// create a departmennt POST: /api/departments
// read a departmennt GET: /api/departments
// update a departmennt put: /api/departments
// delete a departmennt delete: /api/departments