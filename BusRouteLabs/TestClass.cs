using System;
using System.Collections.Generic;
using System.Text;

namespace BusRouteLabs
{
    class TestClass
    {
        Random Rand { get; set; } = new Random();
        int Gaert { get; set; }

        public TestClass()
        {
            Gaert = Rand.Next(0, 5);
        }

        public void TestMethod()
        {
            Gaert = Rand.Next(100, 200);
            Console.WriteLine($"Efter manipulering i metoden: {Gaert}");
        }
        public override string ToString()
        {
            return Gaert.ToString();
        }
    }
}
