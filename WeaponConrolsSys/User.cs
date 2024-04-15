using System.Data.SqlClient;
using WeaponConrolsSys;
public class User
{
    static DbController DbConnector = new DbController();

    public int UserID { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? First_name { get; set; }
    public string? Last_name { get; set; }
    public string? AgencyName { get; set; }
    public int AccessLevelID { get; set; }
    public int AgencyID { get; set; }


    public static void UpdateUser(int userId)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();


            string query = "SELECT * FROM Users WHERE UserID = @UserId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", "UserID", "First Name", "Last Name", "AgencyID", "Agency Name", "AccessLevelID");
                Console.WriteLine(new string('-', 100));

                while (reader.Read())
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", reader["UserID"], reader["first_name"], reader["last_name"], reader["AgencyID"], reader["AgencyName"], reader["AccessLevelID"]);
                }
            }

            Console.WriteLine(@"
                Enter the field you want to update:
                1. Username
                2. first_name
                3. last_name
                4. AccessLevelID
                5. AgencyID
                6. AgencyName
                7. Return to main menu
");

            int choice = Convert.ToInt32(Console.ReadLine());

            string fieldToUpdate = "";
            switch (choice)
            {
                case 1:
                    fieldToUpdate = "Username";
                    break;
                case 2:
                    fieldToUpdate = "first_name";
                    break;
                case 3:
                    fieldToUpdate = "last_name";
                    break;
                case 4:
                    fieldToUpdate = "AccessLevelID";
                    break;
                case 5:
                    fieldToUpdate = "AgencyID";
                    break;
                case 6:
                    fieldToUpdate = "AgencyName";
                    break;
                case 7:
                    rnboUserClass.ReturnToMainMenu();
                    break;
                default:
                    Console.WriteLine("Choose corect action");
                    return;
            }

            Console.WriteLine("Enter the new value:");
            string newValue = Console.ReadLine();

            query = $"UPDATE Users SET {fieldToUpdate} = @NewValue WHERE UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewValue", newValue);
            command.Parameters.AddWithValue("@UserId", userId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("User data updated successfully");
            }
            else
            {
                Console.WriteLine("User data update failed");
            }
        }
    }
}