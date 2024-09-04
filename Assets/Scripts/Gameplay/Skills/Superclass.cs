using System;
using System.Text;

namespace Gameplay.Skills
{
    public class Superclass
    {
        private int _testInt;
        private bool _testBool;
        private string _testIntProp;

        public int TestInt
        {
            get => _testInt;
            set => _testInt = value;
        }

        public virtual float ReturnFloat()
        {
            return 15f;
        }
    }

    public class Subclass : Superclass
    {
        public object fucked;

        public new object TestInt
        {
            get => fucked;
            set => fucked = value;
        }
        
    }

    public enum ShitEnum
    {
        A,
        B,
        C
    }
}