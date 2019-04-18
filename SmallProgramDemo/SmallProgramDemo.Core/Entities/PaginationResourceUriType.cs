using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Entities
{
    /// <summary>
    /// 数据资源的元数据中的分页元数据枚举类
    /// </summary>
    public enum PaginationResourceUriType
    {
        /// <summary>
        /// 当前页
        /// </summary>
        CurrentPage,
        /// <summary>
        /// 前一页
        /// </summary>
        PreviousPage,
        /// <summary>
        /// 后一页
        /// </summary>
        NextPage
    }
}
