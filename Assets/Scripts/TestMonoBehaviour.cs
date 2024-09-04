using System.Collections.Generic;
using Gameplay.Skills;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestMonoBehaviour : MonoBehaviour
    {
        private void Start()
        {
            object _object = new object();
            
            Superclass A1 = new Superclass
            {
                TestInt = 1
            };

            Superclass A2 = new Superclass
            {
                TestInt = 2
            };

            Superclass A3 = new Subclass()
            {
                TestInt = new Effector2D()
            };
            
            List<Superclass> superclasses = new List<Superclass>()
            {
                A1, A2, A3
            };

            int sum = 0;
            float floatSum = 0;
           

            for (int i = 0; i < superclasses.Count; i++)
            {
                sum += superclasses[i].TestInt;
                floatSum += superclasses[i].ReturnFloat();
            }

            Debug.Log(sum.ToString());
            Debug.Log(floatSum.ToString());

        }
    }
}