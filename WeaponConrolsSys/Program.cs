namespace WeaponConrolsSys
{
    public class AccessControlService
    {
        private UserService userService = new UserService();

        public bool CanUserPerformAction(string username)
        {
            User user = userService.GetUserFromDatabase(username);

            if (user == null)
            {
                return false;
            }

            if (user.AgencyID != 0)
            {
                Console.Clear();
                Console.WriteLine("You are not a member of the RNBO, you are denied access to the system");
                return false;
            }

            switch (user.AgencyID)
            {
                case 0:
                    return HandleAccessLevel(user.AccessLevelID);
                case 1:
                    return HandleAccessLevel(user.AccessLevelID);
                case 2:
                    return HandleAccessLevel(user.AccessLevelID);
                case 3:
                    return HandleAccessLevel(user.AccessLevelID);
                default:
                    return false;
            }
        }

        private bool HandleAccessLevel(int accessLevelID)
        {
            switch (accessLevelID)
            {
                case 0:
                    rnboUserClass.ZeroLevelAccses();
                    return true;
                case 1:
                    rnboUserClass.FirstLevelAccses();
                    return true;
                case 2:
                    rnboUserClass.SecondLevelAccses();
                    return true;
                case 3:
                    rnboUserClass.ThirdLevelAccses();
                    return true;
                default:
                    return false;
            }
        }
    }


    public class Program
    {
        public static string? loginUsername;
        private static string? loginPassword;

        static void Main(string[] args)
        {
            UserLogin();
        }

        public static void UserLogin()
        {
            Console.WriteLine("Welcome to WeaponControlSys\n");

            UserService userService = new UserService();
            AccessControlService accessControlService = new AccessControlService();

            bool loginSuccessful;
            do
            {
                Console.WriteLine("Enter username:");
                loginUsername = Console.ReadLine();

                Console.WriteLine("Enter password:");
                loginPassword = Console.ReadLine();

                loginSuccessful = userService.LoginUser(loginUsername, loginPassword);
                if (loginSuccessful)
                {
                    accessControlService.CanUserPerformAction(loginUsername);
                }
                else
                {
                    Console.WriteLine("Access denied. Please try again");
                }
            } while (!loginSuccessful);
        }
    }
}
