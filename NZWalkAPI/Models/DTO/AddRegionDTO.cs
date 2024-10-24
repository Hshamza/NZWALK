using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO
{
    public class AddRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be min of 3 characters")]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
