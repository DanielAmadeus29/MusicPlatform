using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

public interface IUserRegistrationService
{
    void RegisterUser(string username, string password);
}

public class UserRegistration
{
    public void RegisterUser(string username, string password)
    {
        string connectionString = "Server=DANIEL\\SQLEXPRESS; Database=MusicPlatform; Trusted_Connection=True;";

        // Check the database connection first
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connected to the database successfully!");

                // Proceed with the registration logic if the connection is successful
                string query = @"
                IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
                BEGIN
                    PRINT 'Username already taken!';
                    RETURN;
                END
                INSERT INTO Users (Username, Password)
                VALUES (@Username, @Password);";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    // Execute the registration query
                    command.ExecuteNonQuery();
                    Console.WriteLine("User registered successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during registration: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
            }
        }
    }
}

