namespace VraiJeu
{
    partial class FormePartie
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
            this.TV = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TV)).BeginInit();
            this.SuspendLayout();
            // 
            // TV
            // 
            this.TV.Location = new System.Drawing.Point(315, 182);
            this.TV.Name = "TV";
            this.TV.Size = new System.Drawing.Size(100, 50);
            this.TV.TabIndex = 0;
            this.TV.TabStop = false;
            // 
            // FormePartie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TV);
            this.Name = "FormePartie";
            this.Text = "FormePartie";
            this.Load += new System.EventHandler(this.FormePartie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox TV;
    }
}