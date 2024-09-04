using System;

namespace CreatureSystems
{
    public interface IReceiveDeathAction
    {
        void SetDeathAction(Action<DeathReason> deathAction);
    }
}