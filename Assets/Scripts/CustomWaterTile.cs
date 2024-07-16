using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomWaterTile
{
    private Vector3Int _cellPosition;
    private Tile _linkedTile;
    private TileSlots _tileSlots;
    
    public Vector3 WorldPosition;
    public bool IsFull => _tileSlots.IsFull();

    public CustomWaterTile(Tile linkedTile, Vector3Int cellPosition, Vector3 worldPosition)
    {
        _linkedTile = linkedTile;
        _cellPosition = cellPosition;
        WorldPosition = worldPosition;
        _tileSlots = new TileSlots();
    }

    public bool AddCreature(Creature creature)
    {
        return _tileSlots.TryAddCreature(creature);
    }
}

internal class TileSlots
{
    private Creature[] _drinkingCreatures;

    public TileSlots()
    {
        _drinkingCreatures = new Creature[GlobalValues.Instance.MaxCreaturesOnTile];
    }

    public bool TryAddCreature(Creature creature)
    {
        if (IsFull())
        {
            Debug.LogError("SearchForWater: attempted to add creature to a full tile");
            return false;
        }
        
        for (int i = 0; i < _drinkingCreatures.Length; i++)
        {
            if (_drinkingCreatures[i] == null)
            {
                _drinkingCreatures[i] = creature;

                void OnCreatureOnDrinkingFinished()
                {
                    ClearSlot(creature);
                    creature.onDrinkingFinsihed -= OnCreatureOnDrinkingFinished;
                }

                creature.onDrinkingFinsihed += OnCreatureOnDrinkingFinished;
                
                return true;
            }
        }
        
        return false;
    }
    
    public void ClearSlot(Creature creature)
    {
        for (int i = 0; i < _drinkingCreatures.Length; i++)
        {
            if (_drinkingCreatures[i] == creature)
                _drinkingCreatures[i] = null;

            break;
        }
    }
    
    public void ClearAllSlots()
    {
        for (int i = 0; i < _drinkingCreatures.Length; i++)
        {
            _drinkingCreatures[i] = null;
        }
    }

    public bool IsFull()
    {
        bool noFreeSlots = true;

        foreach (Creature drinkingCreature in _drinkingCreatures)
        {
            if (drinkingCreature == null)
            {
                noFreeSlots = false;
            }
        }

        Debug.Log(noFreeSlots);
        return noFreeSlots;
    }

}