using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    /// <summary>
    /// ResourceModel排序属性映射到EntityModel排序属性
    /// </summary>
    /// <typeparam name="TSource">ResourceModel</typeparam>
    /// <typeparam name="TDestination">EntityModel</typeparam>
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
            where TDestination : IEntity
    {
        /// <summary>
        /// 一个ResourceModel的属性名称Key(string)到EntityModel的MappedProperty的键值对字典属性
        /// 例如ResourceModel的post的age属性，到EntityModel的birthday的MappedProperty的键值对
        /// MappingDictionary["age"] = List<MappedProperty> mappedPropery
        /// </summary>
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mappingDictionary">一个ResourceModel的属性名称Key(string)到EntityModel的MappedProperty的键值对字典参数</param>
        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            //用参数初始化MappingDictionary属性
            MappingDictionary = mappingDictionary;

            //将属性中key为id的键对应为Entity的id，并且设置Revert为false
            MappingDictionary[nameof(IEntity.id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(IEntity.id), Revert = false}
            };
        }
    }
}
