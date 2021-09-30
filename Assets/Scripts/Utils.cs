using UnityEngine;

public static class Utils
{
    public static Vector2 RandomUnitVector()
    {
        float random = Random.Range(0f, 2 * Mathf.PI);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public static Vector3 PickRandomPoint(int radius, Transform transform)
    {
        Vector3 point = Random.insideUnitSphere * radius;
        point.z = 0;
        point += transform.position;
        return point;
    }
}