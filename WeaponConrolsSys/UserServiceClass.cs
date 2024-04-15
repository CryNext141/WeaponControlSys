using System.Data.SqlClient;

namespace WeaponConrolsSys
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
                string agencyName;

                using (SqlCommand getAgencyNameCommand = new SqlCommand(getAgencyNameQuery, connection))
                {
                    getAgencyNameCommand.Parameters.AddWithValue("@AgencyID", agencyID);
                    agencyName = (string)getAgencyNameCommand.ExecuteScalar();
                }

                string query = "INSERT INTO Users (Username, Password, First_name, Last_name, AgencyID, AgencyName, AccessLevelID) VALUES (@Username, @Password, @First_name, @Last_name, @AgencyID, @AgencyName, @AccessLevelID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@First_name", first_name);
                    command.Parameters.AddWithValue("@Last_name", last_name);
                    command.Parameters.AddWithValue("@AgencyID", agencyID);
                    command.Parameters.AddWithValue("@AgencyName", agencyName);
                    command.Parameters.AddWithValue("@AccessLevelID", accessLevelID);

                    command.ExecuteNonQuery();
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
            User? user = null;

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                First_name = reader["First_name"].ToString(),
                                Last_name = reader["Last_name"].ToString(),
                                AgencyID = Convert.ToInt32(reader["AgencyID"]),
                                AgencyName = reader["AgencyName"].ToString(),
                                AccessLevelID = Convert.ToInt32(reader["AccessLevelID"])
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}
