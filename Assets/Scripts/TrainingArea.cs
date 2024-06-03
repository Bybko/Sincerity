using UnityEngine;
using System.Collections.Generic;

public class TrainingArea : MonoBehaviour
{
    public int borderNum = 0;
    [SerializeField] private EventHandler _events;    
    [SerializeField] private List<CharacterAgents> _characterAgents = new List<CharacterAgents>();


    private void Start()
    {
        //_events.OnCharacterDestroy += CharactersAnalize;
        _events.OnEpisodeEnd += EpisodeReset;
    }


    private void EpisodeReset()
    {
        foreach (CharacterAgents agent in _characterAgents)
        {
            agent.TotalEndEpisode();
            agent.ResetAgent();
            
            //if (agent.prevHappiness < agent.GetCurrentHappiness()) { agent.SetComplexReward(1f); }
        }
        _events.OnEpisodeReset.Invoke();
    }


    private void CharactersAnalize()
    {
        int numOfAlives = 0;
        foreach (CharacterAgents agent in _characterAgents)
        {
            if (agent.gameObject.GetComponent<PhysicalStatus>().GetHealth() > 0f) 
            {
                agent.SetActionReward(1f);
                numOfAlives++;
            }
        }
        if (numOfAlives <= borderNum) { EpisodeReset(); }
    }
}
