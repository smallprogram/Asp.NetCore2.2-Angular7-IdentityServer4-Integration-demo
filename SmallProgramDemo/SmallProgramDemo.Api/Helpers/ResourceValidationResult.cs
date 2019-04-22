using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace SmallProgramDemo.Api.Helpers
{
    /// <summary>
    /// 解析ModelState错误信息类
    /// </summary>
    public class ResourceValidationResult : Dictionary<string, IEnumerable<ResourceValidationError>>
    {
        /// <summary>
        /// 构造函数，不区分大小写
        /// </summary>
        public ResourceValidationResult() : base(StringComparer.OrdinalIgnoreCase)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="modelState">modelState验证信息</param>
        public ResourceValidationResult(ModelStateDictionary modelState)
            : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    var errorsToAdd = new List<ResourceValidationError>();
                    foreach (var error in errors)
                    {
                        //如果错误信息中有“|”,就将竖线之前的信息存储到ResourceValidationError的ValidatorKey中，
                        //并将“|”之后的数据存储到ResourceValidationError的Message中
                        //如果错误信息中没有“|”,则将错误信息存储到ResourceValidationError的Message中
                        //之后将ResourceValidationError实例添加到errorsToAdd集合中
                        var keyAndMessage = error.ErrorMessage.Split('|');

                        if (keyAndMessage.Length > 1)
                        {
                            errorsToAdd.Add(new ResourceValidationError(keyAndMessage[1], keyAndMessage[0]));
                        }
                        else
                        {
                            errorsToAdd.Add(new ResourceValidationError(keyAndMessage[0]));
                        }
                    }
                    Add(key, errorsToAdd);
                }
            }
        }
    }
}