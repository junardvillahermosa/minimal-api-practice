using System.ComponentModel.DataAnnotations;

namespace SixMinApi.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        public string? PlatformName { get; set; }
    }
}