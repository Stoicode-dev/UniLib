namespace Stoicode.UniLib.Interfaces
{
    /// <summary>
    /// Interface to make a class clone-able
    /// </summary>
    public interface ICloneable<out T>
    {
        T Clone();
    }
}