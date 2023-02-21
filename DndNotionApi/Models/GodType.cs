namespace WebApplication1.Models;

/// <summary>
///     Type of God
/// </summary>
public enum GodType
{
    /// <summary>
    ///     The supreme beings, first gods to rise.
    /// </summary>
    CreatorGods,

    /// <summary>
    ///     Natural enemies of the <see cref="CreatorGods" />, represent 7 deadly sins.
    /// </summary>
    DarkGods,

    /// <summary>
    ///     Second wave of gods.
    /// </summary>
    GreaterDeities,

    /// <summary>
    ///     Third wave of gods.
    /// </summary>
    LesserGods,

    /// <summary>
    ///     Strongest mortals in the realms.
    /// </summary>
    QuasiDeities
}