using UnityEngine;

public class Corpse : MonoBehaviour
{
    public float startSize;
    public float size;
    public float age;

    private void Start()
    {
        size = startSize;
    }

    private void Update()
    {
        age += Time.deltaTime;
        if (age >= GlobalValues.Instance.corpseMaxAge) Perish();
    }

    private void OnDestroy()
    {
        FoodController.Instance.corpses.Remove(this);
    }

    public bool GetEaten(float chompSize, Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) >= Creature.EatingRange) return false;
        if (size <= Creature.ChompSize) return false;

        size -= chompSize;
        CheckForPerish();
        return true;
    }

    private void CheckForPerish()
    {
        if (size <= Creature.ChompSize) Perish();
    }

    private void Perish()
    {
        Destroy(gameObject, 0.1f);
    }
}