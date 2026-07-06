using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Application.Services;
using NotificationSystem.Application.Interfaces;

using NotificationSystem.Domain;
using NotificationSystem.Application.Dtos;
using Hangfire;
using NotificationSystem.Application;

namespace NotificationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        { _notificationService = notificationService; }
        [HttpPost("marketing")]
        public async Task<IActionResult> CreateMarketingNotificationAsync(NotificationMessage message)
        {
           // await _notificationService.SendAsync(message);
           BackgroundJob.Enqueue<NotificationJobs>(job=> job.ProcessMarketingNotification(message));
           return Ok("Queued");
        }
    }
    //--------------------------------------------------------------------- 

}
