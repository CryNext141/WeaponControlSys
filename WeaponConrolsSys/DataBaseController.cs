using System.Data.SqlClient;

namespace WeaponControlsSys
{
    class DbController
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=WeaponControls;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}