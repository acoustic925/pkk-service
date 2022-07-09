namespace PkkService.Queries.Application.Queries.Features.MatchedPkkObjects
{
    /// <summary>
    /// Запрос для поиска совпадающих объектов ПКК
    /// </summary>
    public class MatchedPkkObjectsQuery : IRequest<IEnumerable<MatchedPkkObjectResponse>>
    {
        /// <summary>
        /// Текст для поиска объектов ПКК
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Слой пкк
        /// </summary>
        public int PkkLayer { get; set; }

        /// <summary>
        /// Приближение
        /// </summary>
        public int? Tolerance { get; set; }

        /// <summary>
        /// Ограничение на количество объектов ПКК в ответе API ПКК
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Пропустить число объектов
        /// </summary>
        public int? Skip { get; set; }
    }
}
