using System;
using System.Drawing;
using System.Windows.Forms;

namespace VraiJeu
{
    public partial class FormePartie : Form
    {

        MenuForme forme = new MenuForme();
        public static Jeux jeu;

        public Jeux jeux 
        {
            get { return jeu; }
            set { jeu = value; }
        }
        public FormePartie()
        {
            InitializeComponent();
            this.Load += FormePartie_Load;
        }

        private void FormePartie_Load(object sender, EventArgs e)
        {


            this.DoubleBuffered = true; //This property reduces flickering by drawing the form's content to an off-screen buffer before displaying it on the screen.
            this.KeyPreview = true;
            this.KeyDown += FormePartie_KeyDown;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;   // gives you an X button
            this.WindowState = FormWindowState.Maximized;
            this.TV.Location = new Point(0, 0);
            this.TV.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            this.jeux = new Jeux(this.TV);
            forme.Hide();

            if (!MenuForme.RecuperSave)
            {
                string Regles = "Règles du jeu UNO :\n\n" +
                    "1. Le jeu se joue avec un paquet de 108 cartes (4 couleurs : rouge, jaune, vert, bleu + cartes spéciales).\n" +
                    "2. Chaque joueur reçoit 7 cartes au début de la partie.\n" +
                    "3. À tour de rôle, chaque joueur doit poser une carte de la même couleur OU du même chiffre/symbole que la carte du dessus de la défausse.\n" +
                    "4. Si un joueur ne peut pas jouer, il pioche une carte.\n" +
                    "5. Cartes spéciales :\n" +
                    "   - +2 : le joueur suivant pioche 2 cartes et passe son tour.\n" +
                    "   - Inversion : le sens du jeu change.\n" +
                    "   - Passer : le joueur suivant passe son tour.\n" +
                    "   - Joker : change la couleur en cours.\n" +
                    "   - Joker +4 : change la couleur ET le joueur suivant pioche 4 cartes.\n" +
                    "6. Quand un joueur n'a plus qu'une seule carte, il doit dire \"UNO !\".\n" +
                    "7. Le premier joueur à se débarrasser de toutes ses cartes gagne la partie.\n\n" +
                    "Bonne partie !";



                MessageBox.Show(Regles, "Règles", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void FormePartie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                forme.Show();
            }
        }
    }
}
