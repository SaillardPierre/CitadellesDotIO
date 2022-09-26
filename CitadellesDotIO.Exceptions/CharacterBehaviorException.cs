using System;

namespace CitadellesDotIO.Exceptions
{
    public class CharacterBehaviourException : Exception
    {
        public CharacterBehaviourException() { }
        public CharacterBehaviourException(string message) : base(message) { }
        public CharacterBehaviourException(string message, Exception inner) : base(message, inner) { }        
    }    
}
