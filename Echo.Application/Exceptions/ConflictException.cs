namespace Echo.Application.Exceptions
{
    public class ConflictException : CustomException
    {
        public virtual string Code { get; }
        public ConflictException(string message, string code = default!) : base(message)
        {
            Code = code;
        }
    }
}