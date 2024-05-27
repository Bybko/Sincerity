using UnityEngine;
using System.Collections.Generic;

public class TrainingArea : MonoBehaviour
{
    [SerializeField] private EventHandler _events;    
    [SerializeField] private List<CharacterAgents> characterAgents = new List<CharacterAgents>();

    //temp
    [SerializeField] private StorageObject _storageObject;
    [SerializeField] private int _foreignObjectsNum;


    private void Start()
    {
        _events.OnForeignObjectDestroy += DecreaseForeignObjectsNum;
        _events.OnEpisodeEnd += CheckTrainEpisode;
    }


    private void Update()
    {
        if (!_storageObject.IsFree()) { _events.OnEpisodeEnd.Invoke(); }
        else if(_foreignObjectsNum - _storageObject.NumOfOccupiedCells() < 4) { _events.OnEpisodeEnd.Invoke(); }
    }


    public void CheckTrainEpisode()
    {
        foreach (CharacterAgents agent in characterAgents)
        {
            if (!_storageObject.gameObject.activeSelf) { agent.SetActionReward(-1f); }
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

        _foreignObjectsNum = 4; //reset after testing
        _events.OnEpisodeReset.Invoke();
    }


    private void DecreaseForeignObjectsNum() { _foreignObjectsNum -= 1; }
}
