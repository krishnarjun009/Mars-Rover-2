using marsrover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marsrover
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DirectionContext();

            var direction = context.GetDirection(Cardinals.North);

            Console.WriteLine("Current direction " + direction.head);

            direction = context.GetDirection(direction.head, TurnType.Left);

            Console.WriteLine("After Turn " + direction.head);
        }
    }
}
