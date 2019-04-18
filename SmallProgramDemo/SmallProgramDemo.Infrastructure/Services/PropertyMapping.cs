using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
            where TDestination : IEntity
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IEntity.id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(IEntity.id), Revert = false}
            };
        }
    }
}
