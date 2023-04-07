using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Common
{
    public class CustomSqlConnection
    {
        public static SqlConnection Create(IConfiguration configuration) {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
