﻿namespace Shop.Contract.Abstractions.Shared;
public class Error : IEquatable<Error>
{
    public static readonly Error None = new (string.Empty, string.Empty);

    public static readonly Error NullValue = new ("Error.NullValue", "The specified return value is null");

    public string Code { get; private set; }

    public string Message { get; private set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }; 
        return a is null || b is null ? false : a.Equals(b);
    }

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        };
        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}
