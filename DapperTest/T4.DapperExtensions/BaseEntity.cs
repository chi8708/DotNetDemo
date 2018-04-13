using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;
using DapperTest.Model;

namespace DapperTest.Model
{
    public class BaseEntity<T>:ClassMapper<T> where T:class
    {
        public BaseEntity()
        {
            var type = typeof(T);
            Map(type.GetProperty("SchemaName")).Ignore();
            Map(type.GetProperty("Properties")).Ignore();
            Map(type.GetProperty("TableName")).Ignore();
            Map(type.GetProperty("EntityType")).Ignore();
        }
    }
}