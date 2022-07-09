using PkkService.Queries.Application.Common;
using PkkService.Queries.Application.PkkServices;

namespace PkkService.Queries.Application.Queries.Features.MatchedPkkObjects
{
    public class MatchedPkkObjectsHandler : IRequestHandler<MatchedPkkObjectsQuery, IEnumerable<MatchedPkkObjectResponse>>
    {
        private readonly IPkkFeaturesService _featuresService;

        public MatchedPkkObjectsHandler(IPkkFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="UnvaliablePkkServiceException"></exception>
        public async Task<IEnumerable<MatchedPkkObjectResponse>> Handle(MatchedPkkObjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _featuresService.FindMatchedPkkObjectsOnLayer(pkkLayer: request.PkkLayer,
                    text: request.Text,
                    skip: request.Skip,
                    limit: request.Limit,
                    tolerance: request.Tolerance,
                    cancellationToken);

                return result.Features.Select(x => new MatchedPkkObjectResponse
                {
                    Sort = x.Sort,
                    Attrs = x.Attrs,
                    Type = x.Type
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
