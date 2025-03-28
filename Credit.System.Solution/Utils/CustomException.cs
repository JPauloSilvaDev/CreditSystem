[Serializable]
public class CSException : Exception
{
    // Default constructor
    public CSException() { }

    // Constructor that accepts a message
    public CSException(string message)
        : base(message) { }

    // Constructor that accepts a message and an inner exception
    public CSException(string message, Exception inner)
        : base(message, inner) { }

    // Protected constructor for serialization
    protected CSException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
