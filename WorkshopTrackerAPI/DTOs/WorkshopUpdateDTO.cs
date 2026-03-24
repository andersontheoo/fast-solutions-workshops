using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeuProjeto.DTOs
{
    public class WorkshopUpdateDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Data de realização é obrigatória")]
        public DateTime DataRealizacao { get; set; }
        
        public string Descricao { get; set; }
        
        public List<int> ColaboradoresIds { get; set; }
    }
}