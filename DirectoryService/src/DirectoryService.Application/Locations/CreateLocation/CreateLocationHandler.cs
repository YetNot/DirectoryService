using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Domain.Locations;
using Microsoft.Extensions.Logging;
using SharedKernel;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Application.Locations.CreateLocation;

public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<CreateLocationHandler> _logger;

    public CreateLocationHandler(ILocationsRepository locationsRepository, ILogger<CreateLocationHandler> logger)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Errors>> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        Result<LocationName, Errors> nameResult = LocationName.Create(command.Request.Name);
        if (nameResult.IsFailure)
        {
            _logger.LogWarning(
                "Failed to created Location name {LocationName}: {Error}",
                command.Request.Name,
                nameResult.Error);
            return nameResult.Error;
        }

        Result<Address, Errors> addressResult = Address.Create(
            command.Request.Address.Country,
            command.Request.Address.City,
            command.Request.Address.Street,
            command.Request.Address.HouseNumber);
        if (addressResult.IsFailure)
        {
            _logger.LogWarning("Failed to created Address: {Error}", addressResult.Error);

            return addressResult.Error;
        }

        Result<TimeZone, Errors> timeZoneResult = TimeZone.Create(command.Request.TimeZone);
        if (timeZoneResult.IsFailure)
        {
            _logger.LogWarning(
                "Failed to created TimeZone {TimeZone}: {Error}",
                command.Request.TimeZone,
                timeZoneResult.Error);

            return timeZoneResult.Error;
        }

        Location? location = Location.Create(nameResult.Value, addressResult.Value, timeZoneResult.Value);

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