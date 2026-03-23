namespace DirectoryService.Domain.Departments;

public class DepartmentLocation
{
    public DepartmentLocation(Guid locationId, Guid departmentId)
    {
        DepartmentLocationId = Guid.NewGuid();
        LocationId = locationId;
        DepartmentId = departmentId;
    }

    public Guid DepartmentLocationId { get; private set; }

    public Guid LocationId { get; private set; }

    public Guid DepartmentId { get; private set; }
}