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
            // btnNumber7 is wired in the designer to button4_Click; do not add a second handler here.
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

            // Handle clear/delete buttons first (they should not append their text)
            if (b == btnFunctionC)
            {
                // Full reset: clear both boxes and internal state
                txtInputWindows.Text = string.Empty;
                txtOutputWindows.Text = string.Empty;
                operandA = 0;
                operandB = 0;
                currentOperator = null;
                currentOperandText = string.Empty;
                waitingForOperandBStart = false;
                return;
            }

            if (b == btnFunctionCE)
            {
                // Clear Entry: remove only the most recently entered operand (do not remove operator)
                var rawFull = txtInputWindows.Text ?? string.Empty;
                var eq = rawFull.IndexOf('=');
                var input = eq >= 0 ? rawFull.Substring(0, eq) : rawFull;

                // Split by spaces to handle the " <op> " formatting
                var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (parts.Count == 0)
                {
                    // nothing to clear
                    currentOperandText = string.Empty;
                    txtOutputWindows.Text = string.Empty;
                    return;
                }

                // Operators we recognize
                var ops = new[] { "+", "-", "×", "÷", "*", "/" };

                // Find last numeric token (operand) from the end and remove it
                int removeIdx = -1;
                for (int i = parts.Count - 1; i >= 0; i--)
                {
                    if (!ops.Contains(parts[i]))
                    {
                        removeIdx = i;
                        break;
                    }
                }

                if (removeIdx >= 0)
                {
                    parts.RemoveAt(removeIdx);
                }

                // Rebuild input with spaces
                var newInput = string.Join(' ', parts);
                txtInputWindows.Text = newInput;

                // Recalculate operator and current operand text
                int opIndex = -1;
                string opToken = null;
                for (int i = 0; i < parts.Count; i++)
                {
                    if (ops.Contains(parts[i]))
                    {
                        opIndex = i;
                        opToken = parts[i];
                        break;
                    }
                }

                if (opIndex >= 0)
                {
                    currentOperator = opToken;
                    // right operand is token after operator if exists
                    if (parts.Count > opIndex + 1)
                    {
                        currentOperandText = parts[opIndex + 1];
                        waitingForOperandBStart = false;
                    }
                    else
                    {
                        currentOperandText = string.Empty;
                        waitingForOperandBStart = true;
                    }
                }
                else
                {
                    currentOperator = null;
                    currentOperandText = parts.Count > 0 ? string.Join(string.Empty, parts) : string.Empty;
                    waitingForOperandBStart = false;
                }

                txtOutputWindows.Text = currentOperandText ?? string.Empty;
                return;
            }

            if (b == btnFunctionDel)
            {
                // If there is a computed result (contains '='), delete from the result part only
                var full = txtInputWindows.Text ?? string.Empty;
                var eqIdx = full.IndexOf('=');
                if (eqIdx >= 0)
                {
                    var before = full.Substring(0, eqIdx);
                    var resultPart = full.Substring(eqIdx + 1);

                    if (string.IsNullOrEmpty(resultPart))
                    {
                        // nothing to delete from result, remove '=' marker
                        txtInputWindows.Text = before;
                        currentOperandText = string.Empty;
                        txtOutputWindows.Text = string.Empty;
                        return;
                    }

                    // remove last char from result part
                    resultPart = resultPart.Substring(0, Math.Max(0, resultPart.Length - 1));

                    if (string.IsNullOrEmpty(resultPart))
                    {
                        // remove the whole result portion including '='
                        txtInputWindows.Text = before;
                        currentOperandText = string.Empty;
                        txtOutputWindows.Text = string.Empty;
                    }
                    else
                    {
                        txtInputWindows.Text = before + "=" + resultPart;
                        currentOperandText = resultPart;
                        txtOutputWindows.Text = resultPart;
                    }

                    // remain in a state where operator is cleared (result shown)
                    currentOperator = null;
                    waitingForOperandBStart = false;
                    return;
                }

                // Otherwise, operate on the visible input (no '=') - remove last character
                var raw = txtInputWindows.Text ?? string.Empty;
                if (string.IsNullOrEmpty(raw))
                {
                    currentOperandText = string.Empty;
                    currentOperator = null;
                    txtOutputWindows.Text = string.Empty;
                    return;
                }

                // remove last character
                raw = raw.Substring(0, raw.Length - 1);
                // trim any trailing spaces left by operator formatting so next delete acts on visible token
                raw = raw.TrimEnd();
                txtInputWindows.Text = raw;

                // Re-evaluate current operator and operand texts after deletion
                var ops = new[] { "+", "-", "×", "÷", "*", "/" };
                int opIdx = -1;
                string op = null;
                foreach (var candidate in ops)
                {
                    var idx = raw.IndexOf(candidate);
                    if (idx >= 0)
                    {
                        opIdx = idx;
                        op = candidate;
                        break;
                    }
                }

                if (opIdx >= 0 && !string.IsNullOrEmpty(op))
                {
                    currentOperator = op;
                    var left = raw.Substring(0, opIdx);
                    var right = raw.Substring(opIdx + op.Length);
                    currentOperandText = right;
                    if (!string.IsNullOrEmpty(left) && int.TryParse(left, out var a))
                        operandA = a;
                    else
                        operandA = 0;

                    waitingForOperandBStart = string.IsNullOrEmpty(right);
                    txtOutputWindows.Text = currentOperandText ?? string.Empty;
                    return;
                }

                // No operator present -> whole raw is current operand
                currentOperator = null;
                currentOperandText = raw;
                if (!string.IsNullOrEmpty(raw) && int.TryParse(raw, out var aa))
                    operandA = aa;
                else
                    operandA = 0;

                waitingForOperandBStart = false;
                txtOutputWindows.Text = currentOperandText ?? string.Empty;
                return;
            }

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

                // append operator to input with surrounding spaces (do not change output)
                txtInputWindows.Text += " " + text + " ";
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
