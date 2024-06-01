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
        foreach (CharacterAgents agent in _characterAgents)
        {
            if (agent.gameObject.activeSelf) { agent.TotalEndEpisode(); }
            agent.ResetAgent();
        }

        _events.OnEpisodeReset.Invoke();
    }


    private void CharactersAnalize()
    {
        int numOfAlives = 0;
        foreach (CharacterAgents agent in _characterAgents)
        {
            if (agent.gameObject.activeSelf) 
            {
                agent.SetActionReward(1f);
                numOfAlives++;
            }
        }
        if (numOfAlives <= 0) { EpisodeReset(); }
    }
}
