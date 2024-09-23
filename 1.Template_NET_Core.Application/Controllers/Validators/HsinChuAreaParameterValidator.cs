using _1.Template_NET_Core.Application.Parameters;
using FluentValidation;

namespace _1.Template_NET_Core.Application.Controllers.Validators
{
    public class HsinChuAreaParameterValidator : AbstractValidator<HsinChuAreaParameter>
    {
        /// <summary>
        /// 驗證欄位
        /// </summary>
        public HsinChuAreaParameterValidator() {

            RuleFor(x => x.Channel)
                .NotEmpty().WithMessage("請輸入來源別")
                .NotNull().WithMessage("請輸入來源別")
                .Must(rows => rows?.Length <= 8).WithMessage("來源別必須<=8").
                Must(rows => {

                    var status = new[] { "Channel1", "Channel2" };
                    if (status.Contains(rows)) return true;

                    return false;
                }).WithMessage("不正確的來源別");
        }

    }
}
