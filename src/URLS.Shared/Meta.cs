namespace URLS.Shared;

public class Meta
{
    public static Meta GetMeta(int totalCount, int per)
    {
        return new Meta
        {
            TotalPages = PaginationHelper.GetTotalPages(totalCount, per),
        };
    }

    public int TotalPages { get; set; }
}