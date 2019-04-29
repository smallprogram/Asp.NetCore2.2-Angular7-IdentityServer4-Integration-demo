using FluentValidation;

namespace SmallProgramDemo.Infrastructure.Resources
{
    public class PostImageResourceValidator: AbstractValidator<PostImageResource>
    {
        public PostImageResourceValidator()
        {
            RuleFor(x => x.FileName)
                .NotNull()
                .WithName("文件名")
                .WithMessage("required|{PropertyName}是必须的")
                .MaximumLength(100)
                .WithMessage("maxlength|{PropertyName}不能超过{MaxLength}");
        }
    }
}
