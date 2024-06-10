using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class UsingItem : MonoBehaviour
{

    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _startPerent;
    [SerializeField] private float _maxDistance = 1.5f;
    [SerializeField] private float _force = 5f;
    private Collider _item;
    private Rigidbody _itemRigidBody;

    private void Update()
    {
        GetItemInHand();
        DropItem();
    }
    
    private void GetItemInHand()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
           if (hit.collider.gameObject.layer == LayerMask.NameToLayer("mark") && Input.GetKeyDown(KeyCode.E))
           {
               if (_item == null)
               {
                   GrabItem(hit);
               }
               else
               {
                   _item.transform.SetParent(_startPerent, true);
                   _itemRigidBody.isKinematic = false;
                   GrabItem(hit);
               }
           }
        }
    }
    
    private void GrabItem(RaycastHit hit)
    {
        _item = hit.collider;
        _itemRigidBody = _item.GetComponent<Rigidbody>();
        _itemRigidBody.isKinematic = true;
        _item.transform.SetParent(_hand, true);
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localRotation = Quaternion.identity;
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_item != null)
            {
                _item.transform.SetParent(_startPerent, true);
                _itemRigidBody.isKinematic = false;
                _itemRigidBody.AddRelativeForce(Vector3.forward * _force, ForceMode.Impulse);
                _item = null;
                _itemRigidBody = null;
            }
        }
    }
}

