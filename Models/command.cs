using System.ComponentModel.DataAnnotations;
using SixMinApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace SixMinApi.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? HowTo { get; set; }

        [Required]
        public string? CommandLine { get; set; }

        //Foreign key property referencing the primary key in the Platform entity/model
        public int? PlatformId { get; set; }

        //Navigation Property
        public Platform? Platform { get; set; }

    }
}