namespace MS3_Back_End.DTOs.Pagination
{
    public class PaginationResponseDTO<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
