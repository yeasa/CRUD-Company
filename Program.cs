

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

app.MapControllers();   
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