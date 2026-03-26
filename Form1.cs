namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        private int operandA;
        private int operandB;

        public Form1()
        {
            InitializeComponent();

            // Wire all buttons to a single handler so every press is shown in txtInputWindows
            btnNumber0.Click += AnyButton_Click;
            btnNumber1.Click += AnyButton_Click;
            btnNumber2.Click += AnyButton_Click;
            btnNumber3.Click += AnyButton_Click;
            btnNumber4.Click += AnyButton_Click;
            btnNumber5.Click += AnyButton_Click;
            btnNumber6.Click += AnyButton_Click;
            btnNumber7.Click += AnyButton_Click; // designer also wired to button4_Click
            btnNumber8.Click += AnyButton_Click;
            btnNumber9.Click += AnyButton_Click;

            btnDot.Click += AnyButton_Click;
            btnNegativeSign.Click += AnyButton_Click;

            btnOperatorAdd.Click += AnyButton_Click;
            btnOperatorSubtract.Click += AnyButton_Click;
            btnOperatorMultiply.Click += AnyButton_Click;
            btnOperatorDivide.Click += AnyButton_Click;

            btnFunctionCE.Click += AnyButton_Click;
            btnFunctionC.Click += AnyButton_Click; // designer also wired to button2_Click
            btnFunctionDel.Click += AnyButton_Click;

            btnResult.Click += AnyButton_Click;
        }

        // Designer placeholders forward to common handler to avoid breaking designer event wiring.
        private void button2_Click(object sender, EventArgs e)
        {
            AnyButton_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnyButton_Click(sender, e);
        }

        // Common handler: append pressed button text to txtInputWindows and show last pressed in txtOutputWindows.
        // If '=' (btnResult) is pressed, evaluate the expression (two integer operands) and show result in txtOutputWindows
        // while leaving txtInputWindows unchanged.
        private void AnyButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button b)
                return;

            var text = b.Text ?? string.Empty;

            // If '=' pressed, evaluate
            if (b == btnResult)
            {
                // Evaluate expression in txtInputWindows. Expect format: <int><op><int>
                var raw = txtInputWindows.Text ?? string.Empty;

                // If previous result already appended (contains '='), strip it
                var prevEq = raw.IndexOf('=');
                var input = prevEq >= 0 ? raw.Substring(0, prevEq) : raw;

                // Find operator position: first occurrence of one of these symbols
                var ops = new[] { "+", "-", "×", "÷", "*", "/" };
                int opIdx = -1;
                string op = null;
                foreach (var candidate in ops)
                {
                    var idx = input.IndexOf(candidate);
                    if (idx >= 0)
                    {
                        opIdx = idx;
                        op = candidate;
                        break;
                    }
                }

                string resultText;

                if (opIdx < 0 || string.IsNullOrEmpty(op))
                {
                    // No operator: try parse whole input and show it
                    if (int.TryParse(input, out var only))
                    {
                        resultText = only.ToString();
                    }
                    else
                    {
                        resultText = "Error";
                    }

                    // Append '=' and result to input and also show in output
                    txtInputWindows.Text = input + "=" + resultText;
                    txtOutputWindows.Text = resultText;
                    return;
                }

                var left = input.Substring(0, opIdx);
                var right = input.Substring(opIdx + op.Length);

                if (!int.TryParse(left, out operandA) || !int.TryParse(right, out operandB))
                {
                    resultText = "Error";
                    txtInputWindows.Text = input + "=" + resultText;
                    txtOutputWindows.Text = resultText;
                    return;
                }

                try
                {
                    int result = op switch
                    {
                        "+" => operandA + operandB,
                        "-" => operandA - operandB,
                        "×" or "*" => operandA * operandB,
                        "÷" or "/" => operandB == 0 ? throw new DivideByZeroException() : operandA / operandB,
                        _ => throw new InvalidOperationException()
                    };

                    resultText = result.ToString();
                    txtInputWindows.Text = input + "=" + resultText;
                    txtOutputWindows.Text = resultText;
                }
                catch (DivideByZeroException)
                {
                    resultText = "Cannot divide by zero";
                    txtInputWindows.Text = input + "=" + resultText;
                    txtOutputWindows.Text = resultText;
                }
                catch
                {
                    resultText = "Error";
                    txtInputWindows.Text = input + "=" + resultText;
                    txtOutputWindows.Text = resultText;
                }

                return;
            }

            // For any other button: append its text to the input textbox and show it in the output textbox
            txtInputWindows.Text += text;
            txtOutputWindows.Text = text;
        }
    }
}
