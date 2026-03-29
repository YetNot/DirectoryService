using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace DirectoryService.Application.Validation;

public static class CustomValidator
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Errors>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Errors> result = factoryMethod.Invoke(value);

            if (result.IsSuccess)
                return;

            foreach (var error in result.Error)
            {
                context.AddFailure(error.Serialize());
            }
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
}