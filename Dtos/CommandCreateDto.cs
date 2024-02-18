using System.ComponentModel.DataAnnotations;

namespace SixMinApi.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        public string? HowTo { get; set; }

        [Required]
        public int PlatformId { get; set; } // Updated to represent the foreign key

        [Required]
        public string? CommandLine { get; set; }

    }
}