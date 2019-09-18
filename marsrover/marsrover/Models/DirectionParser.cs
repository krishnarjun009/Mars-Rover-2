using System;
using System.Collections.Generic;

namespace marsrover.Models
{
    public enum TurnType
    {
        Left,
        Right
    }

    public enum Cardinals
    {
        North,
        East,
        South,
        West,
    }

    public partial class Direction
    {
        public int x { get; }
        public int y { get; }

        public Cardinals left { get; }
        public Cardinals right { get; }
        public Cardinals head { get; }

        public Direction(int x, int y, Cardinals head, Cardinals left, Cardinals right)
        {
            this.head = head;
            this.left = left;
            this.right = right;
            this.x = x;
            this.y = y;
        }
    }

    public class DirectionContext
    {
        private readonly static Dictionary<Cardinals, Direction> directionMap =
            new Dictionary<Cardinals, Direction>()
            {
                { Cardinals.North, new Direction(0, 1, Cardinals.North, Cardinals.West, Cardinals.East) },
                { Cardinals.South, new Direction(0, -1, Cardinals.South, Cardinals.East, Cardinals.West) },
                { Cardinals.West, new Direction(-1, 0, Cardinals.West, Cardinals.South, Cardinals.North) },
                { Cardinals.East, new Direction(1, 0, Cardinals.East, Cardinals.North, Cardinals.South) }
            };

        private readonly static Dictionary<TurnType, Func<Cardinals, Direction>> turnStrategy =
           new Dictionary<TurnType, Func<Cardinals, Direction>>()
           {
                {TurnType.Left, OnTurnLeft},
                {TurnType.Right, OnTurnRight}
           };

        private static Direction OnTurnRight(Cardinals cardinal)
        {
            var dir = directionMap[cardinal];
            return directionMap[dir.right];
        }

        private static Direction OnTurnLeft(Cardinals cardinal)
        {
            var dir = directionMap[cardinal];
            return directionMap[dir.left];
        }

        public Direction GetDirection(Cardinals currentCardinal, TurnType turn)
        {
            return turnStrategy[turn]?.Invoke(currentCardinal);
        }

        public Direction GetDirection(Cardinals currentCardinal)
        {
            return directionMap[currentCardinal];
        }
    }
}
