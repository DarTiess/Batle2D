using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LeaderBoard {
    [Serializable]
    public class StatisticsUi
    {
        public GameObject Zone;
        public Text Name, Power;  
    }

    [Serializable]
    public class StatisticsChar
    {
        public string Name; 
        public int Power;
        public Sprite Sprite;
    }

    public class LeaderBoard : MonoBehaviour
    {    
#pragma warning disable 0649 
        [SerializeField] private float _timeUpdate;  private float _timerUpdate;
    
//    public BodyStructure Character;
        public List<StatisticsUi> ListUiZone;
        public List<StatisticsChar> _listUiLead;
#pragma warning restore 0649     

//    private void Start()
//    {Character = CharacterControl.McThis.gameObject.GetComponent<BodyStructure>();}

        void Update()
        {
            _timerUpdate += Time.deltaTime;
            if(_timerUpdate<_timeUpdate) return;
            _timerUpdate = 0;
            UpdateLeaderBoard();    
        }
     
        public void UpdateLeaderBoard()    
        {
            /*
        _listUiLead.Clear();
        
        StatisticsChar cha = new StatisticsChar();
        cha.Power = Character.Eaten;
        cha.Name = Character.Name;
        _listUiLead.Add(cha); 
        
        foreach (var bot in LevelManager.McThis.ListBot)
        {    
            StatisticsChar ch = new StatisticsChar();
            ch.Power = bot.Eaten;
            ch.Name = bot.Name;
            _listUiLead.Add(ch);
        }

        _listUiLead = _listUiLead.OrderBy(i => -i.Power).ToList();

        for (int i = 0; i < ListUiZone.Count; i++)
        {
            if (_listUiLead.Count > i)
            {
                ListUiZone[i].Zone.SetActive(true);
                ListUiZone[i].Name.text = _listUiLead[i].Name;
                
                if (_listUiLead[i].Name == Character.Name){ListUiZone[i].Name.color = Color.yellow;}
                else{ListUiZone[i].Name.color = Color.white;}

                ListUiZone[i].Power.text = _listUiLead[i].Power.ToString();
            }
            else
            {ListUiZone[i].Zone.SetActive(false);}
        }
        */
        }   
    }
}