using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    /// <summary>
    /// ResourceModel到EntityModel的属性映射容器服务类，需要在Startup中注册
    /// </summary>
    public class PropertyMappingContainer : IPropertyMappingContainer
    {
        protected internal readonly IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>();

        /// <summary>
        /// 将具体的ResourceModel与EntityModeled的属性映射排序的对应类PropertyMapping注册到容器
        /// </summary>
        /// <typeparam name="T">具体的ResourceModel与EntityModeled的属性映射排序的对应类PropertyMapping</typeparam>
        public void Register<T>() where T : IPropertyMapping, new()
        {
            if (PropertyMappings.All(x => x.GetType() != typeof(T)))
            {
                PropertyMappings.Add(new T());
            }
        }
        /// <summary>
        /// 解析方法，负责解析出ResourceModel对应的EntityModel的属性
        /// </summary>
        /// <typeparam name="TSource">ResourceModel</typeparam>
        /// <typeparam name="TDestination">EntityModel</typeparam>
        /// <returns>PropertyMapping实例</returns>
        public IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity
        {
            //从容器中解析出具体的ResourceModel对应的EntityModel的ProperyMappings
            //例如postPropertyMappings
            var matchingMapping = PropertyMappings.OfType<PropertyMapping<TSource, TDestination>>().ToList();
            if (matchingMapping.Count == 1)
            {
                return matchingMapping.First();
            }

            throw new Exception($"Cannot find property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        /// <summary>
        /// 验证具体的ResourceModel与EntityModeled的属性映射是否存在
        /// </summary>
        /// <typeparam name="TSource">ResourceModel</typeparam>
        /// <typeparam name="TDestination">EntityModel</typeparam>
        /// <param name="fields">ResourceModel的属性名称</param>
        /// <returns>bool值</returns>
        public bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity
        {
            var propertyMapping = Resolve<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    continue;
                }
                if (!propertyMapping.MappingDictionary.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
