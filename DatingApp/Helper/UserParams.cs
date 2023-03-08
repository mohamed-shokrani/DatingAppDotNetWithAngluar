namespace DatingApp.Helper
{
    public class UserParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _PageSize = 10; //default Page size

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize )? MaxPageSize : value; 
        }


    }
}
