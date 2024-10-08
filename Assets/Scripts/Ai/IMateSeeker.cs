﻿using CreatureSystems;

namespace Ai
{
    public interface IMateSeeker
    {
        Creature SeekMate(Creature creature);
        void BeTargeted(Creature targetCreature, Creature targetingCreature);
    }
}