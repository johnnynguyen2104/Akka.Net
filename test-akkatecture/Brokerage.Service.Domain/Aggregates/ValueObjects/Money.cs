using Akkatecture.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Aggregates.ValueObjects
{
    public class Money : SingleValueObject<decimal>
    {

        public Money(decimal value) : base(value)
        {
            if (value < 0)
            {
                throw new ArgumentException(nameof(value));
            }
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Value - right.Value);
        }

        public static Money operator *(Money left, Money right)
        {
            return new Money(left.Value * right.Value);
        }

        public static Money operator %(Money left, Money right)
        {
            return new Money(left.Value % right.Value);
        }

        public static Money operator /(Money left, Money right)
        {
            return new Money(left.Value / right.Value);
        }
    }
}
