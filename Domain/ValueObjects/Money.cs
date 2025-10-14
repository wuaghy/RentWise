namespace Domain.Constants
{
    public readonly record struct Money(decimal Value)
    {
        public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
        public static Money operator *(Money a, decimal b) => new(a.Value * b);
        public override string ToString() => Value.ToString("0.00");
    }
}