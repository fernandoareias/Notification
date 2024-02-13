using System;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Application
{
    public class OperationView : View
    {
        protected OperationView()
        {

        }
        public OperationView(bool sucess)
        {
            Sucess = sucess;
        }

        public bool Sucess {
            get;
            private set;
        }
    }
}

