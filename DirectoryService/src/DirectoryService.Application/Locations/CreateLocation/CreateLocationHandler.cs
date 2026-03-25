using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.ValueObjects;
using SharedKernel;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Application.Locations.CreateLocation;

public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
{
    private readonly ILocationsRepository _locationsRepository;

    public CreateLocationHandler(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    public async Task<Result<Guid, Errors>> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        Result<NameLocation, Errors> nameResult = NameLocation.Create(command.Request.Name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        Result<Address, Errors> addressResult = Address.Create(
            command.Request.Address.Country,
            command.Request.Address.City,
            command.Request.Address.Street,
            command.Request.Address.HouseNumber);
        if (addressResult.IsFailure)
        {
            return addressResult.Error;
        }

        Result<TimeZone, Errors> timeZoneResult = TimeZone.Create(command.Request.TimeZone);
        if (timeZoneResult.IsFailure)
        {
            return timeZoneResult.Error;
        }

        var location = new Location(nameResult.Value, addressResult.Value, timeZoneResult.Value);

        Result<Guid, Error> result = await _locationsRepository.AddAsync(location,  cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return location.Id;
    }
}