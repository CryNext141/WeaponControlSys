using Dapper;
using System.Data.SqlClient;


namespace WeaponControlsSys
{
    internal class rnboUserClass
    {
        static DbController DbConnector = new DbController();

        static string? choose, subChoose;

        static User user = new User();
        static UserService userService = new UserService();

        public static void ZeroLevelAccses()
            //testComment
        {
            user = userService.GetUserFromDatabase(Program.loginUsername);

            Console.Clear();
            Console.Write("     Welcome, ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(user.First_name + " " + user.Last_name);
            Console.ResetColor();
            Console.WriteLine($"! From {user.AgencyName.ToUpper()} with access level {user.AccessLevelID}");
            Console.WriteLine("Yo");

            Console.WriteLine(@"
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
                        HandleLawEnforcementMenu();
                        break;
                    case "2":
                        HandleOfficersMenu();
                        break;
                    case "3":
                        HandleWeaponsMenu();
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

            void HandleLawEnforcementMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
            1. View a list of all law enforcement agencies.
            2. Create a new law enforcement agency
            3. Disband a law enforcement agency
            4. Return to main menu");
                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewLawEnforcementList();

                        break;
                    case "2":
                        Console.Clear();
                        AddLawEnforcementAgency();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteAgencyAndUsers();
                        break;
                    case "4":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleOfficersMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
            1. View the list of all officers.
            2. Add a new officer to the system
            3. Delete an officer from the system
            4. Update information about officers
            5. Return to main menu");
                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewOfficersList();
                        break;
                    case "2":
                        Console.Clear();
                        AddNewUser();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteUser();
                        break;
                    case "4":
                        Console.Clear();
                        UserUpdateData();
                        break;
                    case "5":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleWeaponsMenu()
            {
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

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewWeaponsList();

                        break;
                    case "2":
                        Console.Clear();
                        UserAddWeapon();
                        break;
                    case "3":
                        Console.Clear();
                        UserDeleteWeapon();
                        break;
                    case "4":
                        Console.Clear();
                        UserAddWeaponToAgency();
                        break;
                    case "5":
                        Console.Clear();
                        UserDeleteWeaponFromAgency();
                        break;
                    case "6":
                        Console.Clear();
                        UserAddAmmunition();
                        break;
                    case "7":
                        Console.Clear();
                        ViewAmmoNumberInStorage();
                        break;
                    case "8":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }
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
                        HandleLawEnforcementSubMenu();
                        break;
                    case "2":
                        HandleOfficersSubMenu();
                        break;
                    case "3":
                        HandleWeaponsSubMenu();
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

            void HandleLawEnforcementSubMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
            1. View a list of all law enforcement agencies.
            2. Return to main menu");
                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewLawEnforcementList();
                        break;
                    case "2":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleOfficersSubMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
            1. View the list of all officers.
            2. Add a new officer to the system
            3. Delete an officer from the system
            4. Return to main menu");
                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewOfficersList();
                        break;
                    case "2":
                        Console.Clear();
                        AddNewUser();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteUser();
                        break;
                    case "4":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleWeaponsSubMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
            1. View the list of all weapons(several options)
            2. Adopt a new weapon into service
            3. Remove a weapon from service
            4. View the number of ammo in a warehouse
            5. Return to main menu");

                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewWeaponsList();
                        break;
                    case "2":
                        Console.Clear();
                        UserAddWeapon();
                        break;
                    case "3":
                        Console.Clear();
                        UserDeleteWeapon();
                        break;
                    case "4":
                        Console.Clear();
                        ViewAmmoNumberInStorage();
                        break;
                    case "5":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }
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
                        HandleViewLawEnforcementMenu();
                        break;
                    case "2":
                        HandleViewOfficersMenu();
                        break;
                    case "3":
                        HandleViewWeaponsMenu();
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

            void HandleViewLawEnforcementMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
    1. View a list of all law enforcement agencies.
    2. Return to main menu");
                Console.WriteLine("Enter your choice:");
                string subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewLawEnforcementList();
                        break;
                    case "2":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleViewOfficersMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
    1. View the list of all officers.
    2. Return to main menu");
                Console.WriteLine("Enter your choice:");
                string subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewOfficersList();
                        break;
                    case "2":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }

            void HandleViewWeaponsMenu()
            {
                Console.Clear();
                Console.WriteLine(@"
    1. View the list of all weapons(several options)
    2. View the number of ammo in a warehouse
    3. Return to main menu");

                Console.WriteLine("Enter your choice:");
                subChoose = Console.ReadLine();

                switch (subChoose)
                {
                    case "1":
                        Console.Clear();
                        ViewWeaponsList();
                        break;
                    case "2":
                        Console.Clear();
                        ViewAmmoNumberInStorage();
                        break;
                    case "3":
                        ReturnToMainMenu();
                        break;
                    default:
                        validChoice = false;
                        break;
                }
            }
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
                string choose = Console.ReadLine()?.ToLowerInvariant();

                switch (choose)
                {
                    case "y":
                        Console.Clear();
                        ZeroLevelAccses();
                        return;
                    case "n":
                        continue;
                    default:
                        Console.WriteLine("Invalid input. Please enter Y/N");
                        break;
                }
            }
        }

