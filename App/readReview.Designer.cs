﻿namespace Objektinis
{
    partial class readReview
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
            this.components = new System.ComponentModel.Container();
            this.reviewText = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.titleOfThing = new System.Windows.Forms.TextBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.LikeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reviewText
            // 
            this.reviewText.Location = new System.Drawing.Point(160, 133);
            this.reviewText.Name = "reviewText";
            this.reviewText.Size = new System.Drawing.Size(456, 227);
            this.reviewText.TabIndex = 0;
            this.reviewText.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // titleOfThing
            // 
            this.titleOfThing.Location = new System.Drawing.Point(236, 67);
            this.titleOfThing.Name = "titleOfThing";
            this.titleOfThing.Size = new System.Drawing.Size(295, 22);
            this.titleOfThing.TabIndex = 2;
            this.titleOfThing.Text = "Name of what is reviewed";
            this.titleOfThing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.titleOfThing.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(540, 387);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(76, 34);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(160, 387);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(76, 34);
            this.previousButton.TabIndex = 4;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(76, 34);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // LikeButton
            // 
            this.LikeButton.Location = new System.Drawing.Point(656, 148);
            this.LikeButton.Name = "LikeButton";
            this.LikeButton.Size = new System.Drawing.Size(44, 30);
            this.LikeButton.TabIndex = 6;
            this.LikeButton.Text = "+1";
            this.LikeButton.UseVisualStyleBackColor = true;
            // 
            // readReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LikeButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.titleOfThing);
            this.Controls.Add(this.reviewText);
            this.Name = "readReview";
            this.Text = "readReview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox reviewText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox titleOfThing;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button LikeButton;
    }
}