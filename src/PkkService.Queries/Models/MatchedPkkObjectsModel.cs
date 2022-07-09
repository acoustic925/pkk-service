namespace PkkService.Queries.Models
{
    /// <summary>
    /// Модель ответа сервиса содержащая идентификационную информацию объектов ПКК
    /// </summary>
    public class MatchedPkkObjectsModel
    {
        /// <summary>
        /// Полное число записей
        /// </summary>
        public int? Total { get; set; }
        
        /// <summary>
        /// Список подходящих объектов ПКК
        /// </summary>
        public IEnumerable<MatchedPkkObjectModel> Results { get; set; }
    }

    /// <summary>
    /// Сматченный объект ПКК
    /// </summary>
    public class MatchedPkkObjectModel
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
        public LayersType? Type { get; set; }
    }
}
