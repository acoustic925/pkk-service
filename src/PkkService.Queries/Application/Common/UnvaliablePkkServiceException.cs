namespace PkkService.Queries.Application.Common
{
    /// <summary>
    /// Исключение, выбрасываемое при проблемах обращения к сервисам ПКК
    /// </summary>
    public class UnvaliablePkkServiceException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public UnvaliablePkkServiceException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public UnvaliablePkkServiceException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UnvaliablePkkServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
