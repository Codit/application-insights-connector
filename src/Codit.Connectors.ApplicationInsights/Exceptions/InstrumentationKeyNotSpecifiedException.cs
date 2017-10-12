using System;
using System.Runtime.Serialization;

namespace Codit.Connectors.ApplicationInsights.Exceptions
{
    [Serializable]
    public class InstrumentationKeyNotSpecifiedException : Exception
    {
        public InstrumentationKeyNotSpecifiedException() : base("No instrumentation key was specified or configured")
        {
        }

        public InstrumentationKeyNotSpecifiedException(string message) : base(message)
        {
        }

        public InstrumentationKeyNotSpecifiedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InstrumentationKeyNotSpecifiedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}