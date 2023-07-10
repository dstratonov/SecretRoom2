using System;

[System.Serializable]
public class Stat
{
    public float BaseValue { get; private set; }
    public float CurrentValue { get; private set; }

    public Stat(float baseValue)
    {
        BaseValue = baseValue;
        CurrentValue = baseValue;
    }

    public void Increase(float amount)
    {
        CurrentValue += amount;
        if (CurrentValue > BaseValue)
        {
            CurrentValue = BaseValue;
        }
    }

    public void Decrease(float amount)
    {
        CurrentValue -= amount;
        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }
    }

    // For percentage increases/decreases
    public void IncreaseByPercentage(float percentage)
    {
        BaseValue += (BaseValue * percentage);
        CurrentValue = BaseValue;
    }

    public void DecreaseByPercentage(float percentage)
    {
        BaseValue -= (BaseValue * percentage);
        BaseValue = Math.Max(0.0f, BaseValue);
        CurrentValue = Math.Min(CurrentValue, BaseValue);
    }
}