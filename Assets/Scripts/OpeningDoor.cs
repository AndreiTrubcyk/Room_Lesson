using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 1.5f;
    void Update()
    {
        var ray = GetInteractableItem();
        if (ray != null && Input.GetKeyDown(KeyCode.E))
        {
            ray.SwitchDoorState();
        }
    }
    
    private Door GetInteractableItem()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit , _maxDistance))
        {
            if (hit.collider.CompareTag("hit"))
            {
                var item = hit.collider.GetComponent<Door>();
                return item;
            }
        }

        return null;
    }
}
