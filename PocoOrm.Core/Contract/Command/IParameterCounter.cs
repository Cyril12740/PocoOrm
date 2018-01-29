namespace PocoOrm.Core.Contract.Command
{
    public interface IParameterCounter
    {
        /// <summary>
        /// Must be introduce an different name on each call
        /// </summary>
        string ParameterName { get; }
    }
}