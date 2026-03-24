namespace DirectoryService.Application.Abstractions;

public interface ICommand;

public interface ICommandHandler<TResponse, in TCommand>
    where TCommand : ICommand
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}