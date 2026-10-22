using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOGame.Models
{
    public enum CardColor
    {
        Yellow, Green, Blue, Red

    }
    public class Card
    {
        

            public CardColor Color { get; set; }
            public int Value { get; set; }

        public Card( CardColor color,int value) 
        {
            Color = color;
            Value=value;

                    
        }

        public bool CanBePlayedOn(Card otherCard)
        {
            return this.Color==otherCard.Color || this.Value==otherCard.Value;
        }

        public override string ToString()
        {
            
            return$"{Color} {Value}";
        }




    }
}
