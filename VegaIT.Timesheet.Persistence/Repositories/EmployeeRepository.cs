using CSharpFunctionalExtensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Entities.Enumerations;
using VegaIT.Timesheet.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace VegaIT.Timesheet.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string strConString = "Server=.;Database=TimesheetDB;Trusted_Connection=True;";

    public void Create(Employee employee)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "INSERT INTO [Employees] ([EmployeeId], [Name], [Username], [Password], [Email], [HoursPerWeek], [Status], [Role])" +
            " VALUES(@id, @name, @username, @password, @email, @hoursperweek, @status, @role)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@id", employee.EmployeeId);
        cmd.Parameters.AddWithValue("@name", employee.Name);
        cmd.Parameters.AddWithValue("@username", employee.Username);
        cmd.Parameters.AddWithValue("@password", employee.Password);
        cmd.Parameters.AddWithValue("@email", employee.Email);
        cmd.Parameters.AddWithValue("@hoursperweek", employee.HoursPerWeek);
        cmd.Parameters.AddWithValue("@status", employee.Status);
        cmd.Parameters.AddWithValue("@role", employee.Role);
        cmd.ExecuteNonQuery();
    }

    public void DeleteById(Guid id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "DELETE FROM [Employees] WHERE [EmployeeId] = @employeeid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", id);
        cmd.ExecuteNonQuery();
    }

    public void Edit(Employee employee)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "UPDATE [Employees] SET [Name]=@name, [Username]=@username, [Password]=@password, [Email]=@email" +
            ", [HoursPerWeek]=@hoursperweek, [Status]=@status, [Role]=@role WHERE [EmployeeId]=@employeeid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", employee.EmployeeId);
        cmd.Parameters.AddWithValue("@name", employee.Name);
        cmd.Parameters.AddWithValue("@username", employee.Username);
        cmd.Parameters.AddWithValue("@password", employee.Password);
        cmd.Parameters.AddWithValue("@email", employee.Email);
        cmd.Parameters.AddWithValue("@hoursperweek", employee.HoursPerWeek);
        cmd.Parameters.AddWithValue("@status", employee.Status);
        cmd.Parameters.AddWithValue("@role", employee.Role);
        cmd.ExecuteNonQuery();
    }

    public void EditPassword(Guid employeeId, string newPassword)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "UPDATE [Employees] SET [Password]=@password WHERE [EmployeeId]=@employeeid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", employeeId);
        cmd.Parameters.AddWithValue("@password", newPassword);
        cmd.ExecuteNonQuery();
    }

    public List<Employee> FindAll()
    {
        var listOfEmployees = new List<Employee>();

        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT [EmployeeId], [Name], [Username], [Password], [Email], [HoursPerWeek], [Status], [Role] FROM [Employees]";
            SqlCommand cmd = new(query, con);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse(reader.GetString(6), out EStatusOfEmployee statusOfEmployee);
                Enum.TryParse(reader.GetString(7), out ERole role);
                var employee = Employee.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetString(4), reader.GetFloat(5), statusOfEmployee, role);

                listOfEmployees.Add(employee.Value);
            }
        }
        return listOfEmployees;
    }

    public Maybe<Employee> FindById(string id)
    {
        using SqlConnection con = new(strConString);
        try
        {
            con.Open();
            string query = "SELECT [EmployeeId], [Name], [Username], [Password], [Email], [HoursPerWeek], [Status], [Role]" +
                " FROM [Employees] WHERE ([EmployeeId]=@employeeid)";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@employeeid", id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Enum.TryParse(reader.GetString(6), out EStatusOfEmployee statusOfEmployee);
                Enum.TryParse(reader.GetString(7), out ERole role);
                var employee = Employee.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetFloat(5), statusOfEmployee, role);
                return Maybe.From(employee.Value);
            }
            return Maybe.None;
        }
        catch (Exception ex)
        {
            return Maybe.None;
        }
    }

    public PageOf<Employee> FindPage(int pageSize, int pageNumber)
    {

        var listOfEmployees = new List<Employee>();
        var totalPages = 0;

        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT [EmployeeId], [Name], [Username], [Password], [Email], [HoursPerWeek]" +
                ", [Status], [Role] FROM [Employees] ORDER BY EmployeeId OFFSET (@pagesize * (@pagenumber - 1)) ROWS FETCH NEXT @pagesize ROWS ONLY";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse(reader.GetString(6), out EStatusOfEmployee statusOfEmployee);
                Enum.TryParse(reader.GetString(7), out ERole role);
                var employee = Employee.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetString(4), reader.GetFloat(5), statusOfEmployee, role);

                listOfEmployees.Add(employee.Value);
            }
        }
        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) as rowsum FROM [Employees]";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalPages = reader.GetInt32(0);
            }
        }
        totalPages = totalPages / pageSize;
        if (totalPages < 1) { totalPages = 1; }
        return new PageOf<Employee>(totalPages, listOfEmployees);
    }
}
