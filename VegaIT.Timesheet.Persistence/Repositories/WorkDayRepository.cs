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

public class WorkDayRepository : IWorkDayRepository
{
    private readonly string strConString = "Server=.;Database=TimesheetDB;Trusted_Connection=True;";

    public void Create(WorkDay workDay)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "INSERT INTO [WorkDays] ([WorkDayId], [EmployeeId], [Date]) VALUES(@id, @employeeid, @date)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@id", workDay.DayId);
        cmd.Parameters.AddWithValue("@employeeid", workDay.Employee.EmployeeId);
        cmd.Parameters.AddWithValue("@date", workDay.Date.ToString());
        cmd.ExecuteNonQuery();

        string queryTwo = "INSERT INTO [SingleTasks] ([SingleTaskId], [WorkDayId], [ClientId], [CategoryId], [ProjectId], [Date], [Description], " +
            "[WorkingHours], [OvertimeHours]) VALUES(@id, @workdayid, @clientid, @categoryid, @projectid, @date, @desc, @workinghours, @overtimehours)";
        SqlCommand cmd2 = new(queryTwo, con);
        foreach (SingleTask s in workDay.Tasks)
        {
            cmd2.Parameters.AddWithValue("@id", s.Id);
            cmd2.Parameters.AddWithValue("@workdayid", workDay.DayId);
            cmd2.Parameters.AddWithValue("@clientid", s.Client.ClientId);
            cmd2.Parameters.AddWithValue("@categoryid", s.Category.CategoryId);
            cmd2.Parameters.AddWithValue("@projectid", s.Project.ProjectId);
            cmd2.Parameters.AddWithValue("@date", s.Date);
            cmd2.Parameters.AddWithValue("@desc", s.Description);
            cmd2.Parameters.AddWithValue("@workinghours", s.WorkingHours);
            cmd2.Parameters.AddWithValue("@overtimehours", s.OvertimeHours);
            cmd2.ExecuteNonQuery();
        }
    }

    public Maybe<WorkDay> FindByEmployeeIdAndDate(Guid employeeId, DateOnly date)
    {

        using SqlConnection con = new(strConString);
        con.Open();
        string query = "SELECT [WorkDays.WorkDayId], [WorkDays.EmployeeId], [WorkDays.Date], [SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId]," +
            "[SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours], [Employees.Name]," +
            " [Employees.Username], [Employees.Password], [Employees.Email], [Employees.HoursPerWeek], [Employees.Status], [Employees.Role]" +
            "FROM [WorkDays]" +
            "INNER JOIN SingleTasks ON SingleTasks.WorkDayId=WorkDays.WorkDayId INNER JOIN Employees ON WorkDays.EmployeeId=Employees.EmployeeId" +
            "WHERE [EmployeeId] = @employeeid AND [Date] = @date";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", employeeId);
        cmd.Parameters.AddWithValue("@date", date);

        var listOfSingleTasks = new List<SingleTask>();
        var listOfWorkDays = new List<WorkDay>();

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string query2 = "[SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId], [SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours]" +
                "[Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Categories.CategoryName], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description], [Projects.Status]" +
                " FROM [SingleTasks] " +
                "INNER JOIN Clients ON SingleTasks.ClientId=Clients.ClientId " +
                "INNER JOIN Categories ON SingleTasks.CategoryId=Categories.CategoryId " +
                "WHERE [WorkDayId] = @workdayid";
            SqlCommand cmd2 = new(query2, con);
            cmd2.Parameters.AddWithValue("@workdayid", reader.GetGuid(1));

            using var reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                string query3 = "SELECT [Projects.ProjectId], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description]," +
            " [Projects.Status], [Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Employee.Name]," +
            " [Employee.Username], [Employee.Password], [Employee.Email], [Employee.HoursPerWeek], [Employee.Status], [Employee.Role]" +
            " FROM [Projects] " +
            "INNER JOIN [Clients] ON [Clients.ClientId] = [Projects.Client]" +
            "INNER JOIN Employees ON [Employees.EmployeeId] = [Projects.Leader] WHERE [Projects.ProjectId] = @projectid";
                SqlCommand cmd3 = new(query, con);
                cmd3.Parameters.AddWithValue("@projectid", reader2.GetGuid(3));
                var listOfProjects = new List<Project>();  //trebace mi izvan scope-a while petlje, kako da inicijalizujem pre petlje (morao sam listu jer bi morao koristiti Project.Create f-ju koja ocekuje Client i Employee kao parametre, oni takodje moraju preko .Create metode)
                using var reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {
                    Enum.TryParse(reader3.GetString(16), out EStatusOfEmployee statusOfEmployee2);
                    Enum.TryParse(reader3.GetString(17), out ERole employeeRole2);
                    Enum.TryParse(reader3.GetString(5), out EStatusOfProject statusOfProject);
                    var client2 = Client.Create(reader3.GetGuid(1), reader3.GetString(6), reader3.GetString(7), reader3.GetString(8), reader3.GetString(9), reader3.GetString(10));
                    var employee2 = Employee.Create(reader3.GetGuid(3), reader3.GetString(11), reader3.GetString(12), reader3.GetString(13), reader3.GetString(14), reader3.GetFloat(15), statusOfEmployee2, employeeRole2);

                    listOfProjects.Add(Project.Create(reader3.GetGuid(0), client2.Value, employee2.Value, reader3.GetString(3), reader3.GetString(4), statusOfProject).Value);
                }
                var client = Client.Create(reader2.GetGuid(1), reader2.GetString(8), reader2.GetString(9), reader2.GetString(10), reader2.GetString(11), reader2.GetString(12));
                var category = Category.Create(reader2.GetGuid(2), reader2.GetString(13));

                var st = SingleTask.Create(reader2.GetGuid(0), client.Value, category.Value, listOfProjects.ElementAt(0), reader2.GetString(4), reader2.GetString(5), reader2.GetFloat(6), reader2.GetFloat(6));
                listOfSingleTasks.Add(st.Value);
            }

            Enum.TryParse(reader.GetString(16), out EStatusOfEmployee statusOfEmployee);
            Enum.TryParse(reader.GetString(17), out ERole employeeRole);
            var employee = Employee.Create(reader.GetGuid(1), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetFloat(15), statusOfEmployee, employeeRole);

            var workDay = WorkDay.Create(reader.GetGuid(0), employee.Value, reader.GetString(2), listOfSingleTasks);
            listOfWorkDays.Add(workDay.Value);

        }
        return Maybe.From(listOfWorkDays.ElementAt(0));
    }

    public List<WorkDay> FindWorkingMonthByEmployeeIdAndDate(Guid employeeId, DateOnly date)
    {
        using SqlConnection con = new(strConString);
        con.Open();
        string query = "SELECT [WorkDays.WorkDayId], [WorkDays.EmployeeId], [WorkDays.Date], [SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId]," +
            "[SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours], [Employees.Name]," +
            " [Employees.Username], [Employees.Password], [Employees.Email], [Employees.HoursPerWeek], [Employees.Status], [Employees.Role]" +
            "FROM [WorkDays]" +
            "INNER JOIN SingleTasks ON SingleTasks.WorkDayId=WorkDays.WorkDayId INNER JOIN Employees ON WorkDays.EmployeeId=Employees.EmployeeId" +
            "WHERE [EmployeeId] = @employeeid AND [Date] BETWEEN @date1 AND date2";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", employeeId);
        cmd.Parameters.AddWithValue("@date1", date.AddDays(-30));
        cmd.Parameters.AddWithValue("@date2", date.AddDays(30));

        var listOfSingleTasks = new List<SingleTask>();
        var listOfWorkDays = new List<WorkDay>();

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string query2 = "[SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId], [SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours]" +
                "[Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Categories.CategoryName], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description], [Projects.Status]" +
                " FROM [SingleTasks] " +
                "INNER JOIN Clients ON SingleTasks.ClientId=Clients.ClientId " +
                "INNER JOIN Categories ON SingleTasks.CategoryId=Categories.CategoryId " +
                "WHERE [WorkDayId] = @workdayid";
            SqlCommand cmd2 = new(query2, con);
            cmd2.Parameters.AddWithValue("@workdayid", reader.GetGuid(1));

            using var reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                string query3 = "SELECT [Projects.ProjectId], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description]," +
            " [Projects.Status], [Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Employee.Name]," +
            " [Employee.Username], [Employee.Password], [Employee.Email], [Employee.HoursPerWeek], [Employee.Status], [Employee.Role]" +
            " FROM [Projects] " +
            "INNER JOIN [Clients] ON [Clients.ClientId] = [Projects.Client]" +
            "INNER JOIN Employees ON [Employees.EmployeeId] = [Projects.Leader] WHERE [Projects.ProjectId] = @projectid";
                SqlCommand cmd3 = new(query, con);
                cmd3.Parameters.AddWithValue("@projectid", reader2.GetGuid(3));
                var listOfProjects = new List<Project>();  //trebace mi izvan scope-a while petlje, kako da inicijalizujem pre petlje (morao sam listu jer bi morao koristiti Project.Create f-ju koja ocekuje Client i Employee kao parametre, oni takodje moraju preko .Create metode)
                using var reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {
                    Enum.TryParse(reader3.GetString(16), out EStatusOfEmployee statusOfEmployee2);
                    Enum.TryParse(reader3.GetString(17), out ERole employeeRole2);
                    Enum.TryParse(reader3.GetString(5), out EStatusOfProject statusOfProject);
                    var client2 = Client.Create(reader3.GetGuid(1), reader3.GetString(6), reader3.GetString(7), reader3.GetString(8), reader3.GetString(9), reader3.GetString(10));
                    var employee2 = Employee.Create(reader3.GetGuid(3), reader3.GetString(11), reader3.GetString(12), reader3.GetString(13), reader3.GetString(14), reader3.GetFloat(15), statusOfEmployee2, employeeRole2);

                    listOfProjects.Add(Project.Create(reader3.GetGuid(0), client2.Value, employee2.Value, reader3.GetString(3), reader3.GetString(4), statusOfProject).Value);
                }
                var client = Client.Create(reader2.GetGuid(1), reader2.GetString(8), reader2.GetString(9), reader2.GetString(10), reader2.GetString(11), reader2.GetString(12));
                var category = Category.Create(reader2.GetGuid(2), reader2.GetString(13));

                var st = SingleTask.Create(reader2.GetGuid(0), client.Value, category.Value, listOfProjects.ElementAt(0), reader2.GetString(4), reader2.GetString(5), reader2.GetFloat(6), reader2.GetFloat(6));
                listOfSingleTasks.Add(st.Value);
            }

            Enum.TryParse(reader.GetString(16), out EStatusOfEmployee statusOfEmployee);
            Enum.TryParse(reader.GetString(17), out ERole employeeRole);
            var employee = Employee.Create(reader.GetGuid(1), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetFloat(15), statusOfEmployee, employeeRole);

            var workDay = WorkDay.Create(reader.GetGuid(0), employee.Value, reader.GetString(2), listOfSingleTasks);
            listOfWorkDays.Add(workDay.Value);

        }
        return listOfWorkDays;
    }

    public List<WorkDay> FindWorkingWeekByEmployeeIdAndDate(Guid employeeId, DateOnly date)
    {
        using SqlConnection con = new(strConString);
        con.Open();
        string query = "SELECT [WorkDays.WorkDayId], [WorkDays.EmployeeId], [WorkDays.Date], [SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId]," +
            "[SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours], [Employees.Name]," +
            " [Employees.Username], [Employees.Password], [Employees.Email], [Employees.HoursPerWeek], [Employees.Status], [Employees.Role]" +
            "FROM [WorkDays]" +
            "INNER JOIN SingleTasks ON SingleTasks.WorkDayId=WorkDays.WorkDayId INNER JOIN Employees ON WorkDays.EmployeeId=Employees.EmployeeId" +
            "WHERE [EmployeeId] = @employeeid AND [Date] BETWEEN @date1 AND date2";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@employeeid", employeeId);
        cmd.Parameters.AddWithValue("@date1", date);
        cmd.Parameters.AddWithValue("@date2", date.AddDays(7));

        var listOfSingleTasks = new List<SingleTask>();
        var listOfWorkDays = new List<WorkDay>();

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string query2 = "[SingleTasks.Id], [SingleTasks.ClientId], [SingleTasks.CategoryId], [SingleTasks.ProjectId], [SingleTasks.Date], [SingleTasks.Description], [SingleTasks.WorkingHours], [SingleTasks.OvertimeHours]" +
                "[Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Categories.CategoryName], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description], [Projects.Status]" +
                " FROM [SingleTasks] " +
                "INNER JOIN Clients ON SingleTasks.ClientId=Clients.ClientId " +
                "INNER JOIN Categories ON SingleTasks.CategoryId=Categories.CategoryId " +
                "WHERE [WorkDayId] = @workdayid";
            SqlCommand cmd2 = new(query2, con);
            cmd2.Parameters.AddWithValue("@workdayid", reader.GetGuid(1));

            using var reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                string query3 = "SELECT [Projects.ProjectId], [Projects.Client], [Projects.Leader], [Projects.ProjectName], [Projects.Description]," +
            " [Projects.Status], [Clients.Name], [Clients.Address], [Clients.City], [Clients.PostalCode], [Clients.Country], [Employee.Name]," +
            " [Employee.Username], [Employee.Password], [Employee.Email], [Employee.HoursPerWeek], [Employee.Status], [Employee.Role]" +
            " FROM [Projects] " +
            "INNER JOIN [Clients] ON [Clients.ClientId] = [Projects.Client]" +
            "INNER JOIN Employees ON [Employees.EmployeeId] = [Projects.Leader] WHERE [Projects.ProjectId] = @projectid";
                SqlCommand cmd3 = new(query, con);
                cmd3.Parameters.AddWithValue("@projectid", reader2.GetGuid(3));
                var listOfProjects = new List<Project>();  //trebace mi izvan scope-a while petlje, kako da inicijalizujem pre petlje (morao sam listu jer bi morao koristiti Project.Create f-ju koja ocekuje Client i Employee kao parametre, oni takodje moraju preko .Create metode)
                using var reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {
                    Enum.TryParse(reader3.GetString(16), out EStatusOfEmployee statusOfEmployee2);
                    Enum.TryParse(reader3.GetString(17), out ERole employeeRole2);
                    Enum.TryParse(reader3.GetString(5), out EStatusOfProject statusOfProject);
                    var client2 = Client.Create(reader3.GetGuid(1), reader3.GetString(6), reader3.GetString(7), reader3.GetString(8), reader3.GetString(9), reader3.GetString(10));
                    var employee2 = Employee.Create(reader3.GetGuid(3), reader3.GetString(11), reader3.GetString(12), reader3.GetString(13), reader3.GetString(14), reader3.GetFloat(15), statusOfEmployee2, employeeRole2);

                    listOfProjects.Add(Project.Create(reader3.GetGuid(0), client2.Value, employee2.Value, reader3.GetString(3), reader3.GetString(4), statusOfProject).Value);
                }
                var client = Client.Create(reader2.GetGuid(1), reader2.GetString(8), reader2.GetString(9), reader2.GetString(10), reader2.GetString(11), reader2.GetString(12));
                var category = Category.Create(reader2.GetGuid(2), reader2.GetString(13));

                var st = SingleTask.Create(reader2.GetGuid(0), client.Value, category.Value, listOfProjects.ElementAt(0), reader2.GetString(4), reader2.GetString(5), reader2.GetFloat(6), reader2.GetFloat(6));
                listOfSingleTasks.Add(st.Value);
            }

            Enum.TryParse(reader.GetString(16), out EStatusOfEmployee statusOfEmployee);
            Enum.TryParse(reader.GetString(17), out ERole employeeRole);
            var employee = Employee.Create(reader.GetGuid(1), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetFloat(15), statusOfEmployee, employeeRole);

            var workDay = WorkDay.Create(reader.GetGuid(0), employee.Value, reader.GetString(2), listOfSingleTasks);
            listOfWorkDays.Add(workDay.Value);

        }
        return listOfWorkDays;
    }
}
