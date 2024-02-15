using System;
using Notification.API.Application.Commands.Views;

namespace Notification.API.Tests.Application.Commands.Views
{
    public class CreateNotificationCommandViewTests
    {
        [Fact(DisplayName = "Should create notification command view")]
        public void ShouldCreateNotificationCommandView()
            => Assert.NotNull(new CreateNotificationCommandView(Guid.NewGuid(), DateTime.Now));

        [Fact(DisplayName = "Should throw exception when try create a view with empty correlation Id")]
        public void ShouldThrowExceptionWhenTryCreateAViewWithEmptyCorrelationId()
            => Assert.Throws<ArgumentException>(() => new CreateNotificationCommandView(Guid.Empty, DateTime.Now));

        [Fact(DisplayName = "Should throw exception when try create a view with min create date")]
        public void ShouldThrowExceptionWhenTryCreateAViewWithMinCreateDate()
            => Assert.Throws<ArgumentException>(() => new CreateNotificationCommandView(Guid.NewGuid(), DateTime.MinValue));

        [Fact(DisplayName = "Should throw exception when try create a view with max create date")]
        public void ShouldThrowExceptionWhenTryCreateAViewWithMaxCreateDate()
            => Assert.Throws<ArgumentException>(() => new CreateNotificationCommandView(Guid.NewGuid(), DateTime.MaxValue));

    }
}

