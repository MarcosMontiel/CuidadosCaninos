using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuidados.Caninos.Marcos.Montiel.Models
{
    public class ComPersona
    {
        [Key]         public int ID { get; set; }          [Required]         [MaxLength(50)]         public string Nombre { get; set; }          [Required]         [MaxLength(50)]         public string APaterno { get; set; }          [Required]         [MaxLength(50)]         public string AMaterno { get; set; }          [Required]         [MaxLength(18)]         public string Curp { get; set; }          [Required]         public DateTime FechaNac { get; set; }          public int FKComCatSexo { get; set; }         [ForeignKey("FKComCatSexo")]         public ComCatSexo ComCatSexo { get; set; }          public int FKComCatEscolaridad { get; set; }         [ForeignKey("FKComCatEscolaridad")]         public ComCatEscolaridad ComCatEscolaridad { get; set; }
    }
}