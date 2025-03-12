namespace Echo.Application.Exceptions
{
    public class DuplicatedRequestException : CustomException
    {
        public DuplicatedRequestException(string message) : base(message)
        {

        }
    }

}