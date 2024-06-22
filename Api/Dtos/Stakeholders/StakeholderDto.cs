namespace Api.Dtos.Stakeholders
{
    public abstract class StakeholderDto : BaseDto
    {
        public long PersonId { get; set; }

        public decimal? Rate { get; set; }
    }
}
