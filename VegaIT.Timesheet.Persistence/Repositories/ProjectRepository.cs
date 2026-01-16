using CSharpFunctionalExtensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Entities.Enumerations;
using VegaIT.Timesheet.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace VegaIT.Timesheet.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly string strConString = "Server=.;Database=TimesheetDB;Trusted_Connection=True;";

    public void Create(Project project)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "INSERT INTO [Projects] ([ProjectId], [Client], [Leader], [ProjectName], " +
            "[Description], [Status]) VALUES(@id, @client, @leader, @name, @desc, @status)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@id", project.ProjectId);
        cmd.Parameters.AddWithValue("@client", project.Client.ClientId);
        cmd.Parameters.AddWithValue("@leader", project.Leader.EmployeeId);
        cmd.Parameters.AddWithValue("@name", project.ProjectName);
        cmd.Parameters.AddWithValue("@desc", project.Description);
        cmd.Parameters.AddWithValue("@status", project.Status);
        cmd.ExecuteNonQuery();
    }

    public void DeleteById(Guid id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "DELETE FROM [Projects] WHERE [ProjectId] = @projectid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@projectid", id);
        cmd.ExecuteNonQuery();
    }

    public void Edit(Project project)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "UPDATE [Projects] SET [Client]=@client, [Leader]=@leader, [ProjectName]=@name," +
            " [Description]=@desc, [Status]=@status WHERE [ProjectId]=@projectid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@projectid", project.ProjectId);
        cmd.Parameters.AddWithValue("@client", project.Client.ClientId);
        cmd.Parameters.AddWithValue("@leader", project.Leader.EmployeeId);
        cmd.Parameters.AddWithValue("@name", project.ProjectName);
        cmd.Parameters.AddWithValue("@desc", project.Description);
        cmd.Parameters.AddWithValue("@status", project.Status);
        cmd.ExecuteNonQuery();
    }

    public Maybe<Project> FindById(string id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "SELECT [Projects.ProjectId], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description]," +
                " [Projects.Status], [Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Employee.Name]," +
                " [Employees.Username], [Employees.Password], [Employees.Email], [Employees.HoursPerWeek], [Employees.Status], [Employees.Role]" +
                " FROM [Projects] " +
                "INNER JOIN [Clients] ON [Clients.ClientId] = [Projects.Client]" +
                "INNER JOIN Employees ON [Employees.EmployeeId] = [Projects.Leader]" +
                " WHERE [Projects.ProjectId] = @projectid";

        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@projectid", id);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Enum.TryParse(reader.GetString(16), out EStatusOfEmployee statusOfEmployee);
            Enum.TryParse(reader.GetString(17), out ERole employeeRole);
            Enum.TryParse(reader.GetString(5), out EStatusOfProject statusOfProject);

            var client = Client.Create(reader.GetGuid(1), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
            var employee = Employee.Create(reader.GetGuid(3), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetFloat(15), statusOfEmployee, employeeRole);
            var project = Project.Create(reader.GetGuid(0), client.Value, employee.Value, reader.GetString(3), reader.GetString(4), statusOfProject);

            return Maybe.From(project.Value);
        }
        return Maybe.None;
    }

    public PageOf<Project> FindPageByFirstLetterAndNameSubstring(string firstLetter, string name, int pageSize, int pageNumber)
    {
        var listOfProjects = new List<Project>();
        var totalPages = 0;

        string prvi = firstLetter + "%";
        string drugi = "%" + name + "%";


        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT Projects.ProjectId, Projects.Client, Projects.Leader, Projects.ProjectName, Projects.Description," +
                " Projects.Status, Clients.Name, Clients.Address, Clients.City, Clients.PostalCode, Clients.Country, Employee.Name," +
                " Employees.Username, Employees.Password, Employees.Email, Employees.HoursPerWeek, Employees.Status, Employees.Role" +
                " FROM [Projects] " +
                "INNER JOIN [Clients] ON Clients.ClientId = Projects.Client" +
                "INNER JOIN Employees ON Employees.EmployeeId = Projects.Leader" +
                " WHERE (Projects.ProjectName LIKE @firstletter) AND (Projects.ProjectName LIKE @name) ORDER BY Projects.ProjectId OFFSET (@pagesize * (@pagenumber - 1)) ROWS FETCH NEXT @pagesize ROWS ONLY";

            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", prvi);
            cmd.Parameters.AddWithValue("@name", drugi);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);

            // Create the first command and execute
         //   var command = new SqlCommand("<SQL Command>", myConnection);
         //   var reader = command.ExecuteReader();

            // Change the SQL Command and execute
        //    command.CommandText = "<New SQL Command>";  Da li mogu samo ovo da ubacim unutar while petlje da pretrazuje employee i client tabele?
    //        command.ExecuteNonQuery();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse(reader.GetString(16), out EStatusOfEmployee statusOfEmployee);
                Enum.TryParse(reader.GetString(17), out ERole employeeRole);
                Enum.TryParse(reader.GetString(5), out EStatusOfProject statusOfProject);

                var client = Client.Create(reader.GetGuid(1), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                var employee = Employee.Create(reader.GetGuid(3), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetFloat(15), statusOfEmployee, employeeRole);
                var project = Project.Create(reader.GetGuid(0), client.Value, employee.Value, reader.GetString(3), reader.GetString(4), statusOfProject);
                
                listOfProjects.Add(project.Value);
            }
        }
        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) as rowsum FROM [Projects]  WHERE ([Projects.ProjectName] LIKE @firstletter) AND ([Projects.ProjectName] LIKE @name)";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", firstLetter);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalPages = reader.GetInt32(0);
            }
        }
        totalPages = totalPages / pageSize;
        return new PageOf<Project>(totalPages, listOfProjects);

    }
}
