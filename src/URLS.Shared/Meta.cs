namespace URLS.Shared;

public class Meta
{
    public static Meta GetMeta(int totalCount, int per)
    {
        return new Meta
        {
            TotalCount = totalCount,
            TotalPages = PaginationHelper.GetTotalPages(totalCount, per),
        };
    }

    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
