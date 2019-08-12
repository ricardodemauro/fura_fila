using FuraFila.WebApp.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.WebApp.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILogger<NotificationController> _logger;

        private readonly NotificationRequestHandler _handler;

        public NotificationController(ILogger<NotificationController> logger, NotificationRequestHandler handler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        [HttpPost]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult PagSeguro([FromForm] string notificationCode, [FromForm] string notificationType)
        {
            _logger.LogInformation(notificationCode);
            _logger.LogInformation(notificationType);

            //   Console.WriteLine(form.ToString());
            return Ok();
        }
    }
}
