//using ProgrammingLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic; // Make sure to include this for List<>
//using ProgrammingLibrary; // Assuming this is needed for ICommand and related classes

namespace ASE_Programming_Language
{
    public partial class Form1 : Form
    {
        private Interpreter interpreter = new Interpreter();
        private List<ICommand> commandsInLoop;

        public Form1()
        {
            InitializeComponent();

            // Create and add the button for drawing shapes
            Button drawButton = new Button
            {
                Text = "R.Shapes",
                Location = new Point(10, 10)
            };
            drawButton.Click += DrawButton_Click;
            Controls.Add(drawButton);

            // Create and add the button for drawing random circles
            Button btnDrawRandomCircles = new Button
            {
                Text = "R.Circles",
                Location = new Point(200, 290) // Adjust location to avoid overlap
            };
            btnDrawRandomCircles.Click += buttonTestLoop_Click;
            Controls.Add(btnDrawRandomCircles);

            // Initialize commandsInLoop
            commandsInLoop = new List<ICommand>();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            DrawCompositeShape();
        }

        private void DrawCompositeShape()
        {
            Random rnd = new Random();

            // Clearing the PictureBox before drawing new shapes
            pictureBox1.Refresh();

            // Getting the Graphics object from the PictureBox
            using (Graphics graphics = pictureBox1.CreateGraphics())
            {
                // Draw a rectangle
                graphics.DrawRectangle(Pens.Black, new Rectangle(rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(20, 100), rnd.Next(20, 100)));

                // Draw a circle
                int radius = rnd.Next(10, 50);
                graphics.DrawEllipse(Pens.Red, new Rectangle(rnd.Next(0, 100), rnd.Next(0, 100), radius, radius));

                // Draw a line
                graphics.DrawLine(Pens.Blue, new Point(rnd.Next(0, 100), rnd.Next(0, 100)), new Point(rnd.Next(100, 200), rnd.Next(100, 200)));

                // Draw an ellipse
                graphics.DrawEllipse(Pens.Green, new Rectangle(rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(30, 80), rnd.Next(20, 60)));

                // Draw a polygon (triangle)
                Point[] points = {
            new Point(rnd.Next(0, 200), rnd.Next(0, 200)),
            new Point(rnd.Next(0, 200), rnd.Next(0, 200)),
            new Point(rnd.Next(0, 200), rnd.Next(0, 200))
        };
                graphics.DrawPolygon(Pens.Orange, points);

                // Draw an arc
                graphics.DrawArc(Pens.Purple, new Rectangle(rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(50, 100), rnd.Next(50, 100)), 0, rnd.Next(180, 360));
            }
        }

        private ICommand ParseCommand(string commandText)
        {
            string[] parts = commandText.Split(' ');

            // Handle variable assignment

            if (parts.Length == 3 && parts[1].Trim() == "=")
            {
                string variableName = parts[0].Trim();
                if (int.TryParse(parts[2].Trim(), out int value))
                {
                    return new CommandVariableAssignment(variableName, value);
                }
            }

            // Handle drawing command
            else if (parts.Length == 2 && parts[0].Trim().ToLower() == "circle")
            {
                // You might want to set default x and y values or allow users to input them
                int defaultX = 0;
                int defaultY = 0;
                return new CommandDrawCircle(parts[1].Trim(), defaultX, defaultY);
            }
            // Handle initialization command
            else if (parts.Length == 4 && parts[0].Trim().ToLower() == "initialize" && parts[2].Trim().ToLower() == "with")
            {
                string variableName = parts[1].Trim();
                if (int.TryParse(parts[3].Trim(), out int value))
                {
                    return new CommandInitialization(variableName, value);
                }
            }
            if (commandText.StartsWith("loop"))
            {
                string[] loopParts = commandText.Substring(4).Trim().Split(' ');
                if (loopParts.Length >= 2 && int.TryParse(loopParts[0], out int loopCount))
                {
                    string[] loopCommands = loopParts[1].Trim(new char[] { '[', ']' }).Split(';');
                    List<ICommand> commands = new List<ICommand>();
                    foreach (string cmd in loopCommands)
                    {
                        ICommand innerCommand = ParseCommand(cmd.Trim());
                        if (innerCommand != null)
                        {
                            commands.Add(innerCommand);
                        }
                    }
                    return new CommandLoop(loopCount, commands);
                }
            }


            // Return null if the input is not recognized
            return null;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string commandText = textBox1.Text;
            ICommand command = ParseCommand(commandText);

            if (command != null)
            {
                // Check if the command is a graphical command before using graphics
                if (command is CommandDrawCircle)
                {
                    // Use the Graphics object of the PictureBox
                    using (Graphics graphics = pictureBox1.CreateGraphics())
                    {
                        command.Execute(interpreter, graphics);
                    }
                }
                else
                {
                    // For non-graphical commands, use the Execute method without Graphics
                    command.Execute(interpreter);
                }
            }
            else
            {
                // Handle unrecognized command
            }
        }





        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonTestLoop_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            commandsInLoop.Clear();

            // Assuming you want to draw 10 random circles as before
            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(pictureBox1.Width);
                int y = rnd.Next(pictureBox1.Height);
                int size = rnd.Next(10, 100);
                commandsInLoop.Add(new CommandDrawCircle(size.ToString(), x, y));
            }

            // Create and execute the loop command
            CommandLoop loopCommand = new CommandLoop(commandsInLoop.Count, commandsInLoop);

            // Ensure drawing is done in the PictureBox
            pictureBox1.Refresh();  // Clear the PictureBox before drawing new shapes
            using (Graphics graphics = pictureBox1.CreateGraphics())
            {
                foreach (var command in commandsInLoop)
                {
                    if (command is CommandDrawCircle drawCircleCommand)
                    {
                        drawCircleCommand.Execute(interpreter, graphics);
                    }
                }
            }
        }


        // TestLoopCommand();
    }

}






