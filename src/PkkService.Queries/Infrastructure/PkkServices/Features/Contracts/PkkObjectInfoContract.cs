namespace PkkService.Queries.Infrastructure.PkkServices.Features.Contracts
{
    /// <summary>
    /// Контракт feature сервиса API ПКК
    /// </summary>
    public class PkkObjectInfoContract
    {
        /// <summary>
        /// Параметры объекта
        /// </summary>
        public PkkObjectInfo Feature { get; set; }
    }

    /// <summary>
    /// Контракт информации об объекте ПКК
    /// </summary>
    public class PkkObjectInfo
    {
        /// <summary>
        /// Атрибуты объекта ПКК
        /// </summary>
        public Dictionary<string, object> Attrs { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public int? Type { get; set; }
    }
}
