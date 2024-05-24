using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCell : MonoBehaviour
{
    private Renderer _selfRenderer;
    private ForeignObject _storedObject = null;
    private bool _isOccupied = false;


    private void Start()
    {
        _selfRenderer = GetComponent<Renderer>();
    }


    public void Teleporter(ForeignObject objectToTeleport)
    {
        if (!_isOccupied)
        {
            Vector3 cellCenter = _selfRenderer.bounds.center;
            float cellHeight = _selfRenderer.bounds.size.y;

            Renderer objectRenderer = objectToTeleport.gameObject.GetComponent<Renderer>();
            float objectHeight = objectRenderer.bounds.size.y;
            Vector3 newPosition = new Vector3(cellCenter.x, cellCenter.y + cellHeight / 2 + objectHeight / 2,
                cellCenter.z);

            objectToTeleport.gameObject.transform.position = newPosition;

            _storedObject = objectToTeleport;
            _isOccupied = true;
        }
    }


    public void CheckStoredObject()
    {
        if (_storedObject == null || !_storedObject.gameObject.activeSelf)
        {
            _storedObject = null;
            _isOccupied = false;
        }
    }


    public void DestroyStoredObject()
    {
        _storedObject.SelfDestroy();
    }


    public bool IsOccupied() {  return _isOccupied; }
}
