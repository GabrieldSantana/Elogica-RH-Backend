namespace Domain;

public class RetornoPaginado<T> where T : class
{
    public int TotalRegistro { get; set; }
    public int Quantidade { get; set; }
    public int Pagina { get; set; }

    public List<T> RetornoPagina { get; set; }
}
