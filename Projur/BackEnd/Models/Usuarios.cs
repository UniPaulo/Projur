using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projur.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Sobrenome { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? DataNascimento { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int? Escolaridade { get; set; }
    }
}
