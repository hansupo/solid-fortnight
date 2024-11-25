using System;
using System.Windows.Forms;

namespace MyWinFormsApp;

public partial class Form1 : Form
{
    private double num1; // First operand
    private double num2; // Second operand
    private string operation; // Current operation (e.g., +, -, *, /)
    private string input = ""; // User input string
    private Label previewLabel; // Label to display the input and result

    public Form1()
    {
        InitializeComponent(); // Initialize the form components
        this.FormBorderStyle = FormBorderStyle.FixedSingle; // Disable resizing
        CreateCalculatorUI(); // Create the calculator user interface
    }

    private void CreateCalculatorUI()
    {
        // Set the form's width and height
        this.Width = 360;
        this.Height = 343;

        // Create and configure the preview label to display calculations
        previewLabel = new Label
        {
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            Font = new System.Drawing.Font("Arial", 16),
//            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10),
            Margin = new Padding(10)
        };
        Controls.Add(previewLabel); // Add the preview label to the form

        // Create a TableLayoutPanel to arrange buttons in a grid
        var tableLayout = new TableLayoutPanel
        {
            RowCount = 4,
            ColumnCount = 4,
            Dock = DockStyle.Fill,
            AutoSize = false,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            Padding = new Padding(0, 65, 0, 0), // Padding to avoid overlap with the preview label
            Margin = new Padding(10)
        };

        // Create buttons for numbers 1 to 9
        for (int i = 1; i <= 9; i++)
        {
            var button = new Button
            {
                Text = i.ToString(),
                Font = new System.Drawing.Font("Arial", 14),
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Height = 30,
                ForeColor = System.Drawing.Color.DarkSlateGray
            };
            button.Click += NumberButton_Click; // Attach click event handler
            tableLayout.Controls.Add(button, (i - 1) % 3, (i - 1) / 3); // Add button to the layout
        }

        // Create button for 0
        var button0 = new Button
        {
            Text = "0",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        button0.Click += NumberButton_Click; // Attach click event handler
        tableLayout.Controls.Add(button0, 1, 3); // Add button to the layout

        // Create operator buttons (+, -, *, /)
        var buttonDivide = new Button
        {
            Text = "/",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonDivide.Click += OperatorButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonDivide, 3, 0); // Add button to the layout

        var buttonMultiply = new Button
        {
            Text = "*",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonMultiply.Click += OperatorButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonMultiply, 3, 1); // Add button to the layout

        var buttonMinus = new Button
        {
            Text = "-",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonMinus.Click += OperatorButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonMinus, 3, 2); // Add button to the layout

        var buttonPlus = new Button
        {
            Text = "+",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonPlus.Click += OperatorButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonPlus, 3, 3); // Add button to the layout

        // Create Calculate button
        var buttonCalculate = new Button
        {
            Text = "=",
            Font = new System.Drawing.Font("Arial", 14),
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonCalculate.Click += CalculateButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonCalculate, 2, 3); // Add button to the layout

        // Create Clear button
        var buttonClear = new Button
        {
            Text = "CLR",
            Dock = DockStyle.Fill,
            Margin = new Padding(5),
            Height = 50
        };
        buttonClear.Click += ClearButton_Click; // Attach click event handler
        tableLayout.Controls.Add(buttonClear, 0, 3); // Add button to the layout

        // Add the TableLayoutPanel to the form
        Controls.Add(tableLayout);
    }

    // Event handler for number button clicks
    private void NumberButton_Click(object sender, EventArgs e)
    {
        var button = sender as Button; // Get the button that was clicked
        if (button != null)
        {
            input += button.Text; // Append the button text to the input string
            previewLabel.Text = input; // Update the preview label to show current input
        }
    }

    // Event handler for operator button clicks
    private void OperatorButton_Click(object sender, EventArgs e)
    {
        var button = sender as Button; // Get the button that was clicked
        if (button != null)
        {
            if (double.TryParse(input, out num1)) // Parse the current input to num1
            {
                operation = button.Text; // Store the operation (e.g., +, -, *, /)
                previewLabel.Text = $"{num1} {operation}"; // Update the preview label to show the operation
                input = ""; // Clear the input for the next number
            }
        }
    }

    // Event handler for the Calculate button click
    private void CalculateButton_Click(object sender, EventArgs e)
    {
        if (double.TryParse(input, out num2)) // Parse the current input to num2
        {
            // Perform the calculation based on the selected operation
            double result = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => 0
            };

            // Format the result for display
            string formattedResult;
            if (result % 1 == 0) // Check if the result is a whole number
            {
                formattedResult = result.ToString("F0"); // No decimal places
            }
            else
            {
                formattedResult = result.ToString("F4"); // Format to 4 decimal places
                if (result.ToString("G17").Length > formattedResult.Length) // Check for more than 4 decimal places
                {
                    formattedResult += "..."; // Add "..." to indicate more digits
                }
                formattedResult = formattedResult.TrimEnd('0'); // Remove trailing zeros
            }

            // Update the preview label to show the full calculation
            previewLabel.Text = $"{num1} {operation} {num2} = {formattedResult}";
            input = ""; // Clear input after calculation
        }
    }

    // Event handler for the Clear button click
    private void ClearButton_Click(object sender, EventArgs e)
    {
        // Reset all variables and clear the preview label
        input = "";
        num1 = 0;
        num2 = 0;
        operation = "";
        previewLabel.Text = ""; // Clear the preview label
    }
}
