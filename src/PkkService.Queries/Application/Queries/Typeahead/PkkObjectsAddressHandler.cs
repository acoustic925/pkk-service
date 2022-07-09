using PkkService.Queries.Application.Common;
using PkkService.Queries.Application.PkkServices;

namespace PkkService.Queries.Application.Queries.Typeahead
{
    public class PkkObjectsAddressHandler : IRequestHandler<PkkObjectsAddressQuery, IEnumerable<PkkObjectAddressResponse>>
    {
        private readonly IPkkTypeaheadService _typeaheadService;

        public PkkObjectsAddressHandler(IPkkTypeaheadService typeaheadService)
        {
            _typeaheadService = typeaheadService;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="UnvaliablePkkServiceException"></exception>
        public async Task<IEnumerable<PkkObjectAddressResponse>> Handle(PkkObjectsAddressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _typeaheadService.FindPkkObjectsAddressOnLayer(request.PkkLayer, request.Text, cancellationToken);

                return result.Results.Select(x => new PkkObjectAddressResponse
                {
                    Title = x.Title,
                    Value = x.Value
                });
            }
            catch (HttpRequestException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
        }
    }
}
