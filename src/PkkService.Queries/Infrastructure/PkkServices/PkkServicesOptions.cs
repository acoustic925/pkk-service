namespace PkkService.Queries.Infrastructure.PkkServices
{
    /// <summary>
    /// Опции для сервисов ПКК
    /// </summary>
    public class PkkServicesOptions
    {
        /// <summary>
        /// Базовый путь к API ПКК
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Метод typeahead
        /// </summary>
        public string TypeaheadApiMethod { get; set; }

        /// <summary>
        /// Метод features
        /// </summary>
        public string FeaturesApiMethod { get; set; }

        /// <summary>
        /// Таймаут запроса
        /// </summary>
        public int? RequestTimeout { get; set; }
    }
}
