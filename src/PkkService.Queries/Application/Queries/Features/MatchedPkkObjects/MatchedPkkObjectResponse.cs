namespace PkkService.Queries.Application.Queries.Features.MatchedPkkObjects
{
    /// <summary>
    /// Модель подходящего объекта ПКК
    /// </summary>
    public class MatchedPkkObjectResponse
    {
        /// <summary>
        /// Атрибуты модели
        /// </summary>
        public Dictionary<string, object> Attrs { get; set; }

        /// <summary>
        /// Порядковый номер ПКК
        /// </summary>
        public long? Sort { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public int? Type { get; set; }
    }
}
