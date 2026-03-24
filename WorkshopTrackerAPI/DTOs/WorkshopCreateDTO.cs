// DTOs/WorkshopCreateDTO.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeuProjeto.DTOs  // 👈 VERIFIQUE SE ESTE NAMESPACE ESTÁ CORRETO
{
    public class WorkshopCreateDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Data de realização é obrigatória")]
        public DateTime DataRealizacao { get; set; }
        
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "É necessário informar pelo menos um colaborador")]
        [MinLength(1, ErrorMessage = "É necessário informar pelo menos um colaborador")]
        public List<int> ColaboradoresIds { get; set; }
    }
}