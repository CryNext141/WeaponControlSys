using System.Data.SqlClient;

namespace WeaponConrolsSys
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