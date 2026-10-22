using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace VraiJeu
{
    public partial class MenuForme : Form
    {

        public static FormePartie forme = new FormePartie();
        public static bool RecuperSave = false;
        public static Jeux ChargerPartie;


        public MenuForme()
        {
            InitializeComponent();
        }

        private void MenuForme_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None; //removes the title bar of the window

            this.pictureBoxNouvellePartie.MouseEnter +=MouseEnterBtn;
            this.pictureBoxNouvellePartie.MouseLeave += MouseLeaveBtn;
            this.pictureBoxReprendre.MouseEnter += MouseEnterBtn;
            this.pictureBoxReprendre.MouseLeave += MouseLeaveBtn;
            this.pictureBoxQuitter.MouseEnter += MouseEnterBtn;
            this.pictureBoxQuitter.MouseLeave += MouseLeaveBtn;
            //+= syntax means "when the mouse enters the PictureBox, call my method MouseEnterBtn".

            if (File.Exists("SauvegardeUNO.bin")==false)
            {
                pictureBoxReprendre.Enabled = false;

                //If there is no save file on the computer, grey out the Resume button so the player cannot click it

            }

        }


        private void MouseEnterBtn(object sender, EventArgs e)
        {
            PictureBox tmp=sender as PictureBox;
            tmp.Size =new Size(tmp.Width+5,tmp.Height+5);
            tmp.Location=(Point)new Size(tmp.Location.X-5,tmp.Location.Y-5);    
            this.Cursor = Cursors.Hand;
            //we make the button  5 pixels bigger in both width and height everytime we point at it .

        }


        private void MouseLeaveBtn(object sender,EventArgs e)
        {
            PictureBox tmp=sender as PictureBox;
            tmp.Size=new Size(tmp.Width-5,tmp.Height-5 );
            tmp.Location=(Point)new Size(tmp.Location.X+5,tmp.Location.Y+5);
            this.Cursor = Cursors.Default;

            //we make the button 5 pixels smaller in both width and height everytime we stop pointing at it .
        }

        private void pictureBoxNouvellePartie_Click(object sender, EventArgs e)
        {

            RecuperSave = false; // we gonna start a new game not a saved one
            forme.Show();//it means we gonna show the game window when we click on the New Game button
            this.Hide(); //this refers to the class itself(menu) and here we wanna hide the menu

        }

        private void pictureBoxReprendre_Click(object sender, EventArgs e)
        {
            if (File.Exists("SauvegardeUNO.bin"))
            {
                try
                {
                    using (FileStream fileStream = File.OpenRead("SauvegardeUNO.bin"))
                    {
                        forme.TV.Controls.Clear();
                        BinaryFormatter bf=new BinaryFormatter();
                        Jeux jeu = (Jeux)bf.Deserialize(fileStream);
                        MessageBox.Show("Partie chargée avec succès !", "",MessageBoxButtons.OK);
                        MenuForme.ChargerPartie = jeu;
                        RecuperSave = true;

                        forme.Show();
                        this.Hide();


                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement de la partie : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                 MessageBox.Show("Aucune sauvegarde trouvée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            //it reads the saved file that was in bytes and convert it to the object Jeux (object that contains the game state ) by the tool BinaryFormatter
        }


        private void pictureBoxQuitter_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir quitter ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (FormePartie.jeu == null || !FormePartie.jeu.Sauvegarder())
                    {
                        MessageBox.Show("Aucune partie en cours ou erreur de sauvegarde !","", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    else
                    {
                        using (FileStream fileStream = File.Create("SauvegardeUNO.bin"))
                        {

                            BinaryFormatter bf = new BinaryFormatter();
                            bf.Serialize(fileStream, FormePartie.jeu);
                        }
                        MessageBox.Show("Partie sauvegardée !", "", MessageBoxButtons.OK);

                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la sauvegarde de la partie : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.Exit();
                }
            }

        }
    }
}
