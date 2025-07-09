namespace Sim.Domain.Response;

public record PagedResponse<T> 
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; } // Novo: Total de páginas disponíveis
    public int TotalRecords { get; set; }
    public IEnumerable<T> Data { get; set; } // Use 'Data' para a lista de itens da página

    // Construtor para facilitar a criação
    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }
}