namespace CreatureSystems
{
    public interface IHungerSystem
    {
        void SatisfyHunger(float calories);
        void SatisfyThirst(float amount);
        float GetHunger();
        float GetThirst();
    }
}