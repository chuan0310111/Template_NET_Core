using _1.Template_NET_Core.Application.Parameters;
using FluentValidation;

namespace _1.Template_NET_Core.Application.Controllers.Validators
{
    public class LoginParameterValidator : AbstractValidator<LoginParameter>
    {
        /// <summary>
        /// 驗證欄位
        /// </summary>
        public LoginParameterValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("請輸入帳號")
                .NotEmpty().WithMessage("請輸入帳號")
                .Must(rows => rows?.Length <= 10).WithMessage("帳號必須<=10");

            RuleFor(x => x.Password)

                .NotNull().WithMessage("請輸入密碼")
                .NotEmpty().WithMessage("請輸入密碼")
                .Must(rows => rows?.Length <= 10).WithMessage("密碼必須<=10");
        }

    }
}
