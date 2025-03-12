namespace Echo.Application.Exceptions
{
    public class PostRateLimitException : CustomException
    {
        public PostRateLimitException(string message) : base(message)
        {

        }
    }

}