using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Services;

public class LetterServices : ILetterServices
{
    public Task<Sent> Process(Notification aggregate)
    {
        throw new NotImplementedException();
    }
}