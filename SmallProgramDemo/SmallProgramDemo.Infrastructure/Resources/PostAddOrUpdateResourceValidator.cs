using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Resources
{
    public class PostAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : PostAddOrUpdateResource
    {
        public PostAddOrUpdateResourceValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithName("标题")
                .WithMessage("required|{PropertyName}是必填的")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");

            RuleFor(x => x.Body)
                .NotNull()
                .WithName("正文")
                .WithMessage("required|{{PropertyName}是必填的")
                .MinimumLength(100)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");
        }
    }
}
