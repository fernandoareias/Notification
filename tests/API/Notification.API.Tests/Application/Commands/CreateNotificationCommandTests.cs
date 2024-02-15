using System;
using Notification.API.Application.Commands;
using Notification.API.DTOs.Requests;

namespace Notification.API.Tests.Application.Commands
{
    public class CreateNotificationCommandTests
    {
        [Fact(DisplayName = "Should create notification command")]
        public void ShouldCreateNotificationCommand()
            => Assert.NotNull(new CreateNotificationCommand(new CreateNotificationRequest()
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
            }));


        [Fact(DisplayName = "Should create a valid notification command")]
        public void ShouldCreateValidNotificationCommand()
            => Assert.True(new CreateNotificationCommand(new CreateNotificationRequest()
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
            }).IsValid());


        [Fact(DisplayName = "Shouldn't. create a valid notification command when not has a message layout")]
        public void ShouldntValidCreateNotificationCommandWhenNotHasAMessageLayout()
           => Assert.False(new CreateNotificationCommand(new CreateNotificationRequest()
           {
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
           }).IsValid());

        [Fact(DisplayName = "Shouldn't. create a valid notification command when not has a recipient")]
        public void ShouldntValidCreateNotificationCommandWhenNotHasARecipient()
          => Assert.False(new CreateNotificationCommand(new CreateNotificationRequest()
          {
              MessageLayout = 15,
              Type = Core.Domain.Enums.ENotificationType.Email,
              Params = new List<CreateNotificationParamsRequest>
                  {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                  }
          }).IsValid());



        [InlineData(" ")]
        [InlineData("")]
        [InlineData("973181331")]
        [InlineData("21")]
        [InlineData("212197318133111")]
        [Theory(DisplayName = "Shouldn't. create a valid notification command when recipient is invalid")]
        public void ShouldntValidCreateNotificationCommandWhenRecipientIsInvalid(string recipient)
          => Assert.False(new CreateNotificationCommand(new CreateNotificationRequest()
          {
              MessageLayout = 15,
              Recipient = recipient,
              Type = Core.Domain.Enums.ENotificationType.Email,
              Params = new List<CreateNotificationParamsRequest>
                  {
                        new CreateNotificationParamsRequest()
                        {
                            Key = "NAME",
                            Value = "Fernando"
                        }
                  }
          }).IsValid());


        [Fact(DisplayName = "Shouldn't. create a valid notification command when not has a param key")]
        public void ShouldntValidCreateNotificationCommandWhenNotHasAParamKey()
         => Assert.False(new CreateNotificationCommand(new CreateNotificationRequest()
         {
             MessageLayout = 15,
             Recipient = "21973181331",
             Type = Core.Domain.Enums.ENotificationType.Email,
             Params = new List<CreateNotificationParamsRequest>
                 {
                        new CreateNotificationParamsRequest()
                        {
                            Key = string.Empty,
                            Value = "Fernando"
                        }
                 }
         }).IsValid());


        [Fact(DisplayName = "Shouldn't. create a valid notification command when not has a param value")]
        public void ShouldntValidCreateNotificationCommandWhenNotHasAParamValue()
            => Assert.False(new CreateNotificationCommand(new CreateNotificationRequest()
       {
           MessageLayout = 15,
           Recipient = "21973181331",
           Type = Core.Domain.Enums.ENotificationType.Email,
           Params = new List<CreateNotificationParamsRequest>
               {
                        new CreateNotificationParamsRequest()
                        {
                             Key = "NAME",
                            Value = string.Empty
                        }
               }
       }).IsValid());
    }
}

