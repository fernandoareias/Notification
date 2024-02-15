using System;
using Notification.API.DTOs.Requests;

namespace Notification.API.Tests.DTOs.Requests
{
    public class CreateNotificationRequestTests
    {
        [Fact(DisplayName = "Should create notification request")]
        public void ShouldCreateNotificationRequest()
            => Assert.NotNull(
                new CreateNotificationRequest()
                {
                    MessageLayout = 15,
                    Recipient = "21973181331",
                    Type = Core.Domain.Enums.ENotificationType.Email,
                    Params = new List<CreateNotificationParamsRequest>
                    {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                    }
                });
    }
}

