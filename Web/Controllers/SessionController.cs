using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly ISessionService _sessionService;

        public SessionController(ILogger<SessionController> logger, ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ObjectResult> Create([FromBody] LoginRequest request)
        {
            try
            {
                var tokenAndUser = await _sessionService.Create(request.document, request.password);
                return StatusCode(201, tokenAndUser);
            }

            catch (PasswordMismatchException exception)
            {
                return StatusCode(400, new{Message = exception.Message});
            }

            catch (NotFoundException exception)
            {
                return StatusCode(404, new{Message = exception.Message});
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
