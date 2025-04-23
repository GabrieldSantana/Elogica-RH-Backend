namespace Domain;

public class RetornoPaginado<T> where T : class
{
    public int TotalRegistro { get; set; }
    public List<T> Registros { get; set; }
}
