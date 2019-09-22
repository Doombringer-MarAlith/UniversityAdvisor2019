namespace Objektinis
{
    partial class reviewForm
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
            this.reviewTextBox = new System.Windows.Forms.RichTextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.numericReview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reviewTextBox
            // 
            this.reviewTextBox.Location = new System.Drawing.Point(188, 93);
            this.reviewTextBox.Name = "reviewTextBox";
            this.reviewTextBox.Size = new System.Drawing.Size(417, 248);
            this.reviewTextBox.TabIndex = 0;
            this.reviewTextBox.Text = "";
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(323, 366);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(160, 57);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "submit";
            this.submitButton.UseVisualStyleBackColor = true;
            // 
            // numericReview
            // 
            this.numericReview.FormattingEnabled = true;
            this.numericReview.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.numericReview.Location = new System.Drawing.Point(675, 93);
            this.numericReview.Name = "numericReview";
            this.numericReview.Size = new System.Drawing.Size(69, 24);
            this.numericReview.TabIndex = 2;
            this.numericReview.SelectedIndexChanged += new System.EventHandler(this.NumericReview_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(341, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Write your review";
            // 
            // reviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericReview);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.reviewTextBox);
            this.Name = "reviewForm";
            this.Text = "reviewForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox reviewTextBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.ComboBox numericReview;
        private System.Windows.Forms.Label label1;
    }
}