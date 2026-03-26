using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;

namespace DirectoryService.Domain.DepartmentLocations;

public sealed class DepartmentLocation
{
    public DepartmentLocation(DepartmentLocationId id, LocationId locationId, DepartmentId departmentId)
    {
        Id = id;
        LocationId = locationId;
        DepartmentId = departmentId;
    }

    public DepartmentLocationId Id { get; private set; }

    public LocationId LocationId { get; private set; }

    public DepartmentId DepartmentId { get; private set; }
}