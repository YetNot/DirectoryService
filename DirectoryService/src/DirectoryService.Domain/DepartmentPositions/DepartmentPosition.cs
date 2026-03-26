using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Positions;

namespace DirectoryService.Domain.DepartmentPositions;

public class DepartmentPosition
{
    public DepartmentPosition(DepartmentPositionId id, PositionId positionId, DepartmentId departmentId)
    {
        Id = id;
        PositionId = positionId;
        DepartmentId = departmentId;
    }

    public DepartmentPositionId Id { get; private set; }

    public PositionId PositionId { get; private set; }

    public DepartmentId DepartmentId { get; private set; }
}