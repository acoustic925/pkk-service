
namespace PkkService.Queries.Application.Queries.Typeahead
{
    /// <summary>
    /// Запрос на поиск адрессов по заданному тексту объектов ПКК
    /// </summary>
    public class PkkObjectsAddressQuery : IRequest<IEnumerable<PkkObjectAddressResponse>>
    {
        public int PkkLayer { get; set; }

        public string Text { get; set; }
    }
}
