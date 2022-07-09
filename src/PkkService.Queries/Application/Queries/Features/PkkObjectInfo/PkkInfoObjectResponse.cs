namespace PkkService.Queries.Application.Queries.Features.PkkObjectInfo
{
    /// <summary>
    /// Модель иформации об объекте ПКК
    /// </summary>
    public class PkkInfoObjectResponse
    {
        /// <summary>
        /// Атрибуты
        /// </summary>
        public Dictionary<string, object> Attrs { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public int? Type { get; set; }
    }
}
