namespace Customers.Api.Models
{
    /// <summary>
    /// Generic Api Response Data
    /// </summary>
    /// <typeparam name="T">Type of Return Object</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Used to build Error Api Responses
        /// </summary>
        /// <param name="errorMessage">error message</param>
        public ApiResponse(string errorMessage) => Error = new Error { ErrorMessage = errorMessage };

        /// <summary>
        /// Used for Building Succesfull Response Messages
        /// </summary>
        /// <param name="data">data to return</param>
        public ApiResponse(T data) => Data = data;

        /// <summary>
        /// Error Object, not null when we have an error.
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// Generic Data Response
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Error Response Data
    /// </summary>
    public class Error
    {
        public string ErrorMessage { get; set; }
    }
}
