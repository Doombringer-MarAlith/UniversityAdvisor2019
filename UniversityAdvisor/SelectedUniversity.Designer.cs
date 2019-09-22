namespace Objektinis
{
    partial class SelectedUniversity
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
            this.faculties = new System.Windows.Forms.ListBox();
            this.writeReviewButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // universityName
            // 
            this.universityName.Location = new System.Drawing.Point(141, 65);
            this.universityName.Name = "universityName";
            this.universityName.Size = new System.Drawing.Size(503, 22);
            this.universityName.TabIndex = 0;
            // 
            // faculties
            // 
            this.faculties.FormattingEnabled = true;
            this.faculties.ItemHeight = 16;
            this.faculties.Location = new System.Drawing.Point(141, 155);
            this.faculties.Name = "faculties";
            this.faculties.Size = new System.Drawing.Size(503, 212);
            this.faculties.TabIndex = 1;
            // 
            // writeReviewButton
            // 
            this.writeReviewButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeReviewButton.Location = new System.Drawing.Point(473, 392);
            this.writeReviewButton.Name = "writeReviewButton";
            this.writeReviewButton.Size = new System.Drawing.Size(171, 35);
            this.writeReviewButton.TabIndex = 2;
            this.writeReviewButton.Text = "add review";
            this.writeReviewButton.UseVisualStyleBackColor = true;
            this.writeReviewButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // readButton
            // 
            this.readButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readButton.Location = new System.Drawing.Point(141, 392);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(171, 35);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "read reviews";
            this.readButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(290, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select a faculty (optional)";
            // 
            // SelectedUniversity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.writeReviewButton);
            this.Controls.Add(this.faculties);
            this.Controls.Add(this.universityName);
            this.Name = "SelectedUniversity";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox universityName;
        private System.Windows.Forms.ListBox faculties;
        private System.Windows.Forms.Button writeReviewButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Label label1;
    }
}