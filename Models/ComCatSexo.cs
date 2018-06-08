using System.ComponentModel.DataAnnotations;

namespace Cuidados.Caninos.Marcos.Montiel.Models
{
    public class ComCatSexo
    {
        [Key]         public int ID { get; set; }          [Required]         [MaxLength(50)]         public string Nombre { get; set; }
    }
}