using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SmallProgramDemo.Api.Helpers
{
    /// <summary>
    /// 自定义错误验证信息返回类
    /// </summary>
    public class MyUnprocessableEntityObjectResult : UnprocessableEntityObjectResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="modelState"></param>
        public MyUnprocessableEntityObjectResult(ModelStateDictionary modelState) : base(new ResourceValidationResult(modelState))
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            StatusCode = 422;
        }
    }
}
