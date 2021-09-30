using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Zenject;

namespace Infrastructure
{
    public class CreatureInstaller : MonoInstaller
    {
        public CreatureList mainCreatureList;
        public CreatureSpawner creatureSpawner; 
        public Tilemap freeSpaceTiles;

        public override void InstallBindings()
        {
            Container
                .Bind<CreatureList>()
                .FromInstance(mainCreatureList);


            Container
                .Bind<CreatureSpawner>()
                .FromInstance(creatureSpawner);


            Container
                .Bind<Tilemap>()
                .FromInstance(freeSpaceTiles);

        }
    }
}