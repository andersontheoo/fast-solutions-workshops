public class WorkshopDTO
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataRealizacao { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public List<int> ColaboradoresIds { get; set; } = new();
}
