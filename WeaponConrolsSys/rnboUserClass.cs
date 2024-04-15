using System.Data.SqlClient;

namespace WeaponConrolsSys
{
    internal class rnboUserClass
    {
        static DbController DbConnector = new DbController();

        static string? choose;
        static string? subChoose;

        static User user = new User();
        static UserService userService = new UserService();

        public static void ZeroLevelAccses()
        {
            user = userService.GetUserFromDatabase(Program.loginUsername);

            Console.Clear();
            Console.WriteLine($@"
                Welcome, {user.First_name + " " + user.Last_name}! From {user.AgencyName.ToUpper()} with access level {user.AccessLevelID}

            ========================================
                You have the following options:
                1. Law enforcement organizations:
                2. Officers
                3. Weapons
                0. Exit
            ========================================");

            bool validChoice;
            do
            {
                validChoice = true;
                Console.WriteLine("\nEnter your choice:");
                choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View a list of all law enforcement agencies.
                2. Create a new law enforcement agency
                3. Disband a law enforcement agency
                4. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewLawEnforcementList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            AddLawEnforcementAgency();
                        }
                        else if (subChoose == "3")
                        {
                            Console.Clear();
                            DeleteAgencyAndUsers();
                        }

                        else if (subChoose == "4")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all officers.
                2. Add a new officer to the system
                3. Delete an officer from the system
                4. Update information about officers
                5. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewOfficersList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            AddNewUser();
                        }
                        else if (subChoose == "3")
                        {
                            Console.Clear();
                            DeleteUser();
                        }
                        else if (subChoose == "4")
                        {
                            Console.Clear();
                           UserUpdateData();
                        }
                        else if (subChoose == "5")
                        {
                            ReturnToMainMenu();
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all weapons(several options)
                2. Adopt a new weapon into service
                3. Remove a weapon from service
                4. Add a new type of weapon for a law enforcement agency
                5. Remove a certain type of weapon from a law enforcement agency
                6. Restore ammunition to a certain security agency
                7. View the number of ammo in a warehouse
                8. Return to main menu");

                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewWeponsList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            UserAddWeapon();
                        }
                        else if (subChoose == "3")
                        {
                            Console.Clear();
                            UserDeleteWeapon();
                        }

                        else if (subChoose == "4")
                        {
                            Console.Clear();
                            UserAddWeaponToAgency();
                        }
                        else if (subChoose == "5")
                        {
                            Console.Clear();
                            UserDeleteWeaponFromAgency();
                        }
                        else if (subChoose == "6")
                        {
                            Console.Clear();
                            UserAddAmmunition();
                        }
                        else if (subChoose == "7")
                        {
                            Console.Clear();
                            ViewAmmoNumberInStorage();
                        }

                        else if (subChoose == "8")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "0":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            } while (!validChoice);
        }

