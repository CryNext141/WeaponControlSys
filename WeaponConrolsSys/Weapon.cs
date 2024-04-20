using Dapper;
using System.Data.SqlClient;
using WeaponControlsSys;

public class Weapon
{
    static DbController DbConnector = new DbController();

    public int WeaponID { get; set; }
    public string? WeaponName { get; set; }
    public string? Caliber { get; set; }


    public static int AddWeapon(Weapon weapon)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            string query = "INSERT INTO GeneralWeapons (WeaponName, Caliber) VALUES (@WeaponName, @Caliber); SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int weaponID = connection.QueryFirstOrDefault<int>(query, weapon);

            return weaponID;
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
                IEnumerable<string> tables = connection.Query<string>(getTablesQuery, transaction: transaction);

                foreach (string tableName in tables)
                {
                    string deleteWeaponAgencyQuery = $"DELETE FROM {tableName} WHERE WeaponName IN (SELECT WeaponName FROM GeneralWeapons WHERE WeaponID = @WeaponID)";
                    connection.Execute(deleteWeaponAgencyQuery, new { WeaponID = weaponId }, transaction: transaction);
                }

                string deleteGeneralWeaponQuery = "DELETE FROM GeneralWeapons WHERE WeaponID = @WeaponID";
                connection.Execute(deleteGeneralWeaponQuery, new { WeaponID = weaponId }, transaction: transaction);

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