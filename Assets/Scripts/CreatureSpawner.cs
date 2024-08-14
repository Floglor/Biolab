using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;


public enum CreatureType
{
    Carnivore,
    Herbivore
}

public enum Config
{
    CarnivoresNumber,
    HerbivoresNumber,
    MutationModifier
}

public class CreatureSpawner : MonoBehaviour
{
    public static CreatureSpawner Instance;
    public float mutationModifier;
    public List<GameObject> creaturePrefabs;
    public bool _isTestingSingularSpawn = false;
    private IMatingCreateNewCreature _createCreatureBehaviour;

    private GameConfigValues _gameConfigValues;

    private Tilemap _freeSpaceTiles;

    [Inject]
    private void Construct(Tilemap freeSpaceTilemap)
    {
        _freeSpaceTiles = freeSpaceTilemap;
    }

    private void Start()
    {
        Instance = this;
        _createCreatureBehaviour = GetComponent<IMatingCreateNewCreature>();
        GetGameConfigValues();
        SpawnStartingCreatures();
    }

    private void GetGameConfigValues()
    {
        if (!_isTestingSingularSpawn)
        {
            _gameConfigValues = new GameConfigValues
            {
                CarnivoresNumber = PlayerPrefs.GetInt(Config.CarnivoresNumber.ToString()),
                HerbivoresNumber = PlayerPrefs.GetInt(Config.HerbivoresNumber.ToString()),
                MutationModifier = PlayerPrefs.GetFloat(Config.MutationModifier.ToString())
            };
        }
        else
        {
            _gameConfigValues = new GameConfigValues
            {
                CarnivoresNumber = 1,
                HerbivoresNumber = 0,
                MutationModifier = PlayerPrefs.GetFloat(Config.MutationModifier.ToString())
            };
        }
        mutationModifier = _gameConfigValues.MutationModifier;
    }

    private void SpawnStartingCreatures()
    {
        List<Vector3> freeTilesWorldPositions = new List<Vector3>();

        foreach (Vector3Int vector3Int in _freeSpaceTiles.cellBounds.allPositionsWithin)
        {
            freeTilesWorldPositions.Add(_freeSpaceTiles.CellToWorld(vector3Int));
        }

        if (_gameConfigValues.CarnivoresNumber > 0)
            for (int i = 0; i < _gameConfigValues.CarnivoresNumber; i++)
            {
                int tileIndex = Random.Range(0, freeTilesWorldPositions.Count);
                SpawnInitialCreature(CreatureType.Carnivore, freeTilesWorldPositions[tileIndex]);
                freeTilesWorldPositions.RemoveAt(tileIndex);
            }

        if (_gameConfigValues.HerbivoresNumber > 0)
            for (int i = 0; i < _gameConfigValues.HerbivoresNumber; i++)
            {
                int tileIndex = Random.Range(0, freeTilesWorldPositions.Count);
                SpawnInitialCreature(CreatureType.Herbivore, freeTilesWorldPositions[tileIndex]);
                freeTilesWorldPositions.RemoveAt(tileIndex);
            }
    }

    private void SpawnInitialCreature(CreatureType creatureType, Vector3 position)
    {
        GameObject creature = Instantiate(
            creatureType == CreatureType.Carnivore ? creaturePrefabs[0] : creaturePrefabs[1], position,
            Quaternion.identity);

        creature.GetComponent<Creature>().isMale = Random.Range(0f, 1f) >= 0.5;
    }

    public void SpawnNewCreature(Creature mother, Creature father, Vector3 place)
    {
        if (mother == null) Debug.LogError("Spawn New Creature: mother is null");
        if (father == null) Debug.LogError("Spawn New Creature: father is null");

        GameObject creaturePrefab = creaturePrefabs.FirstOrDefault(prefab
            => prefab.GetComponent<Creature>().creatureType == mother.creatureType);

        GameObject creature = Instantiate(creaturePrefab, place, Quaternion.identity);
        Creature creatureComponent = creature.GetComponent<Creature>();

        _createCreatureBehaviour.CreateCreature(creatureComponent, mother, father, mutationModifier);
    }
}