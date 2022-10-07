using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CitadellesDotIO.Exceptions
{
    public class SpellTargetException : Exception
    {
        public SpellTargetException()
        {
        }

        public SpellTargetException(string message) : base(message)
        {
        }

        public SpellTargetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SpellTargetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
