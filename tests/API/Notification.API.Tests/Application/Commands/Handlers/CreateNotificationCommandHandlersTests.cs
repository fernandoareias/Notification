using System;
using Moq;
using Notification.API.Application.Commands;
using Notification.API.Application.Commands.Handlers;
using Notification.API.Application.Commands.Views;
using Notification.API.DTOs.Requests;
using Notification.Core.MessageBus.Services.Interfaces;

namespace Notification.API.Tests.Application.Commands.Handlers
{
    public class CreateNotificationCommandHandlersTests
    {
        [Fact(DisplayName = "Should create a SMS notification")]
        public async void ShouldCreateASMSNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
            {
                MessageLayout = 15,
                Recipient = "21973181331",
                Type = Core.Domain.Enums.ENotificationType.SMS,
                Params = new List<CreateNotificationParamsRequest>
                    {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                    }
            });


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(view);

        }


        [Fact(DisplayName = "Should return correlationid when create notification")]
        public async void ShouldReturnCorrelationIdWhenCreateNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
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


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken()) as CreateNotificationCommandView;

            Assert.NotNull(view?.CorrelationId);

        }

        [Fact(DisplayName = "Should return runtime when create notification")]
        public async void ShouldReturnCreateDateWhenCreateNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
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


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken()) as CreateNotificationCommandView;

            Assert.NotNull(view?.Runtime);

        }


        [Fact(DisplayName = "Should create a Email notification")]
        public async void ShouldCreateAEmailNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
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


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(view); 
        }


        [Fact(DisplayName = "Should create a Letter notification")]
        public async void ShouldCreateALetterNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
            {
                MessageLayout = 15,
                Recipient = "21973181331",
                Type = Core.Domain.Enums.ENotificationType.Letter,
                Params = new List<CreateNotificationParamsRequest>
                    {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                    }
            });


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(view);
        }

        [Fact(DisplayName = "Should create a Push notification")]
        public async void ShouldCreateAPushNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
            {
                MessageLayout = 15,
                Recipient = "21973181331",
                Type = Core.Domain.Enums.ENotificationType.Push,
                Params = new List<CreateNotificationParamsRequest>
                    {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                    }
            });


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(view);
        }


        [Fact(DisplayName = "Should create a WhatsApp notification")]
        public async void ShouldCreateAWhatsAppNotification()
        {
            var messageBus = new Mock<IMessageBus>();

            messageBus.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CreateNotificationCommand>()));

            var command = new CreateNotificationCommand(new CreateNotificationRequest()
            {
                MessageLayout = 15,
                Recipient = "21973181331",
                Type = Core.Domain.Enums.ENotificationType.WhatsApp,
                Params = new List<CreateNotificationParamsRequest>
                    {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                    }
            });


            var handler = new CreateNotificationCommandHandlers(messageBus.Object);

            var view = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(view);
        }
    }
}

