[Serializable]
public class CSException : Exception
{
    public CSException() { }

    public CSException(string message)
        : base(message) { }

    public CSException(string message, Exception inner)
        : base(message, inner) { }

    protected CSException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
