using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Godot
{
    public static class NodeExtension
    {
        public static void EnsureChildrenReady(this Node node)
        {
            // Check if all children nodes are ready
            bool allChildrenReady = true;
            foreach (Node child in node.GetChildren())
            {
                if (!child.IsInsideTree())
                {
                    allChildrenReady = false;
                    break;
                }
            }

            // If all children nodes are ready, execute the code
            if (!allChildrenReady)
            {
                // If not all children nodes are ready, create a new timer to wait again
                Timer timer = new();
                timer.OneShot = true;
                timer.Connect("timeout", new Callable(node, nameof(EnsureChildrenReady)));
                timer.Start(0);
            }
        }
    }
}
