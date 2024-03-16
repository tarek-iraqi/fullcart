namespace FullCart.Server.Shared.BaseModels;

public class PaginatedResult<T>
{
    public PagingMetaData Pagination { get; init; }

    public IEnumerable<T> List { get; init; }

    public PaginatedResult() { }

    public PaginatedResult(IEnumerable<T> data, long count, int pageNumber, int pageSize)
    {
        List = data;
        var totalNumberOfPages = Math.Ceiling((decimal)count / pageSize);
        Pagination = new PagingMetaData
        {
            HasNext = pageNumber < totalNumberOfPages,
            HasPrevious = pageNumber > 1,
            TotalPages = (int)totalNumberOfPages,
            TotalRecords = count,
            PageIndex = pageNumber
        };
    }
}

public class PagingMetaData
{
    public bool HasPrevious { get; set; }

    public bool HasNext { get; set; }

    public int TotalPages { get; set; }

    public long TotalRecords { get; set; }

    public int PageIndex { get; set; }
}
