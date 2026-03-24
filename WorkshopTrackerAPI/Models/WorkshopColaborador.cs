namespace SeuProjeto.Models
{
    public class WorkshopColaborador
    {
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}