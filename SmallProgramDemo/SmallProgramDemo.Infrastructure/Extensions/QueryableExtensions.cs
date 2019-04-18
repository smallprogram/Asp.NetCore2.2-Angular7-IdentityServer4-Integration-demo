using SmallProgramDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SmallProgramDemo.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 针对ResourceModel到EntityModel的propertyMapping的设置，和orderBy的字段进行排序
        /// </summary>
        /// <typeparam name="T">EntityModel类型</typeparam>
        /// <param name="source">数据 集合</param>
        /// <param name="orderBy">需要排序的字段，多字段用“,”分隔，降序排序在末尾追加“ desc”默认为“ asc”升序排序</param>
        /// <param name="propertyMapping">ResourceModel到EntityModel的propertyMapping配置类实体</param>
        /// <returns></returns>
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, IPropertyMapping propertyMapping)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (propertyMapping == null)
            {
                throw new ArgumentNullException(nameof(propertyMapping));
            }

            //取出ResourceModel到EntityModel的映射字典
            var mappingDictionary = propertyMapping.MappingDictionary;

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            //如果没有排序字段，直接返回原数据集合
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            //将排序字段按照“,”生成字符串数组
            var orderByAfterSplit = orderBy.Split(',');

            //反转排序字段字符串数组的元素顺序，并且循环取出每个元素的值
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                //去掉元素中的空格
                var trimmedOrderByClause = orderByClause.Trim();
                //如果元素中带有“ desc”,则将变量orderDescending设置为true，默认false
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");
                //去除元素中的空格，并赋值给propertyName变量
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);


                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                //如果该排序元素不在mappingDictionary字典中抛出异常，如果存在将该元素对应的Entity属性集合放入mappedProperties集合
                if (!mappingDictionary.TryGetValue(propertyName, out List<MappedProperty> mappedProperties))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }
                if (mappedProperties == null)
                {
                    throw new ArgumentNullException(propertyName);
                }

                //反转需要排序的Entity属性的mappedProperties集合
                mappedProperties.Reverse();

                //循环需要排序的Entity属性的mappedProperties集合
                foreach (var destinationProperty in mappedProperties)
                {
                    //判断mappedProperties元素的Revert字段是否为反向映射
                    if (destinationProperty.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    //使用System.Linq.Dynamic.Core的OrderBy方法动态组成需要排序的字段的LINQ语句
                    source = source.OrderBy(destinationProperty.Name + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source;
        }

        public static IQueryable<object> ToDynamicQueryable<TSource>
            (this IQueryable<TSource> source, string fields, Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(fields))
            {
                return (IQueryable<object>)source;
            }

            fields = fields.ToLower();
            var fieldsAfterSplit = fields.Split(',').ToList();
            if (!fieldsAfterSplit.Contains("id", StringComparer.InvariantCultureIgnoreCase))
            {
                fieldsAfterSplit.Add("id");
            }
            var selectClause = "new (";

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }

                var key = mappingDictionary.Keys.SingleOrDefault(k => String.CompareOrdinal(k.ToLower(), propertyName.ToLower()) == 0);
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }
                var mappedProperties = mappingDictionary[key];
                if (mappedProperties == null)
                {
                    throw new ArgumentNullException(key);
                }
                foreach (var destinationProperty in mappedProperties)
                {
                    selectClause += $" {destinationProperty.Name},";
                }
            }

            selectClause = selectClause.Substring(0, selectClause.Length - 1) + ")";
            return (IQueryable<object>)source.Select(selectClause);
        }

    }
}
