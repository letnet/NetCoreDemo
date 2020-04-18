using NetCoreTemplate.Domain.Entitys;
using NetCoreTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Domain
{
    public interface ITestDomain : IBaseDomain
    {
        Test Get(string id);

        void Insert(List<Test> list);
    }
}
