using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoMaster.Domain.Dtos
{
    public class FormaRequestDto
    {
        [Required]
        [JsonPropertyName("tipoForma")]
        public string TipoForma { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("propriedades")]
        public Dictionary<string, double>? Propriedades { get; init; }
    }
}