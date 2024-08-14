namespace CreatureSystems
{
    public interface IHungerSystem
    {
        void SatisfyHunger(float calories);
        float GetHunger();
    }
}