using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LegendsOfKesmaiSurvival.Core.Exceptions
{
    [Serializable()]
    public class UnhandledException : Exception
    {
        public UnhandledException()
        {
        }

        public UnhandledException(string message)
            : base(message)
        {
            
        }

        public UnhandledException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
