using SharedKernel;

namespace DirectoryService.Domain.Shared;

public static class DirectoryError
{
    public static Error NameLocationConflict() =>
        Error.Conflict("location.name.conflict", "Локация с таким именем уже существует");

    public static Error AddressLocationConflict() =>
        Error.Conflict("location.address.conflict", "Локация с таким адресом уже существует");

    public static Error DatabaseError() =>
        Error.Failure("directory.database.error", "Ошибка базы данных при работе с сервисом - directory");

    public static Error OperationCancelled() =>
        Error.Failure("directory.operation.cancelled", "Операция была отменена");
}