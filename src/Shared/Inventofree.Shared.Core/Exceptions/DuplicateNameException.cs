using System;

namespace Inventofree.Shared.Core.Exceptions;

public class DuplicateNameException : Exception
{
    public DuplicateNameException(string message) : base(message)
    {
        
    }
}