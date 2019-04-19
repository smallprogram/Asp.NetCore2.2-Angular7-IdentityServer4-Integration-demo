using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Extensions
{
    /// <summary>
    /// 集合资源塑形扩展类
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 集合资源塑形扩展方法
        /// </summary>
        /// <typeparam name="TSource">原数据的类型</typeparam>
        /// <param name="source">原始数据集合</param>
        /// <param name="fields">返回那些字段，多字段用“,”分隔，如果不传递默认为全属性集合返回</param>
        /// <returns>塑形后的IEnumerable<ExpandoObject>数据集合</returns>
        public static IEnumerable<ExpandoObject> ToDynamicIEnumerable<TSource>(this IEnumerable<TSource> source, string fields = null)
        {
            //判读原始数据集合是否为空
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            //创建一个ExpandoObject集合，用于存放塑形后数据集合
            var expandoObjectList = new List<ExpandoObject>();

            //创建一个PropertyInfo集合，用于存放传入的塑形属性名称集合
            var propertyInfoList = new List<PropertyInfo>();

            //如果没有传入需要塑形的属性，则返回将所有属性名称添加到propertyInfoList中
            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            //如果有传入的属性属性，则将传入的属性名称添加到propertyInfoList中
            else
            {
                var fieldsAfterSplit = fields.Split(',').ToList();
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    if (string.IsNullOrEmpty(propertyName))
                    {
                        continue;
                    }
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                }
            }

            //循环原始数据，提出每条数据，从propertyInfoList提取每条propertyInfo对应的propertyInfo.Name和对应的propertyValue，
            //存入expandoObjectList中
            foreach (TSource sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(dataShapedObject);
            }
            //返回塑形后的数据
            return expandoObjectList;
        }
    }
}
