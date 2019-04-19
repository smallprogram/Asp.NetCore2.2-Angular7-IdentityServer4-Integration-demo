using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    public interface IPropertyMappingContainer
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity;

        /// <summary>
        /// 排序属性验证方法
        /// </summary>
        /// <typeparam name="TSource">ResourceModel类型</typeparam>
        /// <typeparam name="TDestination">EntityModel类型</typeparam>
        /// <param name="fields">排序字段，多字段用“,”分隔</param>
        /// <returns></returns>
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity;
    }
}
