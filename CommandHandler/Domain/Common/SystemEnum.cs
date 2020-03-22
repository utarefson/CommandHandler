namespace CommandHandler.Domain.Common
{
    public enum RabbitMQCommand
    {
        // ProductUnity
        CreateProductUnityCommand,
        UpdateProductUnityCommand,
        DeleteProductUnityCommand,

        // Product
        CreateProducCommand,
        UpdateProductCommand,
        DeleteProductCommand,

        // User
        CreateUserCommand,
        UpdateUserCommand,
        DeleteUserCommand,

        // KardexEntry
        CreateKardexEntryCommand,
        UpdateKardexEntryCommand,
        DeleteKardexEntryCommand,

        // Default
        Unknown
    }

    public enum CommandExecutionResult
    {
        Succeed,
        Fail
    }
}
