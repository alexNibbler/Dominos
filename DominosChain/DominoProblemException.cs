namespace DominosChain;

public class DominoProblemException : Exception
{
    private string _dominoProblemExceptionMessage;
    public DominoProblemException(string message)
    {
        _dominoProblemExceptionMessage = message;
    }

    public override string Message => _dominoProblemExceptionMessage;
}