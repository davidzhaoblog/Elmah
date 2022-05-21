namespace Framework.Models
{
    public class Pagination
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
    }
}

