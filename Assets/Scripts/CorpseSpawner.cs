using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    public static CorpseSpawner Instance;
    public GameObject corpsePrefab;

    private void Start()
    {
        Instance = this;
    }

    public void CreateCorpse(Creature creature)
    {
        GameObject corpse = Instantiate(corpsePrefab, creature.transform.position, Quaternion.identity);
        corpse.GetComponent<SpriteRenderer>().sprite = creature.GetComponent<SpriteRenderer>().sprite;
        corpse.GetComponent<SpriteRenderer>().color = Color.red;
        corpse.GetComponent<Corpse>().startSize = creature.ReturnCorpseSize();
        corpse.GetComponent<Corpse>().age = 0;
        FoodController.Instance.corpses.Add(corpse.GetComponent<Corpse>());
    }
}