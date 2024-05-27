using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EscapeAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private Brain _brain;
    private NavMeshAgent _navMesh;
    private GameObject _character;


    public EscapeAction(GameObject character)
    {
        _character = character;
        _brain = character.GetComponentInChildren<Brain>();
        _navMesh = character.GetComponent<NavMeshAgent>();
    }


    public void Action()
    {
        float runDistance = 10f;
        Vector3 directionToChaser = _character.transform.position - _connectedObject.GetObjectPosition();
        directionToChaser.Normalize();

        float angle = 30f;

        bool isNavMeshArea = false;
        while (!isNavMeshArea)
        {
            float randomAngle = Random.Range(-angle, angle);
            Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * directionToChaser;
            Vector3 randomRunTo = _character.transform.position + randomDirection * runDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomRunTo, out hit, runDistance, NavMesh.AllAreas))
            {
                randomRunTo.x = hit.position.x;
                randomRunTo.z = hit.position.z;

                Vector3 directionToTarget = randomRunTo - _character.transform.position;
                float distanceToTarget = directionToTarget.magnitude;

                Ray ray = new Ray(_character.transform.position, directionToTarget);
                RaycastHit raycastHit;

                if (!Physics.Raycast(ray, out raycastHit, distanceToTarget))
                {
                    isNavMeshArea = true;
                }
            }
        }
    }


    public void SelfDelete()
    {
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
