
using PkkService.Queries.Application.Common;
using PkkService.Queries.Application.Queries.Features.MatchedPkkObjects;
using PkkService.Queries.Application.Queries.Features.PkkObjectInfo;
using PkkService.Queries.Models;
using System.Net;

namespace PkkService.Queries.Controllers
{
    [ApiController]
    [Route("api/features")]
    public class PkkFeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PkkFeaturesController> _logger;

        public PkkFeaturesController(IMediator mediator, ILogger<PkkFeaturesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Найти объекты ПКК на слое по строгому вхождению переданного текста
        /// </summary>
        /// <param name="layer">Слой ПКК</param>
        /// <param name="text">Текст</param>
        /// <param name="limit">Ограничение на количество объектов ПКК в ответе API ПКК</param>
        /// <param name="skip">Пропустить колличество объектов</param>
        /// <param name="tolerance">Приближение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{layer}/match")]
        [ProducesResponseType(typeof(MatchedPkkObjectsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.GatewayTimeout)]
        public async Task<IActionResult> FindMatchedPkkObjectsOnLayer(LayersType layer,
            [FromQuery] string text,
            [FromQuery] int? limit = 40,
            [FromQuery] int? skip = 0,
            [FromQuery] int? tolerance = 0,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var matchedPkkObjects = await _mediator.Send(new MatchedPkkObjectsQuery
                {
                    PkkLayer = (int)layer,
                    Text = text,
                    Limit = limit,
                    Skip = skip,
                    Tolerance = tolerance
                }, cancellationToken);

                if (matchedPkkObjects is null)
                    return Ok(new MatchedPkkObjectsModel());

                return Ok(new MatchedPkkObjectsModel
                {
                    Total = matchedPkkObjects.Count(),
                    Results = matchedPkkObjects?.Select(x => new MatchedPkkObjectModel()
                    {
                        Sort = x.Sort,
                        Type = (LayersType)x.Type,
                        Attrs = x.Attrs
                    })
                });
            }
            catch (UnvaliablePkkServiceException ex)
            {
                _logger.LogError($"PkkTypeaheadService Error: {ex.Message}");

                return StatusCode((int)HttpStatusCode.GatewayTimeout, new());
            }
        }

        /// <summary>
        /// Найти информацию по объекту ПКК
        /// </summary>
        /// <param name="layer">Слой ПКК</param>
        /// <param name="id">Идентификатор объекта ПКК</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{layer}/{id}")]
        [ProducesResponseType(typeof(PkkObjectInfoModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.GatewayTimeout)]
        public async Task<IActionResult> FindPkkObjectInfo(LayersType layer, string id, CancellationToken cancellationToken)
        {
            try
            {
                var pkkObjectInfo = await _mediator.Send(new PkkObjectInfoQuery
                {
                    PkkLayer = (int)layer,
                    PkkObjectId = id
                }, cancellationToken);

                if (pkkObjectInfo is null)
                    return Ok(new PkkObjectInfoModel());

                return Ok(new PkkObjectInfoModel
                {
                    Type = (LayersType)pkkObjectInfo.Type,
                    Attrs = pkkObjectInfo.Attrs
                });
            }
            catch (UnvaliablePkkServiceException ex)
            {
                _logger.LogError($"PkkTypeaheadService Error: {ex.Message}");

                return StatusCode((int)HttpStatusCode.GatewayTimeout, new());
            }
        }
    }
}
