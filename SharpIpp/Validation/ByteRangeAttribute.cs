using System;
using System.ComponentModel.DataAnnotations;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ByteRangeAttribute : ByteMultiRangeAttribute
{
    public int Minimum { get; }
    public int Maximum { get; }

    public ByteRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
}
