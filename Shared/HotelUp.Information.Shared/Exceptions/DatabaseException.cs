﻿namespace HotelUp.Information.Shared.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message)
    {
    }
}