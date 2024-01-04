using ASE_Programming_Language;
using System.Collections.Generic;

public class CommandIf : ICommand
{
    private readonly string conditionVariableName;
    private readonly List<ICommand> commands;

    public CommandIf(string conditionVariableName, List<ICommand> commands)
    {
        this.conditionVariableName = conditionVariableName;
        this.commands = commands;
    }

    public void Execute(Interpreter interpreter)
    {
        if (interpreter.GetVariableValue(conditionVariableName) != 0)
        {
            foreach (var command in commands)
            {
                command.Execute(interpreter);
            }
        }
    }

    public string GetVariableName()
    {
        return conditionVariableName;
    }
}
