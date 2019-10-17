namespace App
{
    partial class UniversitySearchForm
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
            this.searchBar.Location = new System.Drawing.Point(80, 48);
            this.searchBar.Margin = new System.Windows.Forms.Padding(2);
            this.searchBar.MaxLength = 50;
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(376, 20);
            this.searchBar.TabIndex = 0;
            this.searchBar.Text = "Enter university name";
            this.searchBar.Click += new System.EventHandler(this.SearchBar_Click);
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
            this.searchButton.Location = new System.Drawing.Point(484, 45);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(80, 24);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // universitiesList
            // 
            this.universitiesList.FormattingEnabled = true;
            this.universitiesList.Location = new System.Drawing.Point(80, 139);
            this.universitiesList.Margin = new System.Windows.Forms.Padding(2);
            this.universitiesList.Name = "universitiesList";
            this.universitiesList.Size = new System.Drawing.Size(376, 173);
            this.universitiesList.TabIndex = 3;
            // 
            // all
            // 
            this.all.AutoSize = true;
            this.all.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all.Location = new System.Drawing.Point(230, 113);
            this.all.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(90, 14);
            this.all.TabIndex = 4;
            this.all.Text = "All universities";
            // 
            // selectUniButton
            // 
            this.selectUniButton.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectUniButton.Location = new System.Drawing.Point(232, 316);
            this.selectUniButton.Margin = new System.Windows.Forms.Padding(2);
            this.selectUniButton.Name = "selectUniButton";
            this.selectUniButton.Size = new System.Drawing.Size(80, 24);
            this.selectUniButton.TabIndex = 5;
            this.selectUniButton.Text = "select";
            this.selectUniButton.UseVisualStyleBackColor = true;
            this.selectUniButton.Click += new System.EventHandler(this.SelectUniButton_Click);
            // 
            // UniversitySearchForm
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.selectUniButton);
            this.Controls.Add(this.all);
            this.Controls.Add(this.universitiesList);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchBar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UniversitySearchForm";
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

