using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beckend.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Eventos = new HashSet<Eventos>();
        }

        [Key]
        [Column("Categoria_id")]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
