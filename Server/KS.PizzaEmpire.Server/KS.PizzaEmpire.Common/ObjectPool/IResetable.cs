namespace KS.PizzaEmpire.Common.ObjectPool
{
    /// <summary>
    /// Represents an item whose state can be reset
    /// </summary>
    public interface IResetable
    {
        void Reset();
    }
}