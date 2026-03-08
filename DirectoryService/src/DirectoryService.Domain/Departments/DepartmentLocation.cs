namespace DirectoryService.Domain.Departments;

public class DepartmentLocation
{
    public DepartmentLocation(Guid locationId, Guid departmentId)
    {
        LocationId = locationId;
        DepartmentId = departmentId;
    }

    public Guid LocationId { get; private set; }

    public Guid DepartmentId { get; private set; }
}