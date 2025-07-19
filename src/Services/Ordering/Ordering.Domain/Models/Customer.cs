namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    // factory method 
    public static Customer Create(CustomerId Id, string name, string Email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(Email);

        var customer = new Customer
        {
            Id = Id,
            Name = name,
            Email = Email
        };

        return customer;
    }
}
