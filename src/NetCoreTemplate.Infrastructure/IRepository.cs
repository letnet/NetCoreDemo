using NetCoreTemplate.Infrastructure.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetCoreTemplate.Infrastructure
{
    /// <summary>
    /// Repository接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 删除行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 删除表所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool DeleteAll();
        /// <summary>
        /// 获取行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(object id);
        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// 添加行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        long Insert(T entity);
        /// <summary>
        /// 更新行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="param">参数</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        PagedResult<T> GetPageList(string sql, int pageIndex, int pageSize, object param = null);
    }
}
