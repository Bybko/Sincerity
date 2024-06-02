using UnityEngine;
using System.Collections.Generic;

public class TrainingArea : MonoBehaviour
{
    [SerializeField] private EventHandler _events;    
    [SerializeField] private List<CharacterAgents> _characterAgents = new List<CharacterAgents>();


    private void Start()
    {
        _events.OnCharacterDestroy += CharactersAnalize;
    }


    private void EpisodeReset()
    {
        Debug.Log(_characterAgents.Count);
        foreach (CharacterAgents agent in _characterAgents)
        {
            agent.TotalEndEpisode();
            agent.ResetAgent();
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
        if (numOfAlives <= 1) { EpisodeReset(); }
    }
}
