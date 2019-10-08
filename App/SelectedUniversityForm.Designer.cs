namespace Objektinis
{
    partial class SelectedUniversityForm
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
            this.universityName = new System.Windows.Forms.TextBox();
            this.facultiesListBox = new System.Windows.Forms.ListBox();
            this.writeReviewButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // universityName
            // 
            this.universityName.Location = new System.Drawing.Point(106, 53);
            this.universityName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.universityName.Name = "universityName";
            this.universityName.Size = new System.Drawing.Size(378, 20);
            this.universityName.TabIndex = 0;
            // 
            // facultiesListBox
            // 
            this.facultiesListBox.FormattingEnabled = true;
            this.facultiesListBox.Location = new System.Drawing.Point(106, 126);
            this.facultiesListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.facultiesListBox.Name = "facultiesListBox";
            this.facultiesListBox.Size = new System.Drawing.Size(378, 173);
            this.facultiesListBox.TabIndex = 1;
            // 
            // writeReviewButton
            // 
            this.writeReviewButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeReviewButton.Location = new System.Drawing.Point(355, 318);
            this.writeReviewButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.writeReviewButton.Name = "writeReviewButton";
            this.writeReviewButton.Size = new System.Drawing.Size(128, 28);
            this.writeReviewButton.TabIndex = 2;
            this.writeReviewButton.Text = "add review";
            this.writeReviewButton.UseVisualStyleBackColor = true;
            this.writeReviewButton.Click += new System.EventHandler(this.WriteReviewButton_Click);
            // 
            // readButton
            // 
            this.readButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readButton.Location = new System.Drawing.Point(106, 318);
            this.readButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(128, 28);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "read reviews";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select a faculty (optional)";
            // 
            // SelectedUniversity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.writeReviewButton);
            this.Controls.Add(this.facultiesListBox);
            this.Controls.Add(this.universityName);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SelectedUniversity";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SelectedUniversity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox universityName;
        private System.Windows.Forms.ListBox facultiesListBox;
        private System.Windows.Forms.Button writeReviewButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Label label1;
    }
}