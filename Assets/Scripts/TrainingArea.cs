using UnityEngine;
using System.Collections.Generic;

public class TrainingArea : MonoBehaviour
{
    [SerializeField] private EventHandler _events;    
    [SerializeField] private List<CharacterAgents> characterAgents = new List<CharacterAgents>();

    //temp
    [SerializeField] private StorageObject _storageObject;


    private void Start()
    {
        _events.OnEpisodeEnd += CheckTrainEpisode;
    }


    private void Update()
    {
        if (!_storageObject.IsFree()) { _events.OnEpisodeEnd.Invoke(); }
    }


    public void CheckTrainEpisode()
    {
        foreach (CharacterAgents agent in characterAgents)
        {
            agent.SetActionReward(_storageObject.GetStoredAward());

            /*if (agent.GetAgentHealth() <= 0)
            {
                agent.SetActionReward(-1f);
            }

            if (agent.GetAgentHealth() == 100)
            {
                agent.SetActionReward(1f);
            }

            if (agent.GetAgentFoodResources() == 100)
            {
                agent.SetActionReward(0.8f);
            }*/
        }

        EpisodeReset();
    }


    private void EpisodeReset()
    {
        foreach (CharacterAgents agent in characterAgents)
        {
            agent.TotalEndEpisode();
            agent.ResetAgent();
        }

        _events.OnEpisodeReset.Invoke();
    }
}
