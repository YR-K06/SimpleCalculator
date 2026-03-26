using System.Windows.Forms;
using System.Drawing;

namespace SimpleCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtInputWindows = new TextBox();
            txtOutputWindows = new TextBox();
            lblTitle = new Label();
            btnFunctionCE = new Button();
            btnFunctionC = new Button();
            btnFunctionDel = new Button();
            btnOperatorDivide = new Button();
            btnNumber7 = new Button();
            btnNumber8 = new Button();
            btnNumber9 = new Button();
            btnOperatorMultiply = new Button();
            btnNumber4 = new Button();
            btnNumber5 = new Button();
            btnNumber6 = new Button();
            btnOperatorSubtract = new Button();
            btnNumber1 = new Button();
            btnNumber2 = new Button();
            btnNumber3 = new Button();
            btnOperatorAdd = new Button();
            btnNumber0 = new Button();
            btnDot = new Button();
            btnResult = new Button();
            btnNegativeSign = new Button();
            SuspendLayout();
            // 
            // txtInputWindows
            // 
            txtInputWindows.Font = new Font("맑은 고딕", 20F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtInputWindows.Location = new Point(62, 197);
            txtInputWindows.Name = "txtInputWindows";
            txtInputWindows.Size = new Size(578, 61);
            txtInputWindows.TabIndex = 0;
            // 
            // txtOutputWindows
            // 
            txtOutputWindows.Font = new Font("맑은 고딕", 20F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtOutputWindows.Location = new Point(62, 278);
            txtOutputWindows.Name = "txtOutputWindows";
            txtOutputWindows.Size = new Size(578, 61);
            txtOutputWindows.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(62, 46);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(593, 81);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Simple Calculator";
            // 
            // btnFunctionCE
            // 
            btnFunctionCE.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFunctionCE.Location = new Point(62, 378);
            btnFunctionCE.Name = "btnFunctionCE";
            btnFunctionCE.Size = new Size(140, 61);
            btnFunctionCE.TabIndex = 3;
            btnFunctionCE.Text = "CE";
            btnFunctionCE.UseVisualStyleBackColor = true;
            // 
            // btnFunctionC
            // 
            btnFunctionC.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFunctionC.Location = new Point(208, 378);
            btnFunctionC.Name = "btnFunctionC";
            btnFunctionC.Size = new Size(140, 61);
            btnFunctionC.TabIndex = 4;
            btnFunctionC.Text = "C";
            btnFunctionC.UseVisualStyleBackColor = true;
            btnFunctionC.Click += button2_Click;
            // 
            // btnFunctionDel
            // 
            btnFunctionDel.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFunctionDel.Location = new Point(354, 378);
            btnFunctionDel.Name = "btnFunctionDel";
            btnFunctionDel.Size = new Size(140, 61);
            btnFunctionDel.TabIndex = 5;
            btnFunctionDel.Text = "del";
            btnFunctionDel.UseVisualStyleBackColor = true;
            // 
            // btnOperatorDivide
            // 
            btnOperatorDivide.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOperatorDivide.ForeColor = Color.Red;
            btnOperatorDivide.Location = new Point(500, 378);
            btnOperatorDivide.Name = "btnOperatorDivide";
            btnOperatorDivide.Size = new Size(140, 61);
            btnOperatorDivide.TabIndex = 6;
            btnOperatorDivide.Text = "÷";
            btnOperatorDivide.UseVisualStyleBackColor = true;
            // 
            // btnNumber7
            // 
            btnNumber7.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber7.ForeColor = Color.Blue;
            btnNumber7.Location = new Point(62, 457);
            btnNumber7.Name = "btnNumber7";
            btnNumber7.Size = new Size(140, 61);
            btnNumber7.TabIndex = 7;
            btnNumber7.Text = "7";
            btnNumber7.UseVisualStyleBackColor = true;
            btnNumber7.Click += button4_Click;
            // 
            // btnNumber8
            // 
            btnNumber8.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber8.ForeColor = Color.Blue;
            btnNumber8.Location = new Point(208, 457);
            btnNumber8.Name = "btnNumber8";
            btnNumber8.Size = new Size(140, 61);
            btnNumber8.TabIndex = 8;
            btnNumber8.Text = "8";
            btnNumber8.UseVisualStyleBackColor = true;
            // 
            // btnNumber9
            // 
            btnNumber9.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber9.ForeColor = Color.Blue;
            btnNumber9.Location = new Point(354, 457);
            btnNumber9.Name = "btnNumber9";
            btnNumber9.Size = new Size(140, 61);
            btnNumber9.TabIndex = 9;
            btnNumber9.Text = "9";
            btnNumber9.UseVisualStyleBackColor = true;
            // 
            // btnOperatorMultiply
            // 
            btnOperatorMultiply.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOperatorMultiply.ForeColor = Color.Red;
            btnOperatorMultiply.Location = new Point(500, 457);
            btnOperatorMultiply.Name = "btnOperatorMultiply";
            btnOperatorMultiply.Size = new Size(140, 61);
            btnOperatorMultiply.TabIndex = 10;
            btnOperatorMultiply.Text = "×";
            btnOperatorMultiply.UseVisualStyleBackColor = true;
            // 
            // btnNumber4
            // 
            btnNumber4.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber4.ForeColor = Color.Blue;
            btnNumber4.Location = new Point(62, 539);
            btnNumber4.Name = "btnNumber4";
            btnNumber4.Size = new Size(140, 61);
            btnNumber4.TabIndex = 11;
            btnNumber4.Text = "4";
            btnNumber4.UseVisualStyleBackColor = true;
            // 
            // btnNumber5
            // 
            btnNumber5.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber5.ForeColor = Color.Blue;
            btnNumber5.Location = new Point(208, 539);
            btnNumber5.Name = "btnNumber5";
            btnNumber5.Size = new Size(140, 61);
            btnNumber5.TabIndex = 12;
            btnNumber5.Text = "5";
            btnNumber5.UseVisualStyleBackColor = true;
            // 
            // btnNumber6
            // 
            btnNumber6.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber6.ForeColor = Color.Blue;
            btnNumber6.Location = new Point(354, 539);
            btnNumber6.Name = "btnNumber6";
            btnNumber6.Size = new Size(140, 61);
            btnNumber6.TabIndex = 13;
            btnNumber6.Text = "6";
            btnNumber6.UseVisualStyleBackColor = true;
            // 
            // btnOperatorSubtract
            // 
            btnOperatorSubtract.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOperatorSubtract.ForeColor = Color.Red;
            btnOperatorSubtract.Location = new Point(500, 539);
            btnOperatorSubtract.Name = "btnOperatorSubtract";
            btnOperatorSubtract.Size = new Size(140, 61);
            btnOperatorSubtract.TabIndex = 14;
            btnOperatorSubtract.Text = "-";
            btnOperatorSubtract.UseVisualStyleBackColor = true;
            // 
            // btnNumber1
            // 
            btnNumber1.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber1.ForeColor = Color.Blue;
            btnNumber1.Location = new Point(62, 621);
            btnNumber1.Name = "btnNumber1";
            btnNumber1.Size = new Size(140, 61);
            btnNumber1.TabIndex = 15;
            btnNumber1.Text = "1";
            btnNumber1.UseVisualStyleBackColor = true;
            // 
            // btnNumber2
            // 
            btnNumber2.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber2.ForeColor = Color.Blue;
            btnNumber2.Location = new Point(208, 621);
            btnNumber2.Name = "btnNumber2";
            btnNumber2.Size = new Size(140, 61);
            btnNumber2.TabIndex = 16;
            btnNumber2.Text = "2";
            btnNumber2.UseVisualStyleBackColor = true;
            // 
            // btnNumber3
            // 
            btnNumber3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber3.ForeColor = Color.Blue;
            btnNumber3.Location = new Point(354, 621);
            btnNumber3.Name = "btnNumber3";
            btnNumber3.Size = new Size(140, 61);
            btnNumber3.TabIndex = 17;
            btnNumber3.Text = "3";
            btnNumber3.UseVisualStyleBackColor = true;
            // 
            // btnOperatorAdd
            // 
            btnOperatorAdd.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOperatorAdd.ForeColor = Color.Red;
            btnOperatorAdd.Location = new Point(500, 621);
            btnOperatorAdd.Name = "btnOperatorAdd";
            btnOperatorAdd.Size = new Size(140, 61);
            btnOperatorAdd.TabIndex = 18;
            btnOperatorAdd.Text = "+";
            btnOperatorAdd.UseVisualStyleBackColor = true;
            // 
            // btnNumber0
            // 
            btnNumber0.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNumber0.ForeColor = Color.Blue;
            btnNumber0.Location = new Point(208, 704);
            btnNumber0.Name = "btnNumber0";
            btnNumber0.Size = new Size(140, 61);
            btnNumber0.TabIndex = 19;
            btnNumber0.Text = "0";
            btnNumber0.UseVisualStyleBackColor = true;
            // 
            // btnDot
            // 
            btnDot.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDot.Location = new Point(354, 704);
            btnDot.Name = "btnDot";
            btnDot.Size = new Size(140, 61);
            btnDot.TabIndex = 20;
            btnDot.Text = ".";
            btnDot.UseVisualStyleBackColor = true;
            // 
            // btnResult
            // 
            btnResult.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnResult.Location = new Point(500, 704);
            btnResult.Name = "btnResult";
            btnResult.Size = new Size(140, 61);
            btnResult.TabIndex = 21;
            btnResult.Text = "=";
            btnResult.UseVisualStyleBackColor = true;
            // 
            // btnNegativeSign
            // 
            btnNegativeSign.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnNegativeSign.Location = new Point(62, 704);
            btnNegativeSign.Name = "btnNegativeSign";
            btnNegativeSign.Size = new Size(140, 61);
            btnNegativeSign.TabIndex = 22;
            btnNegativeSign.Text = "+/-";
            btnNegativeSign.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(717, 890);
            Controls.Add(btnNegativeSign);
            Controls.Add(btnResult);
            Controls.Add(btnDot);
            Controls.Add(btnNumber0);
            Controls.Add(btnOperatorAdd);
            Controls.Add(btnNumber3);
            Controls.Add(btnNumber2);
            Controls.Add(btnNumber1);
            Controls.Add(btnOperatorSubtract);
            Controls.Add(btnNumber6);
            Controls.Add(btnNumber5);
            Controls.Add(btnNumber4);
            Controls.Add(btnOperatorMultiply);
            Controls.Add(btnNumber9);
            Controls.Add(btnNumber8);
            Controls.Add(btnNumber7);
            Controls.Add(btnOperatorDivide);
            Controls.Add(btnFunctionDel);
            Controls.Add(btnFunctionC);
            Controls.Add(btnFunctionCE);
            Controls.Add(lblTitle);
            Controls.Add(txtOutputWindows);
            Controls.Add(txtInputWindows);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtInputWindows;
        private TextBox txtOutputWindows;
        private Label lblTitle;
        private Button btnFunctionCE;
        private Button btnFunctionC;
        private Button btnFunctionDel;
        private Button btnOperatorDivide;
        private Button btnNumber7;
        private Button btnNumber8;
        private Button btnNumber9;
        private Button btnOperatorMultiply;
        private Button btnNumber4;
        private Button btnNumber5;
        private Button btnNumber6;
        private Button btnOperatorSubtract;
        private Button btnNumber1;
        private Button btnNumber2;
        private Button btnNumber3;
        private Button btnOperatorAdd;
        private Button btnNumber0;
        private Button btnDot;
        private Button btnResult;
        private Button btnNegativeSign;
    }
}
