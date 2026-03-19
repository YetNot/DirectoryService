namespace DirectoryService.Domain.Departments;

public class DepartmentPosition
{
    public DepartmentPosition(Guid positionId, Guid departmentId)
    {
        DepartmentPositionId = Guid.NewGuid();
        PositionId = positionId;
        DepartmentId = departmentId;
    }

    public Guid DepartmentPositionId { get; private set; }

    public Guid PositionId { get; private set; }

    public Guid DepartmentId { get; private set; }
}