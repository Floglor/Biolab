using Ai;
using Zenject;

namespace Infrastructure
{
    public class StatesInstaller : MonoInstaller
    {
        public State matingState;
        public State remainState;

        public override void InstallBindings()
        {
            Container
                .Bind<State>()
                .FromInstance(matingState);
        }
    }
}