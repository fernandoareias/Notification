using System;
using Notification.Worker.Domain.Entities;

namespace Notification.Worker.Domain.Tests.Entities
{
    public class SentEvents
    {
        [Fact(DisplayName = "Should create sent")]
        public void ShouldCreateSent()
            => Assert.NotNull(new Sent(Guid.NewGuid().ToString(), "XPTO", true));

        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Theory(DisplayName = "Should dont create sent when externalid is invalid")]
        public void ShouldDontCreateSentWhenExternalIdIsInvalid(string sent)
            => Assert.Throws<ArgumentException>(() => new Sent(sent, "XPTO", true));

        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Theory(DisplayName = "Should dont create sent when partner system is invalid")]
        public void ShouldDontCreateSentWhenPartnerSystemIsInvalid(string partner)
           => Assert.Throws<ArgumentException>(() => new Sent(Guid.NewGuid().ToString(), partner, true));
    }
}

