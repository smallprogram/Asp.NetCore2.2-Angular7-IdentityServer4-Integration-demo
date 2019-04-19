using System.Reflection;

namespace SmallProgramDemo.Infrastructure.Services
{
    /// <summary>
    /// 数据塑形中塑形属性的合法性判断服务类
    /// </summary>
    public class TypeHelperService : ITypeHelperService
    {
        /// <summary>
        /// 数据塑形中塑形属性的合法性判断服务方法
        /// </summary>
        /// <typeparam name="T">ResourceModel类型</typeparam>
        /// <param name="fields">塑形字段，多字段用","分隔</param>
        /// <returns>bool类型，存在true，不存在false</returns>
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();

                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }

                var propertyInfo = typeof(T)
                    .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
