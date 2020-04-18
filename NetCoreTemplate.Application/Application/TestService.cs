using NetCoreTemplate.Application.Models;
using NetCoreTemplate.Domain;
using NetCoreTemplate.Domain.Entitys;
using NetCoreTemplate.Infrastructure;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Application.Application
{
    public class TestService : ITestService
    {
        ITestDomain _testDomain { get; set; }
        public TestService(ITestDomain testDomain)
        {
            this._testDomain = testDomain;
        }

        [Transactional]
        public TestDto Get(string id)
        {
            using (MiniProfiler.Current.Step("一个测试")) {
                var test = _testDomain.Get(id);
                return test.MapTo<TestDto>();
            }
        }


        [Transactional]
        public void Insert(List<TestDto> list)
        {
            var listTest = list.MapToList<Test>();
            _testDomain.Insert(listTest);
        }
    }
}
