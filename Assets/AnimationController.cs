using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator Animator;

    private AIPath _aiPath;
    private static readonly int DesiredVelocityX = Animator.StringToHash("DesiredVelocityX");
    private static readonly int DesiredVelocityY = Animator.StringToHash("DesiredVelocityY");

    private void Start()
    {
        _aiPath = GetComponent<Creature>().aiPath;
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        Animator.SetFloat(DesiredVelocityX, _aiPath.desiredVelocity.x);
        Animator.SetFloat(DesiredVelocityY, _aiPath.desiredVelocity.y);
    }
}