        public static void ViewLawEnforcementList()
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                var agencies = connection.Query("SELECT AgencyID, AgencyName FROM LawEnforcementAgencies");

                Console.WriteLine("{0,-10} {1,-30}", "AgencyID", "AgencyName");
                Console.WriteLine(new string('-', 40));

                foreach (var agency in agencies)
                {
                    Console.WriteLine("{0,-10} {1,-30}", agency.AgencyID, agency.AgencyName);
                }
            }

            ReturnToMainMenu();
        }


        public static void ViewOfficersList()
        {
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UserID, First_name, Last_name, AgencyID, AgencyName, AccessLevelID, RegistrationDate FROM Users", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-15} {5,-20} {6,-20}", "UserID", "First Name", "Last Name", "AgencyID", "Agency name", "AccessLevelID", "RegistrationDate");
                    Console.WriteLine(new string('-', 115));

                    while (reader.Read())
                    {
                        Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-20} {5,-15} {6,-20}", reader["UserID"], reader["First_name"], reader["Last_name"], reader["AgencyID"], reader["AgencyName"], reader["AccessLevelID"], reader["RegistrationDate"]);
                    }
                }
            }

            ReturnToMainMenu();
        }

        private static void ViewWeaponsList()
        {
            Console.WriteLine(@"
        1. View the weapons in service
        2. View weapons in a particular law enforcement agency");
            Console.WriteLine("Enter your choice:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGeneralWeapons();
                    break;
                case "2":
                    ViewWeaponsByAgency();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            ReturnToMainMenu();
        }

        private static void ViewGeneralWeapons()
        {
            Console.Clear();
            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();
                var weapons = connection.Query("SELECT * FROM GeneralWeapons");

                Console.WriteLine("{0,-10} {1,-20} {2,-10}", "WeaponID", "Weapon name", "Caliber");
                Console.WriteLine(new string('-', 40));

                foreach (var weapon in weapons)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-10}", weapon.WeaponID, weapon.WeaponName, weapon.Caliber);
                }
            }
        }

        private static void ViewWeaponsByAgency()
        {
            Console.Clear();
            Console.WriteLine("Enter Agency ID:");
            if (!int.TryParse(Console.ReadLine(), out int agencyID))
            {
                Console.WriteLine("Invalid Agency ID");
                return;
            }

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                var agencies = connection.Query($"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'Weapons_Agency%'");

                foreach (var agency in agencies)
                {
                    string tableName = agency.TABLE_NAME;
                    string query = $"SELECT * FROM {tableName} WHERE AgencyID = @AgencyID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AgencyID", agencyID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Weapon name: {reader["WeaponName"]}, Ammunition count: {reader["AmmunitionAmount"]}");
                            }
                        }
                    }
                }
            }
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

            ReturnToMainMenu();
        }

        public static void DeleteAgencyAndUsers()
        {
            Console.WriteLine("Enter agency ID:");
            if (!int.TryParse(Console.ReadLine(), out int agencyID))
            {
                Console.WriteLine("Invalid agency ID.");
                return;
            }

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (SqlTransaction sqlTran = connection.BeginTransaction())
                {
                    string? agencyName = connection.QueryFirstOrDefault<string>(
                        "SELECT AgencyName FROM LawEnforcementAgencies WHERE AgencyID = @AgencyID",
                        new { AgencyID = agencyID },
                        transaction: sqlTran
                    );
                    if (string.IsNullOrEmpty(agencyName))
                    {
                        Console.WriteLine("Agency not found.");
                        return;
                    }
                    connection.Execute(
                        "DELETE FROM Users WHERE AgencyID = @AgencyID",
                        new { AgencyID = agencyID },
                        transaction: sqlTran
                    );
                    connection.Execute(
                        $"DROP TABLE Weapons_Agency{agencyName}",
                        transaction: sqlTran
                    );
                    connection.Execute(
                        "DELETE FROM LawEnforcementAgencies WHERE AgencyID = @AgencyID",
                        new { AgencyID = agencyID },
                        transaction: sqlTran
                    );
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
            if (!int.TryParse(Console.ReadLine(), out int userID))
            {
                Console.WriteLine("Invalid User ID.");
                return;
            }

            using (SqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                int rowsAffected = connection.Execute("DELETE FROM Users WHERE UserID = @UserID", new { UserID = userID });

                if (rowsAffected == 0)
                {
                    Console.WriteLine("User not found.");
                    return;
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

                var ammoAmounts = connection.Query<int>("SELECT AmmunitionAmount FROM AmmunitionStorage");

                Console.WriteLine("{0,-10}", "AmmunitionAmount");
                Console.WriteLine(new string('-', 20));

                foreach (var amount in ammoAmounts)
                {
                    Console.WriteLine("{0,-10}", amount);
                }
            }
            ReturnToMainMenu();
        }
    }
}
