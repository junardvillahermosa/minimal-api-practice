using System.ComponentModel.DataAnnotations;

namespace SixMinApi.Dtos
{
    public class CommandUpdateDto
    {
        [Required]
        public string? HowTo { get; set; }

        [Required]
        public int PlatformId { get; set; } // Include PlatformId to represent the relationship

        //  [Required]
        //  [MaxLength(5)]
        //  public string? Platform { get; set; }

        [Required]
        public string? CommandLine { get; set; }

    }
}