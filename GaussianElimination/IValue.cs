namespace GaussianElimination
{
    public interface IValue<T> where T : IValue<T>
    {
        IValue<T> Add(IValue<T> v2);
    }
}