using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeoMaster.Domain.Dtos
{
    public class FormaContidaRequestDto
    {
        [Required]
        [JsonPropertyName("formaExterna")]
        public FormaRequestDto FormaExterna { get; set; } = new();

        [Required]
        [JsonPropertyName("formaInterna")]
        public FormaRequestDto FormaInterna { get; set; } = new();
    }
}