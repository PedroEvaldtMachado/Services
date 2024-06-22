namespace Api.Dtos.Countrys
{
    public class CountryDto : BaseDto
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public ICollection<CountryObligationsDto> CountryObligations { get; set; }

        public CountryDto()
        {
            CountryObligations = new List<CountryObligationsDto>();
        }
    }
}
