using System;
using System.Collections.Generic;

namespace SeuProjeto.DTOs
{
    public class WorkshopResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataRealizacao { get; set; }
        public List<ColaboradorInfoDTO> Colaboradores { get; set; }
    }
}