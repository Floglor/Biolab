using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UI
{
    [RequireComponent(typeof(FoodController))]
    [RequireComponent(typeof(Tilemap))]
    public class FoodOnClickInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionPrefab;
        [SerializeField] private int _sortingLayer;
    

        private GameObject _selectionObj;
        private Tilemap _foodTileMap;
        private FoodController _foodController;

        private void InstantiateSelectionObject()
        {
            _selectionObj = Instantiate(_selectionPrefab, transform);
            _selectionObj.GetComponent<SpriteRenderer>().color = Color.clear;
            _selectionObj.GetComponent<SpriteRenderer>().sortingOrder = 3;

        }

        private void Deselect()
        {
            _selectionObj.GetComponent<SpriteRenderer>().color = Color.clear;
            DebugUI.Instance.ClearDebugText();
        }

        private void SelectTile(Vector3Int tpos, Vector3 worldPosition)
        {
            Vector3 tileAnchor = _foodTileMap.tileAnchor;
            Vector3 position = new Vector3(tpos.x + tileAnchor.x, tpos.y + tileAnchor.y, 0);
            _selectionObj.transform.position = position;
            _selectionObj.GetComponent<SpriteRenderer>().color = Color.white;
        
            string foodText = _foodController
                .GetFoodTile(_foodTileMap.CellToWorld(tpos))
                .FoodLevel
                .ToString(CultureInfo.CurrentCulture);
        
            DebugUI.Instance.SetDebugText($"Food Level: {foodText}");
        }

        private void Start()
        {
            InstantiateSelectionObject();
            _foodTileMap = GetComponent<Tilemap>();
            _foodController = GetComponent<FoodController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int cellPosition = _foodTileMap.WorldToCell(worldPoint);

                // Try to get a tile from cell position
                TileBase tile = _foodTileMap.GetTile(cellPosition);

                if (tile)
                {
                    SelectTile(cellPosition, worldPoint);
                }
                else
                {
                    Deselect();
                }
            }
        }
    }
}
