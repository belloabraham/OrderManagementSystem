namespace OrderManagementSystem.Domain.Constant;


public static class DiscountThreshold
{
    /// <summary>
    /// The minimum totally spent required to qualify as a VIP customer for discount eligibility.
    /// </summary>
    public const decimal MinimumSpendForDiscount = 10000m;

    /// <summary>
    /// The discount rate applied to eligible orders.
    /// </summary>
    public const decimal DiscountRate = 0.10m;
}