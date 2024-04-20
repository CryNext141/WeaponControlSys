using Dapper;
using System.Data.SqlClient;
using WeaponControlsSys;
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

            string tableName = $"Weapons_Agency{agencyName}";
            string query = $"INSERT INTO {tableName} (WeaponName, AmmunitionAmount, AgencyID) VALUES (@WeaponName, @AmmunitionAmount, @AgencyID)";

            connection.Execute(query, new { WeaponName = weaponAgency.WeaponName, AmmunitionAmount = weaponAgency.AmmunitionAmount, AgencyID = weaponAgency.AgencyID });
        }
    }

    public static void DeleteWeaponFromAgency(string agencyName, string weaponName, int agencyId)
    {
        string tableName = $"Weapons_Agency{agencyName}";
        string query = $"DELETE FROM {tableName} WHERE WeaponName = @WeaponName AND AgencyID = @AgencyID";

        using (SqlConnection connection = DbConnector.GetConnection())
        {
            connection.Open();

            connection.Execute(query, new { WeaponName = weaponName, AgencyID = agencyId });
        }
    }
}