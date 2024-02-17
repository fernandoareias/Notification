using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Notification.API.Application.Commands.Views;
using Notification.API.Controllers;
using Notification.API.DTOs.Requests;
using Notification.Core.Common.CQRS;
using Notification.Core.Common.Validators;
using Notification.Core.Common.Validators.Interfaces;
using Notification.Core.Mediator.Interfaces;

namespace Notification.API.Tests.Controllers
{
    public class NotificationControllerTests
    {
        [Fact(DisplayName = "Should return created when create notification")]
        public async void ShouldReturnCreatedWhenCreateNotification()
        {
            var mediatorHandler = new Mock<IMediatorHandler>();
            mediatorHandler.Setup(c => c.Send<Command>(It.IsAny<Command>())).ReturnsAsync(new CreateNotificationCommandView(Guid.NewGuid(), DateTime.Now));

            var validatorServices = new Mock<IValidatorServices>();

            var controller = new NotificationController(mediatorHandler.Object, validatorServices.Object);

            var response = await controller.Create(new CreateNotificationRequest()
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

            Assert.True(response is CreatedResult);
        }


        [Fact(DisplayName = "Should return bad request when has error")]
        public async void ShouldReturnBadRequestWhenHasErro()
        {
            var mediatorHandler = new Mock<IMediatorHandler>();
            mediatorHandler.Setup(c => c.Send<Command>(It.IsAny<Command>())).ReturnsAsync((CreateNotificationCommandView)null);

            var validatorServices = new ValidatorServices();

            validatorServices.AddError("erro", "erro"); 
            var controller = new NotificationController(mediatorHandler.Object, validatorServices);

            var response = await controller.Create(new CreateNotificationRequest()
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

            Assert.True(response is BadRequestObjectResult);
        }
    }
}

