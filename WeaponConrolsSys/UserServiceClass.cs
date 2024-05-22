using Dapper;
using System.Data.SqlClient;

namespace WeaponControlsSys
{
    public class UserService
    {
        DbController DbConnector = new DbController();
        public void RegisterUser(string username, string password, string first_name, string last_name, int agencyID, int accessLevelID)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                string getAgencyNameQuery = "SELECT AgencyName FROM LawEnforcementAgencies WHERE AgencyID = @AgencyID";
                string? agencyName;

                using (SqlCommand getAgencyNameCommand = new SqlCommand(getAgencyNameQuery, connection))
                {
                    getAgencyNameCommand.Parameters.AddWithValue("@AgencyID", agencyID);
                    agencyName = getAgencyNameCommand.ExecuteScalar() as string;
                }

                if (string.IsNullOrEmpty(agencyName))
                {
                    Console.WriteLine("Agency not found.");
                    return;
                }

                using (SqlTransaction transaction = connection.BeginTransaction())
                {

                    string query = "INSERT INTO Users (Username, Password, First_name, Last_name, AgencyID, AgencyName, AccessLevelID, RegistrationDate) " +
                                   "VALUES (@Username, @Password, @First_name, @Last_name, @AgencyID, @AgencyName, @AccessLevelID, GETDATE())";

                    connection.Execute(query, new
                    {
                        Username = username,
                        Password = hashedPassword,
                        First_name = first_name,
                        Last_name = last_name,
                        AgencyID = agencyID,
                        AgencyName = agencyName,
                        AccessLevelID = accessLevelID
                    }, transaction: transaction);

                    transaction.Commit();
                }
            }
        }


        public bool LoginUser(string username, string password)
        {
            User user = GetUserFromDatabase(username);

            if (user == null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public User GetUserFromDatabase(string username)
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";
                return connection.QueryFirstOrDefault<User>(query, new { Username = username });
            }
        }
    }
}
