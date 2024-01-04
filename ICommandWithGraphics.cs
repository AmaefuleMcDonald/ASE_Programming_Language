using System.Drawing;

namespace ASE_Programming_Language
{
    public class CommandDrawCircle : ICommandWithGraphics
    {
        // Fields, constructor, and other members...

        public void Execute(Interpreter interpreter)
        {
            // Non-graphical execution logic (if any)
        }

        public void Execute(Interpreter interpreter, Graphics graphics)
        {
            // Graphical execution logic
        }

        public string GetVariableName()
        {
            // Return the variable name associated with this command
        }
    }
}
