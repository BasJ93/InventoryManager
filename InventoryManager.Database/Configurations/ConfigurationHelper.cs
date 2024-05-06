namespace InventoryManager.Database.Configurations;

public class ConfigurationHelper
{
    public static string ToTableName<T>(string? tableName) => tableName ?? typeof(T).Name;
}