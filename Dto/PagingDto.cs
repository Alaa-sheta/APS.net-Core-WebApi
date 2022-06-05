namespace TechnicalTask.Dto
{
    public class PagingDto
    {
        public PagingDto(int page)
        {
            PageSize = 50;
            Page = page;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}
