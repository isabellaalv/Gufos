﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beckend.Models
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Eventos = new HashSet<Eventos>();
        }

        [Key]
        [Column("Localizacao_id")]
        public int LocalizacaoId { get; set; }
        [Required]
        [Column("CNPJ")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [Column("Razao_Social")]
        [StringLength(255)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }

        [InverseProperty("Localizacao")]
        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
