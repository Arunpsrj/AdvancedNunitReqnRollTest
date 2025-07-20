using AdvancedReqnRollTest.Models;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AdvancedReqnRollTest.Pages;

public class DbHelperPage
{
    private readonly string _connectionString; 
    
    public DbHelperPage(AppSettings settings)
    {
        _connectionString = settings.ConnectionStrings.DefaultConnection;

    }
    
    public bool InsertTempRecords(string tempid, string firstname, string lastname)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            var query = "INSERT INTO profile_temp (TempId, FirstName, LastName) VALUES (@TempId, @FirstName, @LastName)";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TempId", tempid);
                cmd.Parameters.AddWithValue("@FirstName", firstname);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}