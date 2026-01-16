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
using VegaIT.Timesheet.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace VegaIT.Timesheet.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string strConString = "Server=.;Database=TimesheetDB;Trusted_Connection=True;";

    public void Create(Client client)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "INSERT INTO [Clients] ([ClientId], [Name], [Address], [City], [PostalCode], [Country]) VALUES(@id, @name, @address, @city, @postalcode, @country)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@id", client.ClientId);
        cmd.Parameters.AddWithValue("@name", client.Name);
        cmd.Parameters.AddWithValue("@address", client.Address);
        cmd.Parameters.AddWithValue("@city", client.City);
        cmd.Parameters.AddWithValue("@postalcode", client.PostalCode);
        cmd.Parameters.AddWithValue("@country", client.Country);
        cmd.ExecuteNonQuery();
    }

    public void DeleteById(Guid id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "DELETE FROM [Clients] WHERE [ClientId] = @clientid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@clientid", id);
        cmd.ExecuteNonQuery();
    }

    public void Edit(Client client)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "UPDATE [Clients] SET [Name]=@name, [Address]=@address, [City]=@city, [PostalCode]=@pc, [Country]=@country WHERE [ClientId]=@clientid";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@clientid", client.ClientId);
        cmd.Parameters.AddWithValue("@name", client.Name);
        cmd.Parameters.AddWithValue("@address", client.Address);
        cmd.Parameters.AddWithValue("@city", client.City);
        cmd.Parameters.AddWithValue("@pc", client.PostalCode);
        cmd.Parameters.AddWithValue("@country", client.Country);
        cmd.ExecuteNonQuery();
    }

    // da li Result.Value daje objekat koji nam treba, Da li sa 0 dohvatamo prvu kolonu, da li koristiti skraceni using, return unutar konekcije?...

    public List<Client> FindAll()
    {
        var listOfClients = new List<Client>();

        using SqlConnection con = new(strConString);
        con.Open();
        string query = "SELECT [ClientId], [Name], [Address], [City], [PostalCode], [Country] FROM [Clients]";
        SqlCommand cmd = new(query, con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var client = Client.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
            listOfClients.Add(client.Value);
        }

        return listOfClients;
    }

    public Maybe<Client> FindById(string id)
    {
        using SqlConnection con = new(strConString);

        con.Open();
        string query = "SELECT [ClientId], [Name], [Address], [City], [PostalCode], [Country] FROM [Clients] WHERE ([ClientId]=@clientid)";
        SqlCommand cmd = new(query, con);
        cmd.Parameters.AddWithValue("@clientid", id);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var client = Client.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
            return Maybe.From(client.Value);
        }
        return Maybe.None;
    }

    public PageOf<Client> FindPageByFirstLetterAndNameSubstring(string firstLetter, string nameSubstring, int pageSize, int pageNumber)
    {
        var listOfClients = new List<Client>();
        var totalPages = 0;

        // Ovako mora jer query ne prihvata drugacije
        string prvi = firstLetter + "%";
        string drugi = "%" + nameSubstring + "%";

        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT [ClientId], [Name], [Address], [City], [PostalCode], [Country]" +
                " FROM [Clients] WHERE ([Name] LIKE @firstletter) AND ([Name] LIKE @name) ORDER BY ClientId OFFSET (@pagesize * (@pagenumber - 1)) ROWS FETCH NEXT @pagesize ROWS ONLY";

            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", prvi);
            cmd.Parameters.AddWithValue("@name", drugi);
            cmd.Parameters.AddWithValue("@pagesize", pageSize);
            cmd.Parameters.AddWithValue("@pagenumber", pageNumber);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var client = Client.Create(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                listOfClients.Add(client.Value);
            }
        }
        using (SqlConnection con = new(strConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) as rowsum FROM [Clients] WHERE ([Name] LIKE @firstletter) AND ([Name] LIKE @name)";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@firstletter", prvi);
            cmd.Parameters.AddWithValue("@name", drugi);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalPages = reader.GetInt32(0);
            }
        }
        totalPages = totalPages / pageSize;
        if (totalPages < 1) { totalPages = 1; }
        return new PageOf<Client>(totalPages, listOfClients);



    }
}
