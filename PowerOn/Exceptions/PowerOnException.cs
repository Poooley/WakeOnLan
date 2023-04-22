namespace WakeOnLan.Exceptions;

using System;

public class PowerOnException : Exception
{
    public PowerOnException() {}

    public PowerOnException(string message) : base(message) {}

    public PowerOnException(string message, Exception inner) : base(message, inner) {}
}