using System;

namespace Customers.Services.Exceptions
{   
    /// <summary>
    /// Exception that indicates that the requested entity does not exist
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string exMessage) : base(exMessage)
        {

        }
    }
}
