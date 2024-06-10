using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObject : MonoBehaviour
{
    private bool _isNewObject;
    [SerializeField] private float _maxDistance = 1.5f;
    private InteractableItem _latestObject;
    void Update()
    {
        Lightning();
    }

    private void Lightning()
    {
        var currentObject = GetInteractableItem();
        _isNewObject = currentObject != _latestObject;
        if (_isNewObject)
        {
            if (_latestObject != null)
            {
                _latestObject.RemoveFocus();
            }
            
            if (currentObject != null)
            {
                currentObject.SetFocus();
            }
            
            _latestObject = currentObject;
        }
    }
    
    private InteractableItem GetInteractableItem()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("mark"))
            {
                var item = hit.collider.GetComponent<InteractableItem>();
                return item;
            }
        }
        return null;
    }
}
