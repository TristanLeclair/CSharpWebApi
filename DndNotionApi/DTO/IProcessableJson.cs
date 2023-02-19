namespace WebApplication1.DTO;

/// <summary>
/// Interface for all DTOs to be able to process themselves into what
/// we need to use in our model.
/// </summary>
/// <typeparam name="T">The type to be returned
/// from the Process method</typeparam>
public interface IProcessableJson<out T>
{
    /// <summary>
    /// Process this json object into its appropriate model class
    /// </summary>
    /// <returns>The appropriate model class</returns>
    public T Process();
}