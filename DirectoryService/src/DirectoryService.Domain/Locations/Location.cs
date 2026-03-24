using DirectoryService.Domain.Departments;
using DirectoryService.Domain.ValueObjects;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Domain.Locations;

public class Location
{
    private List<DepartmentLocation> _departmentLocations = [];

    public Location(
        NameLocation name,
        Address address,
        TimeZone timeZone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        TimeZone = timeZone;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    // EF Core
    private Location()
    {
    }

    public Guid Id { get; private set; }

    public NameLocation Name { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public TimeZone TimeZone { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;
}