namespace URLS.Shared;

public static class PaginationHelper
{
    public static int GetSkip(int page, int per) => (page - 1) * per;
    public static int GetTotalPages(int totalCount, int per) => (int)Math.Ceiling((decimal)totalCount / per);
}