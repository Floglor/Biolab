﻿using CreatureSystems;

namespace Ai
{
    public interface IMatingCreateNewCreature
    {
        void CreateCreature(Creature child, Creature mother, Creature father, float mutationModifier);
    }
}