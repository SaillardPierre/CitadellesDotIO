namespace CitadellesDotIO.Exceptions
{
    public class CharacterBehaviourException : System.Exception
    {
        public CharacterBehaviourException() { }
        public CharacterBehaviourException(string message) : base(message) { }
        public CharacterBehaviourException(string message, System.Exception inner) : base(message, inner) { }        
    }    
}
