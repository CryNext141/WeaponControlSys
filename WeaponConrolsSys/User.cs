using Dapper;
using System.Data.SqlClient;
using WeaponControlsSys;
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

            string getUserQuery = "SELECT * FROM Users WHERE UserID = @UserId";
            var user = connection.QueryFirstOrDefault<User>(getUserQuery, new { UserId = userId });

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", "UserID", "First Name", "Last Name", "AgencyID", "Agency Name", "AccessLevelID");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", user.UserID, user.First_name, user.Last_name, user.AgencyID, user.AgencyName, user.AccessLevelID);

            Console.WriteLine(@"
            Enter the field you want to update:
            1. Username
            2. First Name
            3. Last Name
            4. Access Level ID
            5. Agency ID
            6. Agency Name
            7. Return to main menu
        ");

            int choice = Convert.ToInt32(Console.ReadLine());

            string fieldToUpdate = "";
            string newValue = "";

            switch (choice)
            {
                case 1:
                    fieldToUpdate = "Username";
                    Console.WriteLine("Enter the new username:");
                    newValue = Console.ReadLine();
                    break;
                case 2:
                    fieldToUpdate = "first_name";
                    Console.WriteLine("Enter the new first name:");
                    newValue = Console.ReadLine();
                    break;
                case 3:
                    fieldToUpdate = "last_name";
                    Console.WriteLine("Enter the new last name:");
                    newValue = Console.ReadLine();
                    break;
                case 4:
                    fieldToUpdate = "AccessLevelID";
                    Console.WriteLine("Enter the new access level ID:");
                    newValue = Console.ReadLine();
                    break;
                case 5:
                    fieldToUpdate = "AgencyID";
                    Console.WriteLine("Enter the new agency ID:");
                    newValue = Console.ReadLine();
                    break;
                case 6:
                    fieldToUpdate = "AgencyName";
                    Console.WriteLine("Enter the new agency name:");
                    newValue = Console.ReadLine();
                    break;
                case 7:
                    rnboUserClass.ReturnToMainMenu();
                    break;
                default:
                    Console.WriteLine("Choose correct action");
                    return;
            }

            string updateQuery = $"UPDATE Users SET {fieldToUpdate} = @NewValue WHERE UserID = @UserId";
            int rowsAffected = connection.Execute(updateQuery, new { NewValue = newValue, UserId = userId });

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