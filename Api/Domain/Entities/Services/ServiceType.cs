namespace Api.Domain.Entities.Services
{
    public class ServiceType : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }
    }
}
