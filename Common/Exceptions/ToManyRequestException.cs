namespace Common.Exceptions;

public class ToManyRequestException:Exception
{
    public ToManyRequestException(string message):base(message)
    {
        
    }
}
