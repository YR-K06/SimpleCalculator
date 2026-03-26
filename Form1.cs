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

            // 모든 버튼을 단일 핸들러에 연결합니다. 버튼을 누른 모든 입력이 `txtInputWindows`에 표시됩니다.
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

        // 디자이너가 연결한 이벤트를 유지하기 위해 플래이스홀더 핸들러를 공통 핸들러로 전달합니다.
        private void button2_Click(object sender, EventArgs e)
        {
            AnyButton_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnyButton_Click(sender, e);
        }

        // 공통 핸들러: 눌린 버튼의 텍스트를 `txtInputWindows`에 누적하고
        // 가장 최근에 누른 버튼 또는 현재 입력 중인 피연산자를 `txtOutputWindows`에 표시합니다.
        // '='(결과) 버튼이 눌리면 입력된 식(두 개의 정수 피연산자)을 계산하여 결과를 `txtOutputWindows`에 표시하고
        // `txtInputWindows`에는 결과를 덧붙여 보여줍니다.
        private void AnyButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button b)
                return;

            var text = b.Text ?? string.Empty;

            // 먼저 지우기(CE/C) 및 삭제(del) 버튼을 처리합니다. 이 버튼들은 자신의 텍스트를 입력창에 추가하지 않습니다.
            if (b == btnFunctionC)
            {
                // 전체 초기화: 입력/출력창과 내부 상태를 모두 지웁니다.
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
                // CE(Entr y 지우기): 가장 최근에 입력된 피연산자 덩어리만 삭제합니다. 연산자는 남깁니다.
                var rawFull = txtInputWindows.Text ?? string.Empty;
                var eq = rawFull.IndexOf('=');
                var input = eq >= 0 ? rawFull.Substring(0, eq) : rawFull;

                // 연산자 양쪽에 공백을 넣는 포맷을 고려하여 공백으로 분할합니다.
                var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (parts.Count == 0)
                {
                    // nothing to clear
                    currentOperandText = string.Empty;
                    txtOutputWindows.Text = string.Empty;
                    return;
                }

                // 인식 가능한 연산자 목록
                var ops = new[] { "+", "-", "×", "÷", "*", "/" };

                // 뒤에서부터 마지막 숫자 토큰(피연산자)을 찾아 제거합니다.
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

                // 공백을 포함한 문자열로 다시 조합합니다.
                var newInput = string.Join(' ', parts);
                txtInputWindows.Text = newInput;

                // 연산자와 현재 입력 중인 피연산자 텍스트를 재계산합니다.
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
                // 이미 계산 결과( '=' 포함)가 있는 경우에는 결과 부분에서만 삭제합니다.
                var full = txtInputWindows.Text ?? string.Empty;
                var eqIdx = full.IndexOf('=');
                if (eqIdx >= 0)
                {
                    var before = full.Substring(0, eqIdx);
                    var resultPart = full.Substring(eqIdx + 1);

                    if (string.IsNullOrEmpty(resultPart))
                    {
                        // 결과에서 지울 것이 없으면 '=' 표시를 제거합니다.
                        txtInputWindows.Text = before;
                        currentOperandText = string.Empty;
                        txtOutputWindows.Text = string.Empty;
                        return;
                    }

                    // 결과 부분에서 마지막 문자를 제거합니다.
                    resultPart = resultPart.Substring(0, Math.Max(0, resultPart.Length - 1));

                    if (string.IsNullOrEmpty(resultPart))
                    {
                        // 결과가 빈 문자열이 되면 '='과 결과 전체를 제거합니다.
                        txtInputWindows.Text = before;
                        currentOperandText = string.Empty;
                        txtOutputWindows.Text = string.Empty;
                    }
                    else
                    {
                        // 결과를 축소하여 다시 표시합니다.
                        txtInputWindows.Text = before + "=" + resultPart;
                        currentOperandText = resultPart;
                        txtOutputWindows.Text = resultPart;
                    }

                    // 결과가 표시된 상태이므로 연산자 상태는 초기화합니다.
                    currentOperator = null;
                    waitingForOperandBStart = false;
                    return;
                }

                // '='가 없을 경우: 입력창의 맨 끝 문자 하나를 삭제합니다.
                var raw = txtInputWindows.Text ?? string.Empty;
                if (string.IsNullOrEmpty(raw))
                {
                    currentOperandText = string.Empty;
                    currentOperator = null;
                    txtOutputWindows.Text = string.Empty;
                    return;
                }

                // 마지막 문자를 제거합니다.
                raw = raw.Substring(0, raw.Length - 1);
                // 연산자 포맷으로 인해 남아 있을 수 있는 끝 공백을 제거합니다.
                raw = raw.TrimEnd();
                txtInputWindows.Text = raw;

                // 삭제 후 연산자와 피연산자 텍스트를 재계산합니다.
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

                    // 오른쪽 피연산자가 비어있으면 다음 입력을 operandB로 간주합니다.
                    waitingForOperandBStart = string.IsNullOrEmpty(right);
                    txtOutputWindows.Text = currentOperandText ?? string.Empty;
                    return;
                }

                // 연산자가 없으면 입력 전체가 현재 피연산자입니다.
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
