using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notification.Core.Common.CQRS;

public abstract class Query : IRequest<IActionResult>
{
    
}