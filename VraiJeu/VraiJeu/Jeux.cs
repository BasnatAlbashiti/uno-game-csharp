using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace VraiJeu
{
    [Serializable]  //Without it, save/load would fail
    public class Jeux
    {
        #region Constantes
        const int NbcartesDepart = 7;
        #endregion Constantes

        #region Attributs
        [NonSerialized] //PictureBox is a Windows control, and Windows controls cannot be saved to disk


        private PictureBox TV;

        [NonSerialized]

        public Button[] btnMain1;

        [NonSerialized]

        public Button[] btnMain2;

        [NonSerialized]
        public Button btnPioche; //the face-down deck the player clicks to draw a card.

        [NonSerialized]
        public Button btnDefausse; // the top of the discard pile, showing what the next player has to match.


        [NonSerialized]
        public Label lblTour;

        //[NonSerialized]

        //public Label lblJoueur1;

        private List<Carte> mainJoueur1;
        private List<Carte> mainJoueur2;
        private List<Carte> pioche;
        private List<Carte> defausse;

        // These are NOT marked [NonSerialized] they ARE the game state, they MUST be saved.

        private bool tour = true;//true = "Player 1 starts".
        private bool sens = true;//true = normal, false = reversed.
        private Couleur couleurEnCours;

        #endregion Attributs

        #region Accesseurs
        public PictureBox tv
        {
            get { return TV; }
            set { TV = value; }
        }


        public bool Tour
        {
            get { return tour; }
            set { tour = value; }
        }


        public bool Sens
        {
            get { return sens; }
            set { sens = value; }
        }


        public Couleur CouleurEnCours
        {
            get { return couleurEnCours; }
            set { couleurEnCours = value; }
        }


        public List<Carte> Pioche
        {
            get { return pioche; }
            set { pioche = value; }
        }


        public List<Carte> Defausse
        {
            get { return defausse; }
            set { defausse = value; }
        }


        public List<Carte> MainJoueur1
        {
            get { return mainJoueur1; }
            set { mainJoueur1 = value; }
        }


        public List<Carte> MainJoueur2
        {
            get { return mainJoueur2; }
            set { mainJoueur2 = value; }
        }

        #endregion Accesseurs

        #region Constructeur

        public Jeux(PictureBox TV)
        {
            this.TV = TV;

            if (!MenuForme.RecuperSave)
            {
                // NOUVELLE PARTIE
                this.pioche = new List<Carte>();
                this.defausse = new List<Carte>();
                this.mainJoueur1 = new List<Carte>();
                this.mainJoueur2 = new List<Carte>();
                this.tour = true;
                this.sens = true;


                CreerPioche();   // crée les 108 cartes
                Melanger();      // mélange aléatoirement
                Distribuer();    // distribue les cartes aux joueurs et place la première carte sur la défausse




            }
            else
            {
                this.pioche = MenuForme.ChargerPartie.Pioche;
                this.defausse = MenuForme.ChargerPartie.Defausse;
                this.mainJoueur1 = MenuForme.ChargerPartie.MainJoueur1;
                this.mainJoueur2 = MenuForme.ChargerPartie.MainJoueur2;
                this.tour = MenuForme.ChargerPartie.Tour;
                this.sens = MenuForme.ChargerPartie.Sens;
                this.couleurEnCours = MenuForme.ChargerPartie.CouleurEnCours;

            }

            AfficherJeu();
        }

        #endregion Constructeur


        #region Méthodes


        private void CreerPioche()
        {
            // Les 4 couleurs principales (Noir est réservé aux Jokers)
            Couleur[] couleurs = { Couleur.Rouge, Couleur.Jaune, Couleur.Vert, Couleur.Bleu };

            foreach (Couleur c in couleurs)
            {
                // 1 carte "0" par couleur
                pioche.Add(new Carte(c, Valeur.Zero));

                // 2 cartes de chaque chiffre de 1 à 9
                Valeur[] chiffres = {
            Valeur.Un, Valeur.Deux, Valeur.Trois, Valeur.Quatre, Valeur.Cinq,
            Valeur.Six, Valeur.Sept, Valeur.Huit, Valeur.Neuf
        };

                foreach (Valeur v in chiffres)
                {
                    pioche.Add(new Carte(c, v));
                    pioche.Add(new Carte(c, v));
                }

                // 2 Skip, 2 Reverse, 2 +2 par couleur
                for (int i = 0; i < 2; i++)
                {
                    pioche.Add(new Carte(c, Valeur.Skip));
                    pioche.Add(new Carte(c, Valeur.Reverse));
                    pioche.Add(new Carte(c, Valeur.PlusDeux));
                }
            }

            // 4 Jokers et 4 Joker+4 (couleur Noir)
            for (int i = 0; i < 4; i++)
            {
                pioche.Add(new Carte(Couleur.Noir, Valeur.Joker));
                pioche.Add(new Carte(Couleur.Noir, Valeur.JokerPlusQuatre));
            }
        }


        private void Melanger()
        {
            Random rng = new Random();
            List<Carte> nouvelle = new List<Carte>();

            while (pioche.Count > 0)
            {
                int index = rng.Next(pioche.Count);
                nouvelle.Add(pioche[index]);
                pioche.RemoveAt(index);
            }

            pioche = nouvelle;

            // Pick a random card from pioche, move it to nouvelle, repeat until pioche is empty
        }


        private void Distribuer()
        {
            for (int i = 0; i < NbcartesDepart; i++)
            {
                mainJoueur1.Add(pioche[0]);
                pioche.RemoveAt(0);
                mainJoueur2.Add(pioche[0]);
                pioche.RemoveAt(0);
            }

            while (
                pioche[0].Valeur == Valeur.Joker ||
                pioche[0].Valeur == Valeur.JokerPlusQuatre ||
                pioche[0].Valeur == Valeur.Skip ||
                pioche[0].Valeur == Valeur.Reverse ||
                pioche[0].Valeur == Valeur.PlusDeux
                ) ;

            Carte temp = pioche[0];      // remember the top card
            pioche.RemoveAt(0);           // remove it from the top
            pioche.Add(temp);             // put it at the bottom of the deck

            defausse.Add(pioche[0]);           // put it on the discard pile
            pioche.RemoveAt(0);           // remove it from the top of the deck

            couleurEnCours = defausse[0].Couleur;


        }


        private Button CreerBoutonCarte(Carte c)
        {
            Button b = new Button();
            b.Size = new Size(80, 120);
            b.Font = new Font("Arial", 14, FontStyle.Bold);
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;


            switch (c.Couleur)
            {
                case Couleur.Rouge:
                    b.BackColor = Color.Red;
                    break;
                case Couleur.Jaune:
                    b.BackColor = Color.Gold;
                    break;
                case Couleur.Vert:
                    b.BackColor = Color.Green;
                    break;
                case Couleur.Bleu:
                    b.BackColor = Color.Blue;
                    break;
                case Couleur.Noir:
                    b.BackColor = Color.Black;
                    break;
            }

            switch (c.Valeur)
            {
                case Valeur.Zero:
                    b.Text = "0";
                    break;
                case Valeur.Un:
                    b.Text = "1";
                    break;
                case Valeur.Deux:
                    b.Text = "2";
                    break;
                case Valeur.Trois:
                    b.Text = "3";
                    break;
                case Valeur.Quatre:
                    b.Text = "4";
                    break;
                case Valeur.Cinq:
                    b.Text = "5";
                    break;
                case Valeur.Six:
                    b.Text = "6";
                    break;
                case Valeur.Sept:
                    b.Text = "7";
                    break;
                case Valeur.Huit:
                    b.Text = "8";
                    break;
                case Valeur.Neuf:
                    b.Text = "9";
                    break;
                case Valeur.Skip:
                    b.Text = "Skip";
                    break;
                case Valeur.Reverse:
                    b.Text = "Reverse";
                    break;
                case Valeur.PlusDeux:
                    b.Text = "+2";
                    break;
                case Valeur.Joker:
                    b.Text = "Joker";
                    break;
                case Valeur.JokerPlusQuatre:
                    b.Text = "Joker +4";
                    break;
            }
            return b;
        }


        // Crée un bouton qui ressemble au DOS d'une carte UNO (face cachée)
        private Button CreerDosCarte()
        {
            Button b = new Button();
            b.Size = new Size(80, 120);
            b.BackColor = Color.Black;
            b.ForeColor = Color.Red;
            b.Text = "UNO";
            b.Font = new Font("Arial", 16, FontStyle.Bold);
            b.FlatStyle = FlatStyle.Flat;
            b.Enabled = false;   // pas cliquable
            return b;
        }

        public void AfficherJeu()
        {
            TV.Controls.Clear();


            int largeurCarte = 80;
            int hauteurCarte = 120;
            int espace = 10;

            // 2) Main du JOUEUR 1 (en bas) — visible seulement si c'est son tour
            btnMain1 = new Button[mainJoueur1.Count];
            for (int i = 0; i < mainJoueur1.Count; i++)
            {
                if (tour)
                {
                    // C'est le tour du Joueur 1 → ses cartes sont visibles et cliquables
                    btnMain1[i] = CreerBoutonCarte(mainJoueur1[i]);
                    btnMain1[i].Tag = "J1_" + i;
                    btnMain1[i].Click += Carte_Click;
                }
                else
                {
                    // Ce n'est pas son tour → on cache ses cartes (dos uniquement)
                    btnMain1[i] = CreerDosCarte();
                }

                btnMain1[i].Location = new Point(20 + i * (largeurCarte + espace),
                                                 TV.ClientSize.Height - hauteurCarte - 20);
                TV.Controls.Add(btnMain1[i]);
            }

            // 3) Main du JOUEUR 2 (en haut) — visible seulement si c'est son tour
            btnMain2 = new Button[mainJoueur2.Count];
            for (int i = 0; i < mainJoueur2.Count; i++)
            {
                if (!tour)
                {
                    // C'est le tour du Joueur 2 → ses cartes sont visibles et cliquables
                    btnMain2[i] = CreerBoutonCarte(mainJoueur2[i]);
                    btnMain2[i].Tag = "J2_" + i;
                    btnMain2[i].Click += Carte_Click;
                }
                else
                {
                    // Ce n'est pas son tour → on cache ses cartes (dos uniquement)
                    btnMain2[i] = CreerDosCarte();
                }

                btnMain2[i].Location = new Point(20 + i * (largeurCarte + espace), 20);
                TV.Controls.Add(btnMain2[i]);
            }

            btnPioche = new Button();
            btnPioche.Size = new Size(largeurCarte, hauteurCarte);
            btnPioche.Location = new Point(TV.ClientSize.Width / 2 - largeurCarte - 20,
                                           TV.ClientSize.Height / 2 - hauteurCarte / 2);
            btnPioche.BackColor = Color.Black;
            btnPioche.ForeColor = Color.White;
            btnPioche.Text = "UNO\n(" + pioche.Count + ")";
            btnPioche.Font = new Font("Arial", 12, FontStyle.Bold);
            btnPioche.FlatStyle = FlatStyle.Flat;
            btnPioche.Click += Pioche_Click;
            TV.Controls.Add(btnPioche);


            Carte carteDessus = defausse[defausse.Count - 1];
            btnDefausse = CreerBoutonCarte(carteDessus);
            btnDefausse.Location = new Point(TV.ClientSize.Width / 2 + 20,
                                             TV.ClientSize.Height / 2 - hauteurCarte / 2);
            TV.Controls.Add(btnDefausse);


            lblTour = new Label();
            lblTour.Text = tour ? "Tour de Joueur 1" : "Tour de Joueur 2";
            lblTour.Font = new Font("Arial", 18, FontStyle.Bold);
            lblTour.ForeColor = Color.White;
            lblTour.BackColor = Color.Transparent;
            lblTour.AutoSize = true;
            lblTour.Location = new Point(TV.ClientSize.Width / 2 - 110,
                                         TV.ClientSize.Height / 2 - hauteurCarte / 2 - 50);
            TV.Controls.Add(lblTour);


        }


        private void Carte_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string tag = b.Tag.ToString();   // ex : "J1_3"

            bool estJoueur1 = tag.StartsWith("J1_");
            int index = int.Parse(tag.Substring(3));

            // Sécurité : on ne peut jouer que sur son propre tour
            if (estJoueur1 != tour) return;

            List<Carte> mainCourante = estJoueur1 ? mainJoueur1 : mainJoueur2;
            List<Carte> mainAdverse = estJoueur1 ? mainJoueur2 : mainJoueur1;

            Carte carteJouee = mainCourante[index];

            // 1) Vérifier que la carte peut être jouée
            if (!PeutJouer(carteJouee))
            {
                MessageBox.Show("Coup invalide ! La carte doit être de couleur "
                    + couleurEnCours.ToString() + " ou avoir la même valeur que la défausse.",
                    "Coup invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Retirer la carte de la main et l'ajouter à la défausse
            mainCourante.RemoveAt(index);
            defausse.Add(carteJouee);

            // 3) Mettre à jour la couleur en cours
            if (carteJouee.Couleur == Couleur.Noir)
            {
                couleurEnCours = ChoisirCouleur();
            }
            else
            {
                couleurEnCours = carteJouee.Couleur;
            }

            // 4) Vérifier la victoire
            if (mainCourante.Count == 0)
            {
                AfficherJeu();
                MessageBox.Show("Joueur " + (estJoueur1 ? "1" : "2") + " a gagné !",
                                "Victoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 5) Annoncer UNO si plus qu'une carte
            if (mainCourante.Count == 1)
            {
                MessageBox.Show("UNO ! Joueur " + (estJoueur1 ? "1" : "2")
                              + " n'a plus qu'une carte !", "UNO !");
            }

            // 6) Appliquer les effets spéciaux
            bool rejoue = false;

            switch (carteJouee.Valeur)
            {
                case Valeur.Skip:
                case Valeur.Reverse:
                    rejoue = true;
                    break;

                case Valeur.PlusDeux:
                    for (int i = 0; i < 2; i++)
                        if (pioche.Count > 0)
                        {
                            mainAdverse.Add(pioche[0]);
                            pioche.RemoveAt(0);
                        }
                    rejoue = true;
                    break;

                case Valeur.JokerPlusQuatre:
                    for (int i = 0; i < 4; i++)
                        if (pioche.Count > 0)
                        {
                            mainAdverse.Add(pioche[0]);
                            pioche.RemoveAt(0);
                        }
                    rejoue = true;
                    break;
            }

            // 7) Changer de tour si la carte ne fait pas rejouer
            if (!rejoue)
            {
                tour = !tour;
            }

            // 8) Rafraîchir l'affichage
            AfficherJeu();
        }


        private void Pioche_Click(object sender, EventArgs e)
        {
            if (pioche.Count == 0)
            {
                MessageBox.Show("La pioche est vide !", "Pioche vide");
                return;
            }

            // Le joueur courant pioche une carte
            List<Carte> mainCourante = tour ? mainJoueur1 : mainJoueur2;
            mainCourante.Add(pioche[0]);
            pioche.RemoveAt(0);

            // Le tour passe à l'adversaire
            tour = !tour;

            AfficherJeu();
        }


        // Vérifie si la carte c peut être posée sur la défausse actuelle


        private bool PeutJouer(Carte c)
        {
            if (c.Couleur == Couleur.Noir)
                return true; // les Jokers peuvent être joués sur n'importe quelle carte

            if (c.Couleur == couleurEnCours)
                return true; // on peut jouer une carte de la même couleur

            Carte dessus = defausse[defausse.Count - 1];
            if (c.Valeur == dessus.Valeur)
                return true; // on peut jouer une carte du même chiffre/symbole

            return false; // sinon, la carte ne peut pas être jouée
        }


        // Affiche une petite fenêtre pour choisir une couleur (Joker / Joker+4)
        private Couleur ChoisirCouleur()
        {
            Couleur choix = Couleur.Rouge; // if the player dint choose a color the system will set the color red as defulte
            Form dlg = new Form(); // create a new form to display the color choices
            dlg.Text = "Choisissez une couleur"; // set the title of the form (bar)
            dlg.Size = new Size(280, 180); // set the size of the form
            dlg.StartPosition = FormStartPosition.CenterParent; // set the form to open in the center of the parent form (TV)
            dlg.ControlBox = false; // remove the minimize, maximize and close buttons from the form (the player has to chosse a color to get it colsed )

            Button bR = new Button()
            {
                Text = "Rouge",
                BackColor = Color.Red,
                ForeColor = Color.White,
                Size = new Size(110, 50),
                Location = new Point(15, 15),


            }; // create a button for the color red

            bR.Click += (s, e) =>
            {
                choix = Couleur.Rouge;
                dlg.Close();
            }; // when the player click red make it as (choix)and close the dilogbox


            Button bJ = new Button()
            {
                Text = "Jaune",
                BackColor = Color.Goldenrod,
                ForeColor = Color.White,
                Size = new Size(110, 50),
                Location = new Point(140, 15),

            };

            // create a button for the color jaune

            bJ.Click += (s, e) =>
            {
                choix = Couleur.Jaune;
                dlg.Close();
            };


            Button bV = new Button
            {
                Text = "Vert",
                BackColor = Color.Green,
                ForeColor = Color.White,
                Size = new Size(110, 50),
                Location = new Point(15, 75)
            };
            bV.Click += (s, e) => { choix = Couleur.Vert; dlg.Close(); };

            Button bB = new Button
            {
                Text = "Bleu",
                BackColor = Color.Blue,
                ForeColor = Color.White,
                Size = new Size(110, 50),
                Location = new Point(140, 75)
            };
            bB.Click += (s, e) => { choix = Couleur.Bleu; dlg.Close(); };

            dlg.Controls.Add(bR);
            dlg.Controls.Add(bJ);
            dlg.Controls.Add(bV);
            dlg.Controls.Add(bB);

            dlg.ShowDialog();
            return choix;

        }

        public bool Sauvegarder()
        {
            if (pioche == null || defausse == null ||
        mainJoueur1 == null || mainJoueur2 == null)
            {
                return false;
            }

            if (defausse.Count == 0)
            {
                // Pas de carte de défausse → pas de partie en cours
                return false;
            }

            return true;
        }

        #endregion Méthodes

    }
}