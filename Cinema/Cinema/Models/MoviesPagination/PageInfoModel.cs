namespace Cinema.Models;

public class PageInfoModel
{
    public PageInfoModel(int current, int moviesCount)
    {
        pageSize = 7;
        if (moviesCount % pageSize != 0)
            pageCount = moviesCount / pageSize + 1;
        else
            pageCount = moviesCount / pageSize;

        currentPage = current;
    }

    public int pageSize { get; set; }
    public int pageCount { get; set; }
    public int currentPage { get; set; }
}