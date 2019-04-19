using System.Collections.Generic;

namespace SmallProgramDemo.Core.Entities
{
    /// <summary>
    /// API响应的传出数据与分页元数据封装类
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PaginatedList<T> :List<T> where T: class
    {
        /// <summary>
        /// 页面数据量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        private int _totalItemsCount;
        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => TotalItemsCount / PageSize + (TotalItemsCount % PageSize > 0 ? 1 : 0);

        /// <summary>
        /// 是否具有前一页
        /// </summary>
        public bool HasPrevious => PageIndex > 0;
        /// <summary>
        /// 是否具有后一页
        /// </summary>
        public bool HasNext => PageIndex < PageCount - 1;

        /// <summary>
        /// PaginatedList构造函数
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示数据条数</param>
        /// <param name="totalItemsCount">总数据条数</param>
        /// <param name="data">数据集合</param>
        public PaginatedList(int pageIndex, int pageSize, int totalItemsCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
      
    }

}
