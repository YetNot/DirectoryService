using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Application.Abstractions;

public interface ICommand;

public interface ICommandHandler<TResponse, in TCommand>
    where TCommand : ICommand
{
    Task<Result<Guid, Errors>> Handle(TCommand command, CancellationToken cancellationToken);
}