using Stats;

namespace Ai
{
    public interface INeedsDecay
    {
        void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState);
    }
}