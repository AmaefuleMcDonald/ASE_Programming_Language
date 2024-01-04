using ASE_Programming_Language;
using System.Drawing;
public class CommandLoop : ICommand
{
    private string loopCountSource;
    private ICommand commandToLoop;

    public CommandLoop(string loopCountSource, ICommand commandToLoop)
    {
        this.loopCountSource = loopCountSource;
        this.commandToLoop = commandToLoop;
    }

    public void Execute(Interpreter interpreter)
    {
        int loopCount;
        // Try parsing as an integer, if not treat it as a variable name
        if (!int.TryParse(loopCountSource, out loopCount))
        {
            loopCount = interpreter.GetVariableValue(loopCountSource);
        }

        for (int i = 0; i < loopCount; i++)
        {
            commandToLoop.Execute(interpreter);
        }
    }

    public void Execute(Interpreter interpreter, Graphics graphics)
    {
        int loopCount;
        if (!int.TryParse(loopCountSource, out loopCount))
        {
            loopCount = interpreter.GetVariableValue(loopCountSource);
        }

        for (int i = 0; i < loopCount; i++)
        {
            // Check if the command supports graphical execution
            if (commandToLoop is CommandDrawCircle)
            {
                commandToLoop.Execute(interpreter, graphics);
            }
            else
            {
                // For commands that do not support graphical execution
                commandToLoop.Execute(interpreter);
            }
        }
    }

    public string GetVariableName()
    {
        return loopCountSource;
    }
}



