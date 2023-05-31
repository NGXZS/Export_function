namespace PWAY_ASPNetCore_WebAPI.Filter
{
    public class PaginationFilter // set pagination LIMIT(size) & OFFSET(page)
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber; // min pageNumber = 1
            this.PageSize = pageSize > 10 ? 10 : pageSize; //max PageSize = 10
        }
    }
}
