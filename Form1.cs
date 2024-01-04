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

namespace ASE_Programming_Language
{
    public partial class Form1 : Form
    {
        private Interpreter interpreter = new Interpreter();
        public Form1()
        {
            InitializeComponent();
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
                return new CommandDrawCircle(parts[1].Trim());
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
       




    }


}



