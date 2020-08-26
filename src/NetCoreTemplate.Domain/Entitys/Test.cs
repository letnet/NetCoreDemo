using Dapper.Contrib.Extensions;
using NetCoreTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Domain.Entitys
{

    [Table("test")]
    public class Test : IEntity
    {
        [ExplicitKey]
        public string ID { get; set; }
        public string Title { get; set; }
    }
}
