using System;

namespace Entities
{
    public enum Team
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public static class TeamLookup
    {
        public static Team Get(ulong number)
        {
            return number switch
            {
                0 => Team.Red,
                1 => Team.Green,
                2 => Team.Blue,
                3 => Team.Yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(number), number, null)
            };
        }
    }
}