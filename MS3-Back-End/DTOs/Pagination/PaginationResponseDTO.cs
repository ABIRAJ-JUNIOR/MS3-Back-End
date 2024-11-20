namespace MS3_Back_End.DTOs.Pagination
{
    public class PaginationResponseDTO<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
