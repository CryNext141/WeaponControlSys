using System.Data.SqlClient;
using WeaponConrolsSys;
public class Weapon_Agency
{
    static DbController DbConnector = new DbController();

    public string? WeaponName { get; set; }
    public int AmmunitionAmount { get; set; }
    public int AgencyID { get; set; }


    public static void AddWeaponToAgency(Weapon_Agency weaponAgency, string agencyName)
    {
        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Weapons_Agency{agencyName} (WeaponName, AmmunitionAmount, AgencyID) VALUES (@WeaponName, @AmmunitionAmount, @AgencyID)";
                command.Parameters.AddWithValue("@WeaponName", weaponAgency.WeaponName);
                command.Parameters.AddWithValue("@AmmunitionAmount", weaponAgency.AmmunitionAmount);
                command.Parameters.AddWithValue("@AgencyID", weaponAgency.AgencyID);

                command.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteWeaponFromAgency(string agencyName, string weaponName, int agencyId)
    {
        string tableName = "Weapons_Agency" + agencyName;
        string query = $"DELETE FROM {tableName} WHERE WeaponName = @weaponName AND AgencyID = @agencyId";

        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@weaponName", weaponName);
                command.Parameters.AddWithValue("@agencyId", agencyId);
                command.ExecuteNonQuery();
            }
        }
    }
}