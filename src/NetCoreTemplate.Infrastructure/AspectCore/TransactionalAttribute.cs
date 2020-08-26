using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTemplate.Infrastructure
{
    /// <summary>
    /// 为工作单元提供事务一致性
    /// </summary>
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        IUnitOfWork _unitOfWork { get; set; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                _unitOfWork = context.ServiceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                _unitOfWork.BeginTransaction();
                await next(context);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
