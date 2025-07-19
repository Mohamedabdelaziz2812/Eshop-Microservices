﻿namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    // ctor
    private ProductId(Guid value) => Value = value;
    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be empty.");
        }

        return new ProductId(value);

    }
}
