namespace PkkService.Queries.Models
{
    /// <summary>
    /// Модель ответа сервиса с информацией об объекте ПКК
    /// </summary>
    public class PkkObjectInfoModel
    {
        /// <summary>
        /// Атрибуты объекта ПКК
        /// </summary>
        public Dictionary<string, object> Attrs { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public LayersType? Type { get; set; }
    }
}
