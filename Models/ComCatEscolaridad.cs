using System.ComponentModel.DataAnnotations;

namespace Cuidados.Caninos.Marcos.Montiel.Models
{
    public class ComCatEscolaridad
    {
        [Key]         public int ID { get; set; }          [Required]         [MaxLength(50)]         public string Valor { get; set; }
    }
}