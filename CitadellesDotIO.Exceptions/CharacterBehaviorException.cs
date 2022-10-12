using System;
using System.Runtime.Serialization;

namespace CitadellesDotIO.Exceptions
{
    [Serializable]
    public class CharacterBehaviourException : Exception
    {
        public CharacterBehaviourException() { }
        public CharacterBehaviourException(string message) 
            : base(message) { }
        public CharacterBehaviourException(string message, Exception inner) 
            : base(message, inner) { }

        protected CharacterBehaviourException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }    
}
