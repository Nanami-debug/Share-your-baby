namespace SCF_Final_Homework
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
            aiButton1 = new ReaLTaiizor.Controls.AirButton();
            airButton2 = new ReaLTaiizor.Controls.AirButton();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // aiButton1
            // 
            aiButton1.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            aiButton1.Font = new Font("Segoe UI", 9F);
            aiButton1.Image = null;
            aiButton1.Location = new Point(158, 383);
            aiButton1.Name = "aiButton1";
            aiButton1.NoRounding = false;
            aiButton1.Size = new Size(168, 93);
            aiButton1.TabIndex = 1;
            aiButton1.Text = "输入";
            aiButton1.Transparent = false;
            aiButton1.Click += airButton1_Click;
            // 
            // airButton2
            // 
            airButton2.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            airButton2.Font = new Font("Segoe UI", 9F);
            airButton2.Image = null;
            airButton2.Location = new Point(496, 379);
            airButton2.Name = "airButton2";
            airButton2.NoRounding = false;
            airButton2.Size = new Size(164, 97);
            airButton2.TabIndex = 3;
            airButton2.Text = "退出";
            airButton2.Transparent = false;
            airButton2.Click += airButton2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(124, 56);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(595, 161);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(221, 272);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(353, 76);
            textBox2.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(867, 585);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(airButton2);
            Controls.Add(aiButton1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            TransparencyKey = Color.Fuchsia;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.AirButton aiButton1;
        private ReaLTaiizor.Controls.AirButton airButton2;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}
