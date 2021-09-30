using UnityEngine;

public class Bunny : Creature
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, eyesight);
    }
}