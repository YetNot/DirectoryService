using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.ValueObjects;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Application.Locations.CreateLocation;

public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
{
    private readonly ILocationsRepository _locationsRepository;

    public CreateLocationHandler(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    public async Task<Guid> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        Result<NameLocation> nameResult = NameLocation.Create(command.Request.Name);
        if (nameResult.IsFailure)
        {
            throw new InvalidDataException(nameResult.Error);
        }

        Result<Address> addressResult = Address.Create(
            command.Request.Address.Country,
            command.Request.Address.City,
            command.Request.Address.Street,
            command.Request.Address.HouseNumber);
        if (addressResult.IsFailure)
        {
            throw new InvalidDataException(addressResult.Error);
        }

        Result<TimeZone> timeZoneResult = TimeZone.Create(command.Request.TimeZone);
        if (timeZoneResult.IsFailure)
        {
            throw new InvalidDataException(timeZoneResult.Error);
        }

        var location = new Location(nameResult.Value, addressResult.Value, timeZoneResult.Value);

        Result<Guid> result = await _locationsRepository.AddAsync(location,  cancellationToken);
        if (result.IsFailure)
        {
            throw new InvalidDataException(result.Error);
        }

        return location.Id;
    }
}