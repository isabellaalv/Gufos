using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beckend.Models
{
    public partial class Presenca
    {
        [Key]
        [Column("Presenca_id")]
        public int PresencaId { get; set; }
        [Column("Evento_id")]
        public int? EventoId { get; set; }
        [Column("Usuario_id")]
        public int? UsuarioId { get; set; }
        [Required]
        [Column("Presenca_status")]
        [StringLength(255)]
        public string PresencaStatus { get; set; }

        [ForeignKey(nameof(EventoId))]
        [InverseProperty(nameof(Eventos.Presenca))]
        public virtual Eventos Evento { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Presenca")]
        public virtual Usuario Usuario { get; set; }
    }
}
