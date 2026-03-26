using DirectoryService.Domain.DepartmentLocations;

namespace DirectoryService.Domain.Locations;

public class Location
{
    private readonly List<DepartmentLocation> _departmentLocations = [];

    // EF Core
    private Location()
    {
    }

    private Location(
        LocationId id,
        LocationName name,
        Address address,
        TimeZone timeZone)
    {
        Id = id;
        Name = name;
        Address = address;
        TimeZone = timeZone;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public LocationId Id { get; private set; } = null!;

    public LocationName Name { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public TimeZone TimeZone { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;

    public static Location Create(
        LocationName name,
        Address address,
        TimeZone timeZone,
        LocationId? locationId = null)
    {
        return new Location(
            locationId ?? new LocationId(Guid.NewGuid()),
            name,
            address,
            timeZone);
    }
}