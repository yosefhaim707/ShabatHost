namespace ShabatHost.Views
{
    partial class HostForm
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
            listView_Categories = new ListView();
            button_Enter = new Button();
            textBox_Insert = new TextBox();
            label_Title = new Label();
            SuspendLayout();
            // 
            // listView_Categories
            // 
            listView_Categories.Location = new Point(136, 281);
            listView_Categories.Name = "listView_Categories";
            listView_Categories.Size = new Size(182, 201);
            listView_Categories.TabIndex = 7;
            listView_Categories.UseCompatibleStateImageBehavior = false;
            // 
            // button_Enter
            // 
            button_Enter.Location = new Point(161, 184);
            button_Enter.Name = "button_Enter";
            button_Enter.Size = new Size(112, 34);
            button_Enter.TabIndex = 6;
            button_Enter.Text = "Enter";
            button_Enter.UseVisualStyleBackColor = true;
            button_Enter.Click += button_Enter_Click;
            // 
            // textBox_Insert
            // 
            textBox_Insert.Location = new Point(141, 120);
            textBox_Insert.Name = "textBox_Insert";
            textBox_Insert.Size = new Size(150, 31);
            textBox_Insert.TabIndex = 5;
            // 
            // label_Title
            // 
            label_Title.AutoSize = true;
            label_Title.Location = new Point(122, 60);
            label_Title.Name = "label_Title";
            label_Title.Size = new Size(196, 25);
            label_Title.TabIndex = 4;
            label_Title.Text = "Host - Enter Categories";
            // 
            // HostForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 597);
            Controls.Add(listView_Categories);
            Controls.Add(button_Enter);
            Controls.Add(textBox_Insert);
            Controls.Add(label_Title);
            Name = "HostForm";
            Text = "HostForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView_Categories;
        private Button button_Enter;
        private TextBox textBox_Insert;
        private Label label_Title;
    }
}