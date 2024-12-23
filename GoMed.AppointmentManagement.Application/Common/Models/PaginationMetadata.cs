namespace GoMed.AppointmentManagement.Application.Common.Models;

public class PaginationMetadata
{
    public const int PageSize = 10;
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }

    public PaginationMetadata(int totalItems, int currentPage)
    {
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        CurrentPage = currentPage > TotalPages ? TotalPages == 0 ? 1 : TotalPages : currentPage;
    }
}