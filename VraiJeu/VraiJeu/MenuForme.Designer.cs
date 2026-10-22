namespace VraiJeu
{
    partial class MenuForme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForme));
            this.pictureBoxNouvellePartie = new System.Windows.Forms.PictureBox();
            this.pictureBoxReprendre = new System.Windows.Forms.PictureBox();
            this.pictureBoxQuitter = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNouvellePartie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReprendre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuitter)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxNouvellePartie
            // 
            this.pictureBoxNouvellePartie.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNouvellePartie.Image")));
            this.pictureBoxNouvellePartie.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxNouvellePartie.Name = "pictureBoxNouvellePartie";
            this.pictureBoxNouvellePartie.Size = new System.Drawing.Size(174, 70);
            this.pictureBoxNouvellePartie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxNouvellePartie.TabIndex = 0;
            this.pictureBoxNouvellePartie.TabStop = false;
            this.pictureBoxNouvellePartie.Click += new System.EventHandler(this.pictureBoxNouvellePartie_Click);
            // 
            // pictureBoxReprendre
            // 
            this.pictureBoxReprendre.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxReprendre.Image")));
            this.pictureBoxReprendre.Location = new System.Drawing.Point(12, 97);
            this.pictureBoxReprendre.Name = "pictureBoxReprendre";
            this.pictureBoxReprendre.Size = new System.Drawing.Size(174, 70);
            this.pictureBoxReprendre.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxReprendre.TabIndex = 1;
            this.pictureBoxReprendre.TabStop = false;
            this.pictureBoxReprendre.Click += new System.EventHandler(this.pictureBoxReprendre_Click);
            // 
            // pictureBoxQuitter
            // 
            this.pictureBoxQuitter.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxQuitter.Image")));
            this.pictureBoxQuitter.Location = new System.Drawing.Point(12, 196);
            this.pictureBoxQuitter.Name = "pictureBoxQuitter";
            this.pictureBoxQuitter.Size = new System.Drawing.Size(174, 70);
            this.pictureBoxQuitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxQuitter.TabIndex = 2;
            this.pictureBoxQuitter.TabStop = false;
            this.pictureBoxQuitter.Click += new System.EventHandler(this.pictureBoxQuitter_Click);
            // 
            // MenuForme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.pictureBoxQuitter);
            this.Controls.Add(this.pictureBoxReprendre);
            this.Controls.Add(this.pictureBoxNouvellePartie);
            this.Name = "MenuForme";
            this.Text = "MenuForme";
            this.Load += new System.EventHandler(this.MenuForme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNouvellePartie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReprendre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuitter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxNouvellePartie;
        private System.Windows.Forms.PictureBox pictureBoxReprendre;
        private System.Windows.Forms.PictureBox pictureBoxQuitter;
    }
}