namespace DbCore.Settings
{
    public class PostgresSettings
    {
        public string? Host { get; set; }
        public string? DataBase { get; set; }

        public int? Port { get; set; }

        public string? User { get; set; }

        public string? Password { get; set; }

        public string ConnectionString
        {
            get { return $"Host={Host};Port={Port};Database={DataBase};Username={User};Password={Password}"; }
        }
    }
}
