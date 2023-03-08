using Microsoft.EntityFrameworkCore;

namespace DatingApp.Helper
{
    public class PageList<T> :List<T>//make it generic so it can take any entity like MemberDTO
    {
        public PageList(IEnumerable<T>  items,int count, int pageNumber, int pageSize)
        {//inside this constructor we are going to parse in the item that we get from our query type Ienumerable 
            CurrentPage = pageNumber;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
     // TotalPage = //if we have got a total count of 10 and our page size 5 then we get 2 pages from our query
     // if we have a count of 9 then we are gonna  work count 9/5 but ceiling will make it in two pages;
            PageSize = pageSize;
            TotatCount = count;
            AddRange(items);//add the range of the itmes inside this ctor so we have access to these items inside our page list ctor
             
        }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set;}

       public int TotatCount { get; set; }//How many items are in this query this could be all of our
        //users so we do not do any paging or based on our query like to get all of female users
        // then it will be hom many of total female users are available and that's what goes in the total count

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> sourceData, int pageNumber, int pageSize)
        {//this is gonna receive our query where we work out the pagination info

            var count = await sourceData.CountAsync();//count means whow many items are left from this query
            var items =await sourceData.Skip((pageNumber -1)*pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);

        } 

    }
}
