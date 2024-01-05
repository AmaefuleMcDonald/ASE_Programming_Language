// IfClass.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Contexts;
using ASE_Programming_Language; // Assuming this is the namespace your classes are in

public class IfClass
{
    private CommandDrawCircle drawCircleCommand;
    private CommandLoop loopCommand;
    private Interpreter interpreter;
    private Graphics graphics; // Add a Graphics field

    public IfClass(Graphics graphics)
    {
        this.graphics = graphics;
        // Initialize the commands and interpreter
        drawCircleCommand = new CommandDrawCircle("sizeArgument", 100, 100); // Example arguments
        loopCommand = new CommandLoop(5, new List<ICommand>()); // Example loop count and command list
        interpreter = new Interpreter();
    }

    public void ExampleMethod(string conditionText)
    {
        int number = 10; // Example variable

        // Parse the condition
        string[] parts = conditionText.Split(' ');
        if (parts.Length == 3)
        {
            string variableName = parts[0];
            string operatorSymbol = parts[1];
            if (int.TryParse(parts[2], out int value))
            {
                bool conditionMet = false;

                // Check the variable and compare based on the operator
                if (variableName == "number")
                {
                    switch (operatorSymbol)
                    {
                        case ">":
                            conditionMet = number > value;
                            break;
                        case "<":
                            conditionMet = number < value;
                            break;
                            // Add other operators as needed
                    }
                }

                if (conditionMet)
                {
                    Console.WriteLine($"Condition met: {conditionText}");
                    // Execute your commands here
                    drawCircleCommand.Execute(interpreter, graphics); // Example command
                    // Add other operations as needed
                }
                else
                {
                    Console.WriteLine($"Condition not met: {conditionText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid condition format.");
        }
    }
}
