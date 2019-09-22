namespace Objektinis
{
    partial class universities
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
            this.searchBar = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchButton = new System.Windows.Forms.Button();
            this.universitiesList = new System.Windows.Forms.ListBox();
            this.all = new System.Windows.Forms.Label();
            this.selectUniButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(106, 59);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(500, 22);
            this.searchBar.TabIndex = 0;
            this.searchBar.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(645, 55);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(106, 30);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // universitiesList
            // 
            this.universitiesList.FormattingEnabled = true;
            this.universitiesList.ItemHeight = 16;
            this.universitiesList.Location = new System.Drawing.Point(106, 171);
            this.universitiesList.Name = "universitiesList";
            this.universitiesList.Size = new System.Drawing.Size(500, 212);
            this.universitiesList.TabIndex = 3;
            // 
            // all
            // 
            this.all.AutoSize = true;
            this.all.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all.Location = new System.Drawing.Point(306, 139);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(113, 16);
            this.all.TabIndex = 4;
            this.all.Text = "All universities";
            // 
            // selectUniButton
            // 
            this.selectUniButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectUniButton.Location = new System.Drawing.Point(309, 389);
            this.selectUniButton.Name = "selectUniButton";
            this.selectUniButton.Size = new System.Drawing.Size(106, 30);
            this.selectUniButton.TabIndex = 5;
            this.selectUniButton.Text = "select";
            this.selectUniButton.UseVisualStyleBackColor = true;
            // 
            // universities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectUniButton);
            this.Controls.Add(this.all);
            this.Controls.Add(this.universitiesList);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchBar);
            this.Name = "universities";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox universitiesList;
        private System.Windows.Forms.Label all;
        private System.Windows.Forms.Button selectUniButton;
    }
}

