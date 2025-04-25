namespace Domain.Dtos;

public class RespostaMenuDto
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
    public string Icone { get; set; }
    public int Ordem { get; set; }
    public int MenuPaiId { get; set; }
}
