using Npgsql;

namespace AniPlannerApi.Data.Helpers;

public class ConnectionStringBuilder
{
    public static bool Initialised = false;
    private static IConfiguration _configuration;

    public static void Initialise(IConfiguration configuration)
    {
        _configuration = configuration;
        Initialised = true;
    }

    public static string Build(string dataBase)
    {
        if (_configuration == null)
            throw new Exception("Intialize Connection String Builder static class in project startup\n" +
                                "Use:  ConnectionStringBuilder.Initialse(Configuration);");

        var connectionString = _configuration.GetConnectionString($"{dataBase}Connection");
        var str = new NpgsqlConnectionStringBuilder(connectionString);
        //https://stackoverflow.com/questions/54767718/iconfiguration-does-not-contain-a-definition-for-getvalue
       
        str.Username = _configuration[$"DatabaseConnectionDetails:{dataBase}UserID"];  /*Encryption.DecryptData(_configuration.GetValue<string>($"AppSettings:Security:{dataBase}UserID"));*/
        str.Password = _configuration[$"DatabaseConnectionDetails:{dataBase}Password"]; //Encryption.DecryptData(_configuration.GetValue<string>($"AppSettings:Security:{dataBase}Password"));
        
        return str.ToString();
    }
}
