using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public Action OnActionRemove;

    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private Receptors _receptors;

    [Header("Agents")]
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private InstinctActionAgent _instinctsAction;
    [SerializeField] private EmotionalBrainAgent _emotions;
    [SerializeField] private BrainAgent _brainDecision;
    [SerializeField] private BrainActionAgent _brainAction;

    private bool _isEmotionalDecisionReady = false;
    private bool _isInstinctDecisionReady = false;
    private bool _isInstinctActionReady = false;
    private bool _isFinalDecisionReady = false;
    private bool _isFinalActionReady = false;

    private Vector3 _searchingPosition;
    private float _distanceThreshold = 0.3f;
    private bool _isSearching = false;

    
    private void Update()
    {
        //maybe too heavy for every frame update
        AnalizeDecision();
    }


    public IEnumerator AnalizeForeignObject(ForeignObject foreignObject)
    {
        bool isTraining = true;
        MemoryObject rememberedObject = _memory.Remember(foreignObject);
        if (rememberedObject == null || isTraining)
        {
            _subconscious.AddDiscoveryAward(foreignObject);

            Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
            _instincts.SetFeeling(feeling);
            _emotions.SetFeeling(feeling);
            _brainDecision.SetFeeling(feeling);

            yield return StartCoroutine(RequestBrainDecision(foreignObject));

            if (_instinctsAction.GetAction() != null) { _instinctsAction.GetAction().Action(); }
            else
            {
                CreateReward(foreignObject);

                if (rememberedObject == null)
                {
                    _memory.MemorizeObject(foreignObject, _instincts.GetInstinctDecision(),
                        _emotions.GetEmotionalDecision(), _brainDecision.GetFinalDecision());
                }
                _memory.SetNewAction(foreignObject, _brainAction.GetAction());
            }
        }
        else
        {
            yield return StartCoroutine(RequestBrainAction(rememberedObject));

            if (_instinctsAction.GetAction() != null) { _instinctsAction.GetAction().Action(); }
            else { _memory.SetNewAction(foreignObject, _brainAction.GetAction()); }
        }
    }


    public void TellAboutReachingObject(ForeignObject goal)
    {
        _memory.ReachObject(goal);
    }

    
    public void ResetMemory()
    {
        _memory.ClearLists();
    }


    public void StopMoving()
    {
        _navMesh.isStopped = true;
    }


    private IEnumerator RequestBrainDecision(ForeignObject foreignObject)
    {
        _instinctsAction.SetCurrentForeignObject(foreignObject);
        _brainAction.RequestDecision();

        yield return new WaitUntil(() => _isInstinctActionReady);

        if (_instinctsAction.GetAction() == null)
        {
            _instincts.RequestDecision();
            _emotions.RequestDecision();

            yield return new WaitUntil(() => _isEmotionalDecisionReady && _isInstinctDecisionReady);

            //reset for next decision
            IsEmotionalDecisionReady(false);
            IsInstinctsDecisionReady(false);

            _brainDecision.SetInputs(_instincts.GetInstinctDecision(), _emotions.GetEmotionalDecision());
            _brainDecision.RequestDecision();

            yield return new WaitUntil(() => _isFinalDecisionReady);

            IsFinalDecisionReady(false);

            _brainAction.SetInputs(_brainDecision.GetFinalDecision());
            _brainAction.SetCurrentForeignObject(_physicalStatus.GetCurrentForeignObject());
            _brainAction.RequestDecision();

            yield return new WaitUntil(() => _isFinalActionReady);

            IsFinalActionReady(false);
        }
    }


    private IEnumerator RequestBrainAction(MemoryObject rememberedObject)
    {
        _instinctsAction.SetCurrentForeignObject(rememberedObject.GetObjectImage());
        _brainAction.RequestDecision();

        yield return new WaitUntil(() => _isInstinctActionReady);

        if (_instinctsAction.GetAction() == null)
        {
            IsInstinctsActionReady(false);

            _brainAction.SetInputs(rememberedObject.GetFinalDecision());
            _brainAction.SetCurrentForeignObject(_physicalStatus.GetCurrentForeignObject());
            _brainAction.RequestDecision();

            yield return new WaitUntil(() => _isFinalActionReady);

            IsFinalActionReady(false);

            _memory.SetNewAction(rememberedObject.GetObjectImage(), _brainAction.GetAction());
        }
    }


    private void AnalizeDecision()
    {
        if (_subconscious.IsWantToSleep() && !_subconscious.SleepingStatus())
        {
            StartCoroutine(Sleep());
        }

        if (!_subconscious.SleepingStatus() && _receptors.IsCoroutinesQueueOver())
        {
            MemoryObject newGoal = _memory.GetMostWantedObjectWithAction();
            if (newGoal != null)
            {
                _isSearching = false;
                _memory.AddNewGoal(newGoal);
                newGoal.GetAction().Action();
            }
            else if(_isSearching == false ||
                Vector3.Distance(_searchingPosition, _characterTransform.position) < _distanceThreshold) 
            {
                Search(); 
            }
        }
    }


    private IEnumerator Sleep()
    {
        _subconscious.FallAsleep();

        float energyRecovery = 10f;
        do
        {
            yield return new WaitForSeconds(5);
            _physicalStatus.ChangeEnergy(energyRecovery);

        } while (_subconscious.SafetySatus() && _subconscious.IsWantToSleep());

        _subconscious.WakeUp();
    }


    private void Search()
    {
        _isSearching = true;

        float searchRadius = 10f;
        float randomX = 0f;
        float randomZ = 0f;
        Vector3 randomPosition = _characterTransform.position;

        bool isNavMeshArea = false;
        while (!isNavMeshArea)
        {
            randomX = _characterTransform.position.x + UnityEngine.Random.Range(-searchRadius, searchRadius);
            randomZ = _characterTransform.position.z + UnityEngine.Random.Range(-searchRadius, searchRadius);
            randomPosition = new Vector3(randomX, _characterTransform.position.y, randomZ);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, searchRadius, NavMesh.AllAreas))
            {
                randomPosition.x = hit.position.x;
                randomPosition.z = hit.position.z;

                Vector3 directionToTarget = randomPosition - _characterTransform.position;
                float distanceToTarget = directionToTarget.magnitude;

                Ray ray = new Ray(_characterTransform.position, directionToTarget);
                RaycastHit raycastHit;

                if (!Physics.Raycast(ray, out raycastHit, distanceToTarget))
                {
                    isNavMeshArea = true;
                }
            }
        }

        _searchingPosition = randomPosition;
        _navMesh.SetDestination(randomPosition);
    }


    private void CreateReward(ForeignObject foreignObject)
    {
        //Just only for food yet
        float damage = foreignObject.GetDamageValue();
        float foodValue = foreignObject.GetFoodValue();
        if (foreignObject is StorageObject) 
        {
            if (_instincts.GetInstinctDecision() > 0) { _instincts.SetReward(2f); } else { _instincts.SetReward(-2f); }
            if (_emotions.GetEmotionalDecision() > 0) { _emotions.SetReward(2f); } else { _emotions.SetReward(-2f); }
            if (_brainDecision.GetFinalDecision() > 0) { _brainDecision.SetReward(2f); } else { _brainDecision.SetReward(-2f); }
        }
        else if(damage <= 0 && foodValue < 0)
        {
            if (_instincts.GetInstinctDecision() > 0) { _instincts.SetReward(-1f); } else { _instincts.SetReward(1f); }
            if (_emotions.GetEmotionalDecision() > 0) { _emotions.SetReward(-1f); } else { _emotions.SetReward(1f); }
            if (_brainDecision.GetFinalDecision() > 0) { _brainDecision.SetReward(-1f); } else { _brainDecision.SetReward(1f); }
        }
        else if (damage > 50 || foodValue > 50) 
        {
            if (_instincts.GetInstinctDecision() > 0) { _instincts.SetReward(1f); } else {  _instincts.SetReward(-1f);}
            if (_emotions.GetEmotionalDecision() > 0) { _emotions.SetReward(1f); } else { _emotions.SetReward(-1f); }
            if (_brainDecision.GetFinalDecision() > 0) { _brainDecision.SetReward(1f); } else { _brainDecision.SetReward(-1f); }
        }
    }


    public void ResetSearchStatus() { _isSearching = false; }
    public void IsInstinctsDecisionReady(bool newStatus) { _isInstinctDecisionReady = newStatus; }
    public void IsInstinctsActionReady(bool newStatus) { _isInstinctActionReady = newStatus; }
    public void IsEmotionalDecisionReady(bool newStatus) { _isEmotionalDecisionReady = newStatus; }
    public void IsFinalDecisionReady(bool newStatus) { _isFinalDecisionReady = newStatus; }
    public void IsFinalActionReady(bool newStatus) { _isFinalActionReady = newStatus; }
}
