namespace Api.Dtos
{
    public class EnumDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EnumDto() 
        {
            Name = string.Empty;
        }
    }
}
