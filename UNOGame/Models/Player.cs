using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOGame.Models
{
    public class Player
    {
        public string Name {  get; set; }
        public List<Card> Hand {  get; set; }

        public Player( string name)
        {
            Name = name;
            Hand = new List<Card>();
        }


        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public void RemoveCard(Card card) 
        {
            Hand.Remove(card);
        }


    }
}
