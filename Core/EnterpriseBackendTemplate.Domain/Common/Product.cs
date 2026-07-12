using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Domain.Common;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    private Product()
    {
        Name = string.Empty;
    }

    public Product(string name, decimal price)
    {
        SetName(name);
        SetPrice(price);
    }

    public void ChangeName(string name)
    {
        SetName(name);
        MarkAsUpdated();
    }

    public void ChangePrice(decimal price)
    {
        SetPrice(price);
        MarkAsUpdated();
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Product name cannot be empty.",
                nameof(name));
        }

        Name = name.Trim();
    }

    private void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "Product price cannot be negative.");
        }

        Price = price;
    }
}

