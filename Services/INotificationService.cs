using RedSignal.Models;

namespace RedSignal.Services;

public interface INotificationService
{
    Task<List<NotificacaoDto>> GetNotificationsForUserAsync(long userId);
}
