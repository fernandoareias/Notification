using System;
using Moq;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Tests
{
    public class NotificationTests
    {
        [Fact(DisplayName = "Should create notification")]
        public void ShouldCreateNotification()
            => Assert.NotNull(new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") }));

        [Fact(DisplayName = "Should create notification")]
        public void ShouldCreateNotificationWithoutParams()
           => Assert.NotNull(new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS));

        [Fact(DisplayName = "Should create notification with params")]
        public void ShouldCreateNotificationWithParams()
            => Assert.NotNull(new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") }));

        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Theory(DisplayName = "Should throw exception when try create notification with correlationId invalid")]
        public void ShouldThrowExceptionWhenTryCreateNotificationWithCorrelationIdInvalid(string correlationId)
            => Assert.Throws<ArgumentException>(() => new Notification(correlationId, "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") }));


        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Theory(DisplayName = "Should throw exception when try create notification with recipient invalid")]
        public void ShouldThrowExceptionWhenTryCreateNotificationWithRecipientInvalid(string recipient)
               => Assert.Throws<ArgumentException>(() => new Notification(Guid.NewGuid().ToString(), recipient, Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") }));


        [InlineData(Core.Domain.Enums.ENotificationType.SMS)]
        [InlineData(Core.Domain.Enums.ENotificationType.Email)]
        [InlineData(Core.Domain.Enums.ENotificationType.Push)]
        [InlineData(Core.Domain.Enums.ENotificationType.WhatsApp)]
        [InlineData(Core.Domain.Enums.ENotificationType.Letter)]
        [Theory(DisplayName = "Should create notification with valid types")]
        public void ShouldCreateNotificationWithValidTypes(Core.Domain.Enums.ENotificationType type)
             => Assert.NotNull(new Notification(Guid.NewGuid().ToString(), "21973181331", type, new List<Parameter> { new Parameter("NAME", "Fernando Areias") }));


        #region Test Send 
        [Fact(DisplayName = "Should send notification with type SMS")]
        public async void ShouldSendNotificationWithTypeSMS()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var smsServices = new Mock<ISMSServices>();

            smsServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(smsServices.Object);

            Assert.Contains(notification.Sent, w => w.Success);
        }


        [Fact(DisplayName = "Should send notification with type Email")]
        public async void ShouldSendNotificationWithTypeEmail()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var emailServices = new Mock<IEmailServices>();

            emailServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(emailServices.Object);

            Assert.Contains(notification.Sent, w => w.Success);
        }


        [Fact(DisplayName = "Should send notification with type Push")]
        public async void ShouldSendNotificationWithTypePush()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var pushServices = new Mock<IEmailServices>();

            pushServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(pushServices.Object);

            Assert.Contains(notification.Sent, w => w.Success);
        }

        [Fact(DisplayName = "Should send notification with type Letter")]
        public async void ShouldSendNotificationWithTypeLetter()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var letterServices = new Mock<IEmailServices>();

            letterServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(letterServices.Object);

            Assert.Contains(notification.Sent, w => w.Success);
        }

        [Fact(DisplayName = "Should send notification with type WhatsApp")]
        public async void ShouldSendNotificationWithTypeWhatsApp()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var whatsappServices = new Mock<IEmailServices>();

            whatsappServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(whatsappServices.Object);

            Assert.Contains(notification.Sent, w => w.Success);
        }
        #endregion

        [Fact(DisplayName = "Should return exception when already send notification")]
        public async void ShouldReturnExceptionWhenAlreadySendNotification()
        {
            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var smsServices = new Mock<ISMSServices>();

            smsServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

            await notification.Send(smsServices.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => notification.Send(smsServices.Object));
        }


        [Fact(DisplayName = "Should publish event when notification send with error")]
        public async void ShouldPublishEventWhenNotificationSendWithError()
        {

            var notification = new Notification(Guid.NewGuid().ToString(), "21973181331", Core.Domain.Enums.ENotificationType.SMS, new List<Parameter> { new Parameter("NAME", "Fernando Areias") });
            var whatsappServices = new Mock<IEmailServices>();

            whatsappServices.Setup(c => c.Process(It.IsAny<Notification>())).ReturnsAsync(new Sent(Guid.NewGuid().ToString(), "XPTO", false));

            await notification.Send(whatsappServices.Object);

            Assert.True(notification.Events.Any());
        }
    }
}

