namespace LoLLauncher
{
    public enum ErrorType
    {
        Password,
        AuthKey,
        Handshake,
        Connect,
        Login,
        Invoke,
        Receive,
        General
    }

    public class Error
    {
        public string ErrorCode = "";
        public string Message = "";
        public ErrorType Type;
    }
}