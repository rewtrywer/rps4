namespace rps4
{
    partial class Form4
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
            dateTimePicker3 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            button1 = new Button();
            label6 = new Label();
            textBox6 = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            textBox5 = new TextBox();
            SuspendLayout();
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.CustomFormat = "HH:mm:ss";
            dateTimePicker3.Format = DateTimePickerFormat.Time;
            dateTimePicker3.Location = new Point(285, 58);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.ShowUpDown = true;
            dateTimePicker3.Size = new Size(101, 27);
            dateTimePicker3.TabIndex = 26;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.Location = new Point(176, 58);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Size = new Size(90, 27);
            dateTimePicker2.TabIndex = 25;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(18, 58);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(146, 27);
            dateTimePicker1.TabIndex = 24;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientInactiveCaption;
            button1.Location = new Point(252, 113);
            button1.Name = "button1";
            button1.Size = new Size(121, 32);
            button1.TabIndex = 23;
            button1.Text = "Применить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(536, 25);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 22;
            label6.Text = "Куда";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(513, 58);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(93, 27);
            textBox6.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(422, 25);
            label5.Name = "label5";
            label5.Size = new Size(56, 20);
            label5.TabIndex = 20;
            label5.Text = "Откуда";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(285, 25);
            label4.Name = "label4";
            label4.Size = new Size(101, 20);
            label4.TabIndex = 19;
            label4.Text = "Время в пути";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(195, 25);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 18;
            label3.Text = "Время";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 25);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 17;
            label2.Text = "Дата";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(407, 58);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(93, 27);
            textBox5.TabIndex = 16;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(636, 166);
            Controls.Add(dateTimePicker3);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(textBox6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox5);
            Name = "Form4";
            Text = "Редактирование";
            Load += Form4_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker3;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Button button1;
        private Label label6;
        private TextBox textBox6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox textBox5;
    }
}