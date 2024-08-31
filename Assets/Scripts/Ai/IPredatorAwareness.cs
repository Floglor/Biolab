using System.Collections;
using CreatureSystems;

namespace Ai
{
    public interface IPredatorAwareness
    {
        IEnumerator BeAware(Creature creature);
    }
}