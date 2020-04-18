using NetCoreTemplate.Application.Models;
using NetCoreTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Application
{
    public interface ITestService : IBaseApplication
    {
        TestDto Get(string id);

        void Insert(List<TestDto> list);
    }
}
