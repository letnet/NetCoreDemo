using Dapper;
using Dapper.Contrib.Extensions;
using NetCoreTemplate.Infrastructure.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NetCoreTemplate.Infrastructure.Repositories
{
    /// <summary>
    /// 泛型Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, object param = null, CommandType? commandType = null)
        {
            var r = _unitOfWork.DbConnection.Query<T>(sql, param: param, transaction: _unitOfWork.DbTransaction, commandType: commandType);
            return r;
        }
        /// <summary>
        /// 删除行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            var r = _unitOfWork.DbConnection.Delete<T>(entity, _unitOfWork.DbTransaction);
            return r;
        }
        /// <summary>
        /// 删除表所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool DeleteAll()
        {
            var r = _unitOfWork.DbConnection.DeleteAll<T>(_unitOfWork.DbTransaction);
            return r;
        }
        /// <summary>
        /// 获取行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            var r = _unitOfWork.DbConnection.Get<T>(id, _unitOfWork.DbTransaction);
            return r;
        }
        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            var r = _unitOfWork.DbConnection.GetAll<T>(_unitOfWork.DbTransaction);
            return r;
        }
        /// <summary>
        /// 添加行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(T entity)
        {
            var r = _unitOfWork.DbConnection.Insert<T>(entity, _unitOfWork.DbTransaction);
            return r;
        }
        /// <summary>
        /// 更新行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            var r = _unitOfWork.DbConnection.Update<T>(entity, _unitOfWork.DbTransaction);
            return r;
        }
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
        public PagedResult<T> GetPageList(string sql, int pageIndex, int pageSize, object param = null)
        {
           var pagingResult =  _unitOfWork.DbConnection.GetPageList<T>(sql, pageIndex, pageSize, param: param, transaction: _unitOfWork.DbTransaction);
            return pagingResult;
        }
    }
}
