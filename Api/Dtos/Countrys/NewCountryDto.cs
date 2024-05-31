namespace Api.Dtos.Countrys
{
    public class NewCountryDto
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public ICollection<CountryObligationsDto> CountryObligationsDtos { get; set; }

        public NewCountryDto()
        {
            CountryObligationsDtos = new List<CountryObligationsDto>();
        }
    }
}
