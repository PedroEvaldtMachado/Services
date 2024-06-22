using Api.Infra.Enums;

namespace Api.Dtos
{
    public class AppSettings
    {
        public Connection ConnectionDatabase { get; set; } = new Connection();
    }

    public class Connection
    {
        public string? ConnectionStrings { get; set; }
        public string? DatabaseName { get; set; }
        public DataBaseType DataBaseType { get; set; }
    }
}
