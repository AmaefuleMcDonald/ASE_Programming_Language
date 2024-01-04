// Circle Drawing Command
using ASE_Programming_Language;
using System;
using System.Drawing;

public class CommandDrawCircle : ICommand
{
    private string variableName;

    public CommandDrawCircle(string variableName)
    {
        this.variableName = variableName;
    }

    // Implementing the Execute method required by ICommand
    public void Execute(Interpreter interpreter)
    {
        // This method might not be relevant for a drawing command,
        // but it's required by the interface.
        // You can decide how to handle it. One option is to throw an exception.
        throw new NotImplementedException("Non-graphical execution not supported for this command.");
    }

    // Implementing the GetVariableName method required by ICommand
    public string GetVariableName()
    {
        return variableName;
    }

    // The Execute method with Graphics parameter, as previously implemented
    public void Execute(Interpreter interpreter, Graphics graphics)
    {
        int size = interpreter.GetVariableValue(variableName);
        interpreter.DrawCircle(size, graphics);
    }
}

