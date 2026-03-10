namespace DirectoryService.Domain.Departments;

public class DepartmentPosition
{
    public DepartmentPosition(Guid positionId, Guid departmentId)
    {
        PositionId = positionId;
        DepartmentId = departmentId;
    }

    public Guid PositionId { get; private set; }

    public Guid DepartmentId { get; private set; }
}