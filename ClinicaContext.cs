using MySqlConnector;

namespace ClinicaWeb.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection Create() => new MySqlConnection(_connectionString);
}
