using CSharpFunctionalExtensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace VegaIT.Timesheet.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly string strConString = "Server=.;Database=TimesheetDB;Trusted_Connection=True;";

    public void Create(Category category)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "INSERT INTO [Categories] ([CategoryId], [CategoryName]) VALUES(@id, @name)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@id", category.CategoryId);
        cmd.Parameters.AddWithValue("@name", category.CategoryName);
        cmd.ExecuteNonQuery();
    }

    public void DeleteById(Guid id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "DELETE FROM [Categories] WHERE ([CategoryId] = @categoryid)";
        SqlCommand cmd = new(query, con);
        System.Console.WriteLine(id);
        cmd.Parameters.AddWithValue("@categoryid", id);
        cmd.ExecuteNonQuery();
    }

    public void Edit(Category category)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "UPDATE [Categories] SET [CategoryName]=@categoryname WHERE ([CategoryId]=@categoryid)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);
        cmd.Parameters.AddWithValue("@categoryid", category.CategoryId);
        cmd.ExecuteNonQuery();
    }

    public Maybe<Category> FindById(string id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "SELECT [CategoryId], [CategoryName] FROM [Categories] WHERE ([CategoryId]=@categoryid)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@categoryid", id);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var category = Category.Create(reader.GetGuid(0), reader.GetString(1));
            return Maybe.From(category.Value);
        }
        return Maybe.None;
    }

    public PageOf<Category> FindPageByFirstLetterAndNameSubstring(string firstLetter, string name, int pageSize, int pageNumber)
    {
        var listOfCategories = new List<Category>();
        var totalPages = 0;

        // Ovako mora jer query ne prihvata drugacije
        string prvi = firstLetter + "%";
        string drugi = "%" + name + "%";

        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT [CategoryId], [CategoryName] FROM [Categories]" +
                " WHERE ([CategoryName]  LIKE @firstletter) AND ([CategoryName] LIKE @name) ORDER BY CategoryId OFFSET (@pagesize * (@pagenumber - 1)) ROWS FETCH NEXT @pagesize ROWS ONLY";


            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", prvi);
            cmd.Parameters.AddWithValue("@name", drugi);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var category = Category.Create(reader.GetGuid(0), reader.GetString(1));
                listOfCategories.Add(category.Value);
            }
        }
        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) as rowsum FROM [Categories] WHERE ([CategoryName]  LIKE @firstletter)  AND ([CategoryName] LIKE @name)";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", prvi);
            cmd.Parameters.AddWithValue("@name", drugi);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalPages = reader.GetInt32(0);
            }
        }
        totalPages = totalPages / pageSize;
        if(totalPages < 1) { totalPages = 1; }
        return new PageOf<Category>(totalPages, listOfCategories);



    }
}
