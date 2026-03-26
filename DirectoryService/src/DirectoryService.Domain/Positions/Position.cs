using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Departments;

namespace DirectoryService.Domain.Positions;

public class Position
{
    private readonly List<DepartmentPosition> _departmentPositions = [];

    // EF Core
    private Position()
    {
    }

    private Position(
        PositionId id,
        PositionName name,
        Description? description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public PositionId Id { get; private set; } = null!;

    public PositionName Name { get; private set; } = null!;

    public Description? Description { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    public static Position Create(
        PositionName name,
        Description? description,
        PositionId? positionId = null)
    {
        return new Position(
            positionId ?? new PositionId(Guid.NewGuid()),
            name,
            description);
    }
}