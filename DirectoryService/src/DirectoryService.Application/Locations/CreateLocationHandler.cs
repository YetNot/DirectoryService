using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Validation;
using DirectoryService.Contracts.Locations;
using DirectoryService.Domain.Locations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using SharedKernel;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Application.Locations;

public record CreateLocationCommand(CreateLocationRequest Request) : ICommand;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Request)
            .NotNull()
            .WithError(GeneralErrors.ValueIsRequired("request"));

        RuleFor(x => x.Request.Name)
            .MustBeValueObject(LocationName.Create);

        RuleFor(x => x.Request.Address)
            .MustBeValueObject(l => Address.Create(
                l.Country,
                l.City,
                l.Street,
                l.HouseNumber));

        RuleFor(x => x.Request.TimeZone)
            .MustBeValueObject(TimeZone.Create);
    }
}

public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<CreateLocationHandler> _logger;
    private readonly IValidator<CreateLocationCommand> _validator;

    public CreateLocationHandler(
        ILocationsRepository locationsRepository,
        ILogger<CreateLocationHandler> logger,
        IValidator<CreateLocationCommand> validator)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, Errors>> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command,  cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToList();
        }

        var nameResult = LocationName.Create(command.Request.Name).Value;

        var addressResult = Address.Create(
            command.Request.Address.Country,
            command.Request.Address.City,
            command.Request.Address.Street,
            command.Request.Address.HouseNumber).Value;

        var timeZoneResult = TimeZone.Create(command.Request.TimeZone).Value;

        var location = Location.Create(nameResult, addressResult, timeZoneResult);

        Result<Guid, Error> result = await _locationsRepository.AddAsync(location,  cancellationToken);
        if (result.IsFailure)
        {
            _logger.LogWarning(
                "Failed to created Location with {LocationId}: {Error}",
                location.Id.Value,
                result.Error);

            return result.Error.ToErrors();
        }

        _logger.LogInformation("Location was successfully created with {LocationId}", location.Id.Value);

        return location.Id.Value;
    }
}