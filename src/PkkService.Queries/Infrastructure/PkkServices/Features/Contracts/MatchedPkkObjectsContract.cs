namespace PkkService.Queries.Infrastructure.PkkServices.Features.Contracts
{
    /// <summary>
    /// Контракт ответа features API ПКК
    /// </summary>
    public class MatchedPkkObjectsContract
    {
        /// <summary>
        /// Список обнаруженных объектов
        /// </summary>
        public IEnumerable<MatchedPkkObject> Features { get; set; }

        /// <summary>
        /// Общее число найденных объектов
        /// </summary>
        public int? Total { get; set; }
    }

    /// <summary>
    /// Контракт обнаруженного объекта ПКК
    /// </summary>
    public class MatchedPkkObject
    {
        /// <summary>
        /// Атрибуты объекта ПКК
        /// </summary>
        public Dictionary<string, object> Attrs { get; set; }

        /// <summary>
        /// Число для сортировки
        /// </summary>
        public long? Sort { get; set; }

        /// <summary>
        /// Слой ПКК
        /// </summary>
        public int? Type { get; set; }
    }
}
