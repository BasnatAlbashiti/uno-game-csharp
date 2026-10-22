using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VraiJeu
{

    [Serializable] // so cards can be saved to disk.
    public enum Couleur
    {
        Rouge,
        Jaune,
        Vert,
        Bleu,
        Noir
    }

    [Serializable]
    public enum Valeur
    {
        Zero,
        Un,
        Deux,
        Trois,
        Quatre,
        Cinq,
        Six,
        Sept,
        Huit,
        Neuf,
        Skip,           
        Reverse,        
        PlusDeux,      
        Joker,          
        JokerPlusQuatre
    }

    [Serializable]
    public class Carte
    {
        private Couleur couleur;
        private Valeur valeur;

        public Couleur Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }

        public Valeur Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }


        public Carte(Couleur c, Valeur v)
        {
            this.couleur = c;
            this.valeur = v;
        }

        public override string ToString()
        {
            return couleur.ToString() + " " + valeur.ToString();    
        }

    }
}
