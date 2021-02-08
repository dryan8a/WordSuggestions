
namespace WordSuggestions
{
    partial class Form1
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
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CompletionSuggestions = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SpellingSuggestions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(12, 12);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(657, 426);
            this.InputBox.TabIndex = 0;
            this.InputBox.Text = "";
            this.InputBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(675, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Completion Suggestions";
            // 
            // CompletionSuggestions
            // 
            this.CompletionSuggestions.FormattingEnabled = true;
            this.CompletionSuggestions.Location = new System.Drawing.Point(675, 47);
            this.CompletionSuggestions.Name = "CompletionSuggestions";
            this.CompletionSuggestions.Size = new System.Drawing.Size(113, 95);
            this.CompletionSuggestions.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(675, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spelling Suggestions";
            // 
            // SpellingSuggestions
            // 
            this.SpellingSuggestions.FormattingEnabled = true;
            this.SpellingSuggestions.Location = new System.Drawing.Point(678, 254);
            this.SpellingSuggestions.Name = "SpellingSuggestions";
            this.SpellingSuggestions.Size = new System.Drawing.Size(110, 95);
            this.SpellingSuggestions.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SpellingSuggestions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CompletionSuggestions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox InputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox CompletionSuggestions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox SpellingSuggestions;
    }
}

