namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        private int operandA;
        private int operandB;
        private string currentOperator;
        private string currentOperandText = string.Empty;
        private bool waitingForOperandBStart = false;

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
                    // Prepare result text. For division use double and show decimals up to 6 places.
                    if (op == "÷" || op == "/")
                    {
                        if (operandB == 0)
                            throw new DivideByZeroException();

                        double dres = (double)operandA / operandB;
                        // format with up to 6 decimal places, removing trailing zeros
                        resultText = dres.ToString("0.######");
                    }
                    else
                    {
                        int ires = op switch
                        {
                            "+" => operandA + operandB,
                            "-" => operandA - operandB,
                            "×" or "*" => operandA * operandB,
                            _ => throw new InvalidOperationException()
                        };

                        resultText = ires.ToString();
                    }

                    // Ensure the combined display (input + '=' + result) does not exceed available space.
                    const int MaxTotalLength = 20; // keep within textbox width
                    var display = input + "=" + resultText;
                    if (display.Length > MaxTotalLength)
                    {
                        int allowedResultLen = Math.Max(1, MaxTotalLength - input.Length - 1);

                        // If decimal, try reducing precision first
                        if (resultText.Contains('.'))
                        {
                            if (double.TryParse(resultText, out var dv))
                            {
                                // try decreasing precision until it fits
                                bool fitted = false;
                                for (int prec = 6; prec >= 0; prec--)
                                {
                                    var fmt = prec == 0 ? "0" : "0." + new string('#', prec);
                                    var candidate = dv.ToString(fmt);
                                    if (candidate.Length <= allowedResultLen)
                                    {
                                        resultText = candidate;
                                        fitted = true;
                                        break;
                                    }
                                }

                                if (!fitted && resultText.Length > allowedResultLen)
                                {
                                    resultText = resultText.Substring(0, allowedResultLen);
                                }
                            }
                            else if (resultText.Length > allowedResultLen)
                            {
                                resultText = resultText.Substring(0, allowedResultLen);
                            }
                        }
                        else
                        {
                            if (resultText.Length > allowedResultLen)
                                resultText = resultText.Substring(0, allowedResultLen);
                        }

                        display = input + "=" + resultText;
                    }

                    txtInputWindows.Text = display;
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

            // Operator buttons: store operandA, set operator state, append to input, do not show in output
            if (b == btnOperatorAdd || b == btnOperatorSubtract || b == btnOperatorMultiply || b == btnOperatorDivide)
            {
                // store current operandA from currentOperandText
                if (!int.TryParse(currentOperandText, out operandA))
                {
                    operandA = 0;
                }

                currentOperator = b.Text;
                waitingForOperandBStart = true; // next number press should start operandB

                // append operator to input (but do not change output)
                txtInputWindows.Text += text;
                return;
            }

            // For any other button: append its text to the input textbox
            txtInputWindows.Text += text;

            // For numeric-like buttons (digits, dot, +/-) update currentOperandText and show full operand
            bool isDigitButton = (b == btnNumber0 || b == btnNumber1 || b == btnNumber2 || b == btnNumber3 || b == btnNumber4 || b == btnNumber5 || b == btnNumber6 || b == btnNumber7 || b == btnNumber8 || b == btnNumber9);
            if (isDigitButton || b == btnDot || b == btnNegativeSign)
            {
                if (waitingForOperandBStart)
                {
                    // start operandB fresh
                    currentOperandText = text;
                    waitingForOperandBStart = false;
                }
                else
                {
                    // continue building current operand
                    currentOperandText += text;
                }

                txtOutputWindows.Text = currentOperandText;
                return;
            }

            // For other non-operator buttons, just show the last pressed button text in output
            txtOutputWindows.Text = text;
        }
    }
}
