namespace DatingApp.Helper
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int pageCount, int itemsPerPage, int totalPages)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;
            ItemsPerPage = itemsPerPage;
            TotalPages = totalPages;
        }
        //adding extension method allows add a pgination header to our http reponse 
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        //these are all the informations we want to send back to our client
    }
}
