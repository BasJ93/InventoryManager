namespace InventoryManager.Domain.Enums;

/// <summary>
/// Enum containing the available container sizes.
/// </summary>
public enum ContainerSize
{
    /// <summary>
    /// A container that does not have its size defined yet.
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// A 1x1 container
    /// </summary>
    Size1X1 = 1,
    
    /// <summary>
    /// A 1x2 (1 wide, 2 tall) container
    /// </summary>
    Size1X2 = 2,
}