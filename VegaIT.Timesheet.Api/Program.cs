using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using VegaIT.Timesheet.Core.Repositories;
using VegaIT.Timesheet.Persistence.Models.DBContext;
using VegaIT.Timesheet.Persistence.Repositories;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IWorkDayRepository, WorkDayRepository>();

var app = builder.Build();

//builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer("Server=.;Database=TimesheetDB;Trusted_Connection=True;"));



app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

