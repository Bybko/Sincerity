using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class TrainingArea : Agent
{
    [SerializeField] private EventHandler _events;    
    [SerializeField] private List<CharacterAgents> characterAgents = new List<CharacterAgents>();


    private void Start()
    {
        _events.OnEpisodeEnd += CheckTrainEpisode;
    }


    public override void OnEpisodeBegin()
    {
        _events.OnEpisodeReset.Invoke();

        foreach (CharacterAgents agent in characterAgents) { agent.ResetAgent(); }
    }


    public void CheckTrainEpisode()
    {
        foreach (CharacterAgents agent in characterAgents)
        {
            if (agent.GetAgentHealth() <= 0)
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
            }

            agent.TotalEndEpisode();
        }

        EndEpisode();
    }
}
