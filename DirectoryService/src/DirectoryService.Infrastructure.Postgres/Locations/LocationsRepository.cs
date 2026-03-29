using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using SharedKernel;

namespace DirectoryService.Infrastructure.Postgres.Locations;

public class LocationsRepository : ILocationsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<LocationsRepository> _logger;

    public LocationsRepository(DirectoryServiceDbContext dbContext, ILogger<LocationsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Locations.AddAsync(location, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success<Guid, Error>(location.Id.Value);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            if (pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                if (pgEx.ConstraintName == "ux_locations_name")
                {
                    return DirectoryError.NameLocationConflict();
                }

                if (pgEx.ConstraintName == "ux_locations_address")
                {
                    return DirectoryError.AddressLocationConflict();
                }
            }

            _logger.LogError(
                ex,
                "Database update error while saving location with id {LocationId}",
                location.Id.Value);

            return DirectoryError.DatabaseError();
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(
                ex,
                "Operation was cancelled while creating the location with id {LocationId}",
                location.Id.Value);

            return DirectoryError.OperationCancelled();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Location not saved with id {LocationId}",
                location.Id.Value);

            return DirectoryError.DatabaseError();
        }
    }
}