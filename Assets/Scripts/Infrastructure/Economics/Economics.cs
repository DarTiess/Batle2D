using UnityEngine;

namespace Infrastructure.Economics
{
    public class Economics : MonoBehaviour
    {
     
        public int Money
        {
            get { return PlayerPrefs.GetInt("Money");; }
            set { PlayerPrefs.SetInt("Money", value); }
        }
           
        public void UseMoney(int count)
        {
            Money += count; 
        }
                          
        public bool CanPayPrice(int price)     
        { 
            if (price > Money) return false;
            return true;
        }
    }
}