        public static void FirstLevelAccses()
        {
            user = userService.GetUserFromDatabase(Program.loginUsername);

            Console.Clear();
            Console.WriteLine($@"
                Welcome, {user.First_name + " " + user.Last_name}! From {user.AgencyName.ToUpper()} with access level {user.AccessLevelID}

            ========================================
                You have the following options:
                1. Law enforcement organizations:
                2. Officers
                3. Weapons
                0. Exit
            ========================================");

            bool validChoice;
            do
            {
                validChoice = true;
                Console.WriteLine("\nEnter your choice:");
                choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View a list of all law enforcement agencies.
                2. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewLawEnforcementList();
                        }
                        else if (subChoose == "2")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all officers.
                2. Add a new officer to the system
                3. Delete an officer from the system
                4. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewOfficersList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            AddNewUser();
                        }
                        else if (subChoose == "3")
                        {
                            Console.Clear();
                            DeleteUser();
                        }
                        else if (subChoose == "4")
                        {
                            ReturnToMainMenu();
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all weapons(several options)
                2. Adopt a new weapon into service
                3. Remove a weapon from service
                4. View the number of ammo in a warehouse
                5. Return to main menu");

                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewWeponsList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            UserAddWeapon();
                        }
                        else if (subChoose == "4")
                        {
                            Console.Clear();
                            ViewAmmoNumberInStorage();
                        }
                        else if (subChoose == "5")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "0":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            } while (!validChoice);
        }



        public static void SecondLevelAccses()
        {
            user = userService.GetUserFromDatabase(Program.loginUsername);

            Console.Clear();
            Console.WriteLine($@"
                Welcome, {user.First_name + " " + user.Last_name}! From {user.AgencyName.ToUpper()} with access level {user.AccessLevelID}

            ========================================
                You have the following options:
                1. Law enforcement organizations:
                2. Officers
                3. Weapons
                0. Exit
            ========================================");

            bool validChoice;
            do
            {
                validChoice = true;
                Console.WriteLine("\nEnter your choice:");
                choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View a list of all law enforcement agencies.
                2. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewLawEnforcementList();
                        }
                        else if (subChoose == "2")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all officers.
                2. Return to main menu");
                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewOfficersList();
                        }
                        else if (subChoose == "2")
                        {
                            ReturnToMainMenu();
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(@"
                1. View the list of all weapons(several options)
                2. View the number of ammo in a warehouse
                3. Return to main menu
                ");

                        Console.WriteLine("Enter your choice:");
                        subChoose = Console.ReadLine();

                        if (subChoose == "1")
                        {
                            Console.Clear();
                            ViewWeponsList();
                        }
                        else if (subChoose == "2")
                        {
                            Console.Clear();
                            ViewAmmoNumberInStorage();
                        }
                        else if (subChoose == "3")
                        {
                            ReturnToMainMenu();
                        }

                        break;
                    case "0":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            } while (!validChoice);
        }

        public static void ThirdLevelAccses()
        {
            user = userService.GetUserFromDatabase(Program.loginUsername);

            Console.Clear();
            Console.WriteLine($@"
                Welcome, {user.First_name + " " + user.Last_name}! From {user.AgencyName.ToUpper()} with access level {user.AccessLevelID}

            ========================================
                You have the following options:
                Your permission level is not high enough, you do not have any rights in the system
                0. Exit
            ========================================");




            bool validChoice;
            do
            {
                validChoice = true;
                Console.WriteLine("\nEnter your choice:");
                choose = Console.ReadLine();

                switch (choose)
                {
                    case "0":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            } while (!validChoice);
        }

        public static void ReturnToMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nBack to action list menu? Y/N");
                choose = Console.ReadLine();

                if (choose == "y".ToLower())
                {
                    Console.Clear();
                    ZeroLevelAccses();
                    break;
                }
                else if (choose == "n".ToLower())
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y/N");
                }
            }
        }
        public static void ViewLawEnforcementList()
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM LawEnforcementAgencies", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("{0,-10} {1,-30}", "AgencyID", "AgencyName");
                        Console.WriteLine(new string('-', 40));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-10} {1,-30}", reader["AgencyID"], reader["AgencyName"]);
                        }
                    }
                }
            }

            ReturnToMainMenu();
        }

        public static void ViewOfficersList()
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UserID, First_name, Last_name, AgencyID, AgencyName, AccessLevelID FROM Users", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", "UserID", "First Name", "Last Name", "AgencyID", "Agency name", "AccessLevelID");
                        Console.WriteLine(new string('-', 95));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-30} {5,-15}", reader["UserID"], reader["First_name"], reader["Last_name"], reader["AgencyID"], reader["AgencyName"], reader["AccessLevelID"]);
                        }
                    }
                }
            }

            ReturnToMainMenu();
        }

        public static void ViewWeponsList()
        {
            Console.WriteLine(@"
                1.View the weapons in service
                2.View weapons in a particular law enforcement agency");
            Console.WriteLine("Enter your choice:");
            choose = Console.ReadLine();
            switch (choose)
            {
                case "1":

                    Console.Clear();
                    using (SqlConnection connection = DbConnector.GetConnection())
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("SELECT * FROM GeneralWeapons", connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                Console.WriteLine("{0,-10} {1,-20} {2,-10}", "WeaponID", "Weapon name", "Caliber");
                                Console.WriteLine(new string('-', 40));

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0,-10} {1,-20} {2,-10}", reader["WeaponID"], reader["WeaponName"], reader["Caliber"]);
                                }

                                reader.Close();
                            }
                        }
                    }
                    break;
                case "2":

                    Console.Clear();
                    Console.WriteLine("Enter Agency ID:");
                    int agencyID = Convert.ToInt32(Console.ReadLine());

                    using (SqlConnection connection = DbConnector.GetConnection())
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'Weapons_Agency%'", connection))

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader.GetString(0);
                                string query = $"SELECT * FROM {tableName} WHERE AgencyID = @AgencyID";

                                using (SqlConnection innerConnection = DbConnector.GetConnection())
                                {
                                    innerConnection.Open();
                                    using (SqlCommand innerCommand = new SqlCommand(query, innerConnection))
                                    {
                                        innerCommand.Parameters.AddWithValue("@AgencyID", agencyID);
                                        using (SqlDataReader innerReader = innerCommand.ExecuteReader())
                                        {
                                            while (innerReader.Read())
                                            {
                                                Console.WriteLine($"Weapon name: {innerReader["WeaponName"]}, Ammunition count: {innerReader["AmmunitionAmount"]}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            ReturnToMainMenu();
        }

        public static void AddLawEnforcementAgency()
        {
            Console.WriteLine("Enter Agency Name:");
            string? agencyName = Console.ReadLine();
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO LawEnforcementAgencies (AgencyName) VALUES (@AgencyName)";
                    command.Parameters.AddWithValue("@AgencyName", agencyName);

                    command.ExecuteNonQuery();

                    CreateWeaponsAgencyTable(agencyName);
                }
            }

            ReturnToMainMenu();
        }


        public static void CreateWeaponsAgencyTable(string agencyName)
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"CREATE TABLE Weapons_Agency{agencyName} (" +
                                          "WeaponName varchar(255), " +
                                          "AmmunitionAmount int, " +
                                          "AgencyID int, " +
                                          "FOREIGN KEY (WeaponName) REFERENCES GeneralWeapons(WeaponName), " +
                                          "FOREIGN KEY (AgencyID) REFERENCES LawEnforcementAgencies(AgencyID)" +
                                          ")";

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteAgencyAndUsers()
        {
            Console.WriteLine("Enter agency ID:");
            string? agencyID = Console.ReadLine();

            using (SqlConnection connection = DbConnector.GetConnection())
            {

                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand("", connection, sqlTran))
                {

                    command.CommandText =
                    "SELECT AgencyName FROM LawEnforcementAgencies WHERE AgencyID = @AgencyID";
                    command.Parameters.Add(new SqlParameter("@AgencyID", agencyID));
                    string agencyName = (string)command.ExecuteScalar();

                    command.CommandText =
                   "DELETE FROM Users WHERE AgencyID = @AgencyID";
                    command.ExecuteNonQuery();

                    command.CommandText =
                    $"DROP TABLE Weapons_Agency{agencyName}";
                    command.ExecuteNonQuery();

                    command.CommandText =
                    "DELETE FROM LawEnforcementAgencies WHERE AgencyID = @AgencyID";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();
                }
            }
            ReturnToMainMenu();
        }

        public static void AddNewUser()
        {
            UserService userService = new UserService();

            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter first name:");
            string first_name = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string last_name = Console.ReadLine();


            Console.WriteLine("Enter ID of departament:");
            int agencyID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter ID level accsess:");
            int accessLevelID = Convert.ToInt32(Console.ReadLine());

            userService.RegisterUser(username, password, first_name, last_name, agencyID, accessLevelID);

            ReturnToMainMenu();
        }

        public static void DeleteUser()
        {
            Console.WriteLine("Enter User ID:");
            string? userID = Console.ReadLine();

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE UserID = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);

                    command.ExecuteNonQuery();
                }
            }

            ReturnToMainMenu();
        }

        public static void UserAddWeapon()
        {
            Console.WriteLine("Enter Weapon Name: ");
            string weaponName = Console.ReadLine();

            Console.WriteLine("Enter Caliber: ");
            string caliber = Console.ReadLine();

            Weapon newWeapon = new Weapon { WeaponName = weaponName, Caliber = caliber };
            Weapon.AddWeapon(newWeapon);

            ReturnToMainMenu();
        }


        public static void UserDeleteWeapon()
        {
            Console.WriteLine("Enter weapon ID:");
            int weaponId = Convert.ToInt32(Console.ReadLine());

            Weapon.DeleteWeapon(weaponId);

            ReturnToMainMenu();
        }

        public static void UserAddWeaponToAgency()
        {
            Console.WriteLine("Enter Weapon Name: ");
            string weaponName = Console.ReadLine();

            Console.WriteLine("Enter Ammunition Amount: ");
            int ammunitionAmount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Agency Name: ");
            string agencyName = Console.ReadLine();

            Console.WriteLine("Enter Agency ID: ");
            int agencyId = Convert.ToInt32(Console.ReadLine());

            Weapon_Agency newWeaponAgency = new Weapon_Agency { WeaponName = weaponName, AmmunitionAmount = ammunitionAmount, AgencyID = agencyId };
            Weapon_Agency.AddWeaponToAgency(newWeaponAgency, agencyName);

            ReturnToMainMenu();
        }


        public static void UserDeleteWeaponFromAgency()
        {
            Console.WriteLine("Enter Agency Name:");
            string agencyName = Console.ReadLine();

            Console.WriteLine("Enter Weapon Name:");
            string weaponName = Console.ReadLine();

            Console.WriteLine("Enter Agency ID:");
            int agencyId = int.Parse(Console.ReadLine());

            Weapon_Agency.DeleteWeaponFromAgency(agencyName, weaponName, agencyId);

            ReturnToMainMenu();
        }

        public static void UserUpdateData()
        {
            Console.WriteLine("Enter User ID:");
            int userID = Convert.ToInt32(Console.ReadLine());

            User.UpdateUser(userID);

            ReturnToMainMenu();
        }

        public static void UserAddAmmunition()
        {
            Console.WriteLine("Enter Agency Name:");
            string agencyName = Console.ReadLine();

            Console.WriteLine("Enter Agency ID:");
            int agencyId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Weapon Mame:");
            string weaponName = Console.ReadLine();

            Console.WriteLine("Enter ammunition count:");
            int ammunition = int.Parse(Console.ReadLine());

            Weapon.AddAmmunition(agencyName, agencyId, weaponName, ammunition);

            ReturnToMainMenu();
        }

        public static void ViewAmmoNumberInStorage()
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AmmunitionStorage", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("{0,-10}", "AmmunitionAmount");
                        Console.WriteLine(new string('-', 20));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-10}", reader["AmmunitionAmount"]);
                        }
                    }
                }
            }

            ReturnToMainMenu();
        }
    }
}
