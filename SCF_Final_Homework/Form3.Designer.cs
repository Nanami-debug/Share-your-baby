namespace FORM3
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox5 = new TextBox();
            label5 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(234, 128);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(168, 30);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(244, 222);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(158, 30);
            textBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(170, 637);
            button1.Name = "button1";
            button1.Size = new Size(168, 79);
            button1.TabIndex = 2;
            button1.Text = "确认注册";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(244, 315);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(158, 30);
            textBox3.TabIndex = 3;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(241, 387);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(153, 30);
            textBox4.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(123, 134);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 5;
            label1.Text = "用户名";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(123, 228);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 6;
            label2.Text = "密码";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(65, 315);
            label3.Name = "label3";
            label3.Size = new Size(154, 24);
            label3.TabIndex = 7;
            label3.Text = "再次确认您的密码";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(106, 390);
            label4.Name = "label4";
            label4.Size = new Size(46, 24);
            label4.TabIndex = 8;
            label4.Text = "邮箱";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(244, 447);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(150, 30);
            textBox5.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(106, 453);
            label5.Name = "label5";
            label5.Size = new Size(64, 24);
            label5.TabIndex = 10;
            label5.Text = "验证码";
            // 
            // button2
            // 
            button2.Location = new Point(170, 516);
            button2.Name = "button2";
            button2.Size = new Size(158, 79);
            button2.TabIndex = 11;
            button2.Text = "获取验证码";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 823);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(textBox5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox5;
        private Label label5;
        private Button button2;
    }
}