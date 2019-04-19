using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Extensions
{
    /// <summary>
    /// 单个资源塑形扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 单个资源塑形扩展方法
        /// </summary>
        /// <typeparam name="TSource">原数据的类型</typeparam>
        /// <param name="source">原数据集合</param>
        /// <param name="fields">返回那些字段，多字段用“,”分隔，如果不传递默认为全属性集合返回</param>
        /// <returns>塑形后的ExpandoObject对象</returns>
        public static ExpandoObject ToDynamic<TSource>(this TSource source, string fields = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            //创建ExpandoObject的实例dataShapedObject，用于存放塑形后的单个资源
            var dataShapedObject = new ExpandoObject();

            //如果fields参数为空，就返回具有所有属性的ExpandoObject实例dataShapedObject
            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }
                return dataShapedObject;
            }

            //如果fields参数不为空，就返回针对fields的塑形后的ExpandoObject实例dataShapedObject
            var fieldsAfterSplit = fields.Split(',').ToList();
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    throw new Exception($"Can't found property ¡®{typeof(TSource)}¡¯ on ¡®{propertyName}¡¯");
                }
                var propertyValue = propertyInfo.GetValue(source);
                ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            }

            return dataShapedObject;
        }
    }
}
