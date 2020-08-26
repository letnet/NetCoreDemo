using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Infrastructure.Dapper
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public class Paged
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页数据行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public long TotalRow { get; set; }
        /// <summary>
        /// 总的分页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (this.TotalRow > 0 && this.PageSize > 0)
                    return (int)Math.Ceiling((decimal)this.TotalRow / this.PageSize);
                else
                    return 0;
            }
        }

        public List<int> GetPageGroup(int size = 5)
        {
            if (TotalPage < 2)
            {
                return new List<int>();
            }
            size = size < 5 ? 5 : size;
            if (TotalPage < size)
                return NumberToList(1, TotalPage);
            int lHalf = (int)Math.Floor((decimal)size / 2);
            int rHalf = lHalf;
            if (size % 2 == 0)
                lHalf--;
            if (PageIndex - lHalf < 1)
                return NumberToList(1, size);
            else if (PageIndex + rHalf > TotalPage)
                return NumberToList(TotalPage - size, TotalPage);
            else
                return NumberToList(PageIndex - lHalf, PageIndex + rHalf);
        }

        private List<int> NumberToList(int start, int end)
        {
            List<int> list = new List<int>();
            for (int i = start; i < end + 1; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}
