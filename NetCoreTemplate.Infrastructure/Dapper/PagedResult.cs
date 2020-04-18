using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Infrastructure.Dapper
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public PagedResult()
        {
            this.Paged = new Paged();
        }

        /// <summary>
        /// 结果
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 分页数据，包含数据总行数、当前页码、页数据行数和总的分页数
        /// </summary>
        public Paged Paged { get; set; }
    }
}
