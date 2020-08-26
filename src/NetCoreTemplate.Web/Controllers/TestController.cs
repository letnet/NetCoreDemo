using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreTemplate.Application;
using NetCoreTemplate.Application.Models;

namespace NetCoreTemplate.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        ITestService _testService { get; set; }
        public TestController(ITestService testService)
        {
            this._testService = testService;
        }

        /// <summary>
        /// 获取测试信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public TestDto GetInfo()
        {
            return _testService.Get("68f4469d-1222-4414-bf73-6823815cdb15");             
        }
    }
}
