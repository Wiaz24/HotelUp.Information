namespace HotelUp.Information.Shared.Exceptions;

public abstract class BusinessRuleException : Exception
{
    protected BusinessRuleException(string message) : base(message)
    {
    }
}