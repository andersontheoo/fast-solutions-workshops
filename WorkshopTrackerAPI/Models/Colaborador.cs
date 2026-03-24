namespace SeuProjeto.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<WorkshopColaborador> WorkshopColaboradores { get; set; } = new();
    }
}
