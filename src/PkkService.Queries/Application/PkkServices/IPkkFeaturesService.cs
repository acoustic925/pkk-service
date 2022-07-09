using PkkService.Queries.Infrastructure.PkkServices.Features.Contracts;

namespace PkkService.Queries.Application.PkkServices
{
    /// <summary>
    /// Сервис для работы с features API ПКК
    /// </summary>
    public interface IPkkFeaturesService
    {
        /// <summary>
        /// Найти совпадающие объекты на слое ПКК
        /// </summary>
        /// <param name="pkkLayer">Слой ПКК</param>
        /// <param name="text">Текст</param>
        /// <param name="skip">Пропустить число объектов</param>
        /// <param name="limit">Лимит объектов</param>
        /// <param name="tolerance">Приближение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<MatchedPkkObjectsContract> FindMatchedPkkObjectsOnLayer(int pkkLayer,
            string text,
            int? skip = 0,
            int? limit = 40,
            int? tolerance = 0,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Найти информацию об объекте ПКК
        /// </summary>
        /// <param name="pkkLayer">Слой ПКК</param>
        /// <param name="pkkObjectId">Идентификатор объекта</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PkkObjectInfoContract> FindPkkObjectInfo(int pkkLayer, string pkkObjectId, CancellationToken cancellationToken);
    }
}
