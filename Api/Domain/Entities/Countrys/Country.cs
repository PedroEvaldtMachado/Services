namespace Api.Domain.Entities.Countrys
{
    public class Country : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public ICollection<CountryObligations> CountryObligations { get; set; }

        public Country()
        {
            CountryObligations = new List<CountryObligations>();
        }
    }
}
