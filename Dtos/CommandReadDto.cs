namespace SixMinApi.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        
        public string? HowTo { get; set; }

        public int PlatformId { get; set; }

       // public string? Platform { get; set; }

        public string? CommandLine { get; set; }


    }
}