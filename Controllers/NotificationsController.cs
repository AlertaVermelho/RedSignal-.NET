using Microsoft.AspNetCore.Mvc;
using RedSignal.Models;
using RedSignal.Services;

namespace RedSignal.Controllers;

[ApiController]
[Route("api/v1/users/{userId}/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<NotificacaoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotifications(long userId)
    {
        var notificacoes = await _notificationService.GetNotificationsForUserAsync(userId);

        return Ok(notificacoes);
    }
}
