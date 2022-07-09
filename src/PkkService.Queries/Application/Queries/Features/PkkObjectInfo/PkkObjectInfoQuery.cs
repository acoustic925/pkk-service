namespace PkkService.Queries.Application.Queries.Features.PkkObjectInfo
{
    /// <summary>
    /// Запрос на поиск информации об объекте ПКК
    /// </summary>
    public class PkkObjectInfoQuery : IRequest<PkkInfoObjectResponse>
    {
        /// <summary>
        /// Идентификатор объекта ПКК
        /// </summary>
        public string PkkObjectId { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public int PkkLayer { get; set; }
    }
}
