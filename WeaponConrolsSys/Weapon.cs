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
}