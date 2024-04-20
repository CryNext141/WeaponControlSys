using System.Data.SqlClient;
using WeaponConrolsSys;

public class Weapon
{
    static DbController DbConnector = new DbController();

    public int WeaponID { get; set; }
    public string? WeaponName { get; set; }
    public string? Caliber { get; set; }


    public static void AddWeapon(Weapon weapon)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "INSERT INTO GeneralWeapons (WeaponName, Caliber) VALUES (@WeaponName, @Caliber); SELECT SCOPE_IDENTITY();";
                command.Parameters.AddWithValue("@WeaponName", weapon.WeaponName);
                command.Parameters.AddWithValue("@Caliber", weapon.Caliber);

                command.ExecuteScalar();
            }
        }
    }

    public static void DeleteWeapon(int weaponId)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                string getTablesQuery = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'Weapons_Agency%'";

                List<string> tables = new List<string>();
                using (SqlCommand command = new SqlCommand(getTablesQuery, connection, transaction))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }
                }

                foreach (string tableName in tables)
                {
                    string deleteWeaponAgencyQuery = $"DELETE FROM {tableName} WHERE WeaponName IN (SELECT WeaponName FROM GeneralWeapons WHERE WeaponID = @WeaponID)";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteWeaponAgencyQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@WeaponID", weaponId);
                        deleteCommand.ExecuteNonQuery();
                    }
                }

                string deleteGeneralWeaponQuery = "DELETE FROM GeneralWeapons WHERE WeaponID = @WeaponID";

                using (SqlCommand command = new SqlCommand(deleteGeneralWeaponQuery, connection, transaction))
                {
                    command.Parameters.AddWithValue("@WeaponID", weaponId);
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }
    }

    public static void AddAmmunition(string agencyName, int agencyId, string weaponName, int ammunitionAmount)
    {
        string agencyTableName = "Weapons_Agency" + agencyName;

        int currentAmmunition = GetCurrentAmmunition(agencyTableName, agencyId, weaponName);

        SubtractAmmunition(ammunitionAmount);
        AddAmmunitionToAgency(agencyTableName, agencyId, weaponName, ammunitionAmount);

        int newAmmunition = GetCurrentAmmunition(agencyTableName, agencyId, weaponName);

        Console.WriteLine($@"
        --------------------------------------------------      
        Weapon: {weaponName}
        --------------------------------------------------
        Ammunition before: {currentAmmunition}
        Ammunition after: {newAmmunition}
        --------------------------------------------------");
    }

    public static int GetCurrentAmmunition(string agencyTableName, int agencyId, string weaponName)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            string getCurrentSql = $"SELECT AmmunitionAmount FROM {agencyTableName} WHERE AgencyID = @agencyId AND WeaponName = @weaponName";
            using (SqlCommand command = new SqlCommand(getCurrentSql, connection))
            {
                command.Parameters.AddWithValue("@agencyId", agencyId);
                command.Parameters.AddWithValue("@weaponName", weaponName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
        }
        return 0;
    }

    public static void SubtractAmmunition(int ammunitionAmount)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            string subtractSql = "UPDATE AmmunitionStorage SET AmmunitionAmount = AmmunitionAmount - @ammunitionAmount WHERE AmmunitionAmount >= @ammunitionAmount";
            using (SqlCommand command = new SqlCommand(subtractSql, connection))
            {
                command.Parameters.AddWithValue("@ammunitionAmount", ammunitionAmount);
                command.ExecuteNonQuery();
            }
        }
    }

    public static void AddAmmunitionToAgency(string agencyTableName, int agencyId, string weaponName, int ammunitionAmount)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            string addSql = $"UPDATE {agencyTableName} SET AmmunitionAmount = AmmunitionAmount + @ammunitionAmount WHERE AgencyID = @agencyId AND WeaponName = @weaponName";
            using (SqlCommand command = new SqlCommand(addSql, connection))
            {
                command.Parameters.AddWithValue("@ammunitionAmount", ammunitionAmount);
                command.Parameters.AddWithValue("@agencyId", agencyId);
                command.Parameters.AddWithValue("@weaponName", weaponName);
                command.ExecuteNonQuery();
            }
        }
    }
}