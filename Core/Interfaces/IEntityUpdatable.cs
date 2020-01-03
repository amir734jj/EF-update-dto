namespace Core.Interfaces
{
    /// <summary>
    ///     Represents updatable entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityUpdatable<T>
    {
        T CustomUpdate(T dto);
    }
}