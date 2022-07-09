using PkkService.Queries.Infrastructure.PkkServices.Typeahead.Contracts;

namespace PkkService.Queries.Application.PkkServices
{
    /// <summary>
    /// Сервис для работы с typeahead API ПКК
    /// </summary>
    public interface IPkkTypeaheadService
    {
        /// <summary>
        /// Найти адресса объектов ПКК на заданном слое по ключевому тексту
        /// </summary>
        /// <param name="pkkLayer">Слой ПКК</param>
        /// <param name="text">Заданный текст для поиска</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TypeaheadContract> FindPkkObjectsAddressOnLayer(int pkkLayer, string text, CancellationToken cancellationToken);
    }
}
