namespace KS.PizzaEmpire.Common
{
    /// <summary>
    /// Represnts an item that captures information about the result of an operation
    /// </summary>    
    public class Result
    {
        /// <summary>
        /// Creates an empty instance of the Result class
        /// </summary>
        public Result() { }

        /// <summary>
        /// The error code associated with the result
        /// </summary>

        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// The item associated with the opertaion
        /// </summary>
        public object Item { get; set; }
    }
}
