public interface IAlive
{
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void TakeDamage(float amount);
    void HealYourself(float amount);
}
