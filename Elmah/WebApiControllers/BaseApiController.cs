using Framework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.WebApiControllers
{
    public class BaseApiController: Controller
    {
        public ActionResult ReturnWithoutBodyActionResult(Response serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok();
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode(serviceResponse.Status.HasValue ? (int)serviceResponse.Status : (int)HttpStatusCode.InternalServerError);
        }

        public ActionResult<T> ReturnResultOnlyActionResult<T>(Response<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse.ResponseBody);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        public ActionResult<Response<T>> ReturnActionResult<T>(Response<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        public ActionResult<PagedResponse<T>> ReturnActionResult<T>(PagedResponse<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }
    }
}
