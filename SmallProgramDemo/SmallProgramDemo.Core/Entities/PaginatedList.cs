using System.Collections.Generic;

namespace SmallProgramDemo.Core.Entities
{
    public class PaginatedList<T> :List<T> where T: class
    {
        //页面数据量
        public int PageSize { get; set; }
        //第几页
        public int PageIndex { get; set; }

        //总数据量
        private int _totalItemsCount;
        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }

        //总页数
        public int PageCount => TotalItemsCount / PageSize + (TotalItemsCount % PageSize > 0 ? 1 : 0);

        //前一页
        public bool HasPrevious => PageIndex > 0;
        //后一页
        public bool HasNext => PageIndex < PageCount - 1;

        //构造函数
        public PaginatedList(int pageIndex, int pageSize, int totalItemsCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
      
    }

}
