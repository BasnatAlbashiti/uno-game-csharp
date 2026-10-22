using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNOGame.Models;

namespace UNOGame.GameLogic
{
    public class GameManager
    {
        public List <Player> Players {  get; set; }
        public int CurrentPlayerIndex {  get; set; }

        public Card CurrentCard {  get; set; }
        public Stack<Card> DrawPile { get; set; }


        public GameManager()
        {
            Players = new List<Player>();
            DrawPile = new Stack<Card>();
        }


        public void StartGame()
        {
            Players.Add(new Player("Player 1"));
            Players.Add(new Player("Player 2"));

            CreateDrawPile();

            foreach (var player in Players)
            {
                for(int i= 0; i < 5; i++)
                {
                    player.AddCard(DrawCard());
                }

            }

            CurrentCard = DrawCard();

            CurrentPlayerIndex = 0;


        }

        private void CreateDrawPile()
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                for (int i=0; i <= 9; i++)
                {
                    DrawPile.Push(new Card(color, i));
                }
            }
        }

        public Card DrawCard()
        {
            if(DrawPile.Count == 0)
                return null;
            return DrawPile.Pop();
        }


        public bool PlayCard(Player player,Card card)
        {

        }

    }
}
