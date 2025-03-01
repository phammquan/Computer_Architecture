using System;
using UnityEngine;

public class SnapToPosition : MonoBehaviour
{
    [SerializeField] private string _targetName;
    private Rigidbody _rigi;
    private Collider _collider;
    private bool _isSnapped = false; 

    private void Start()
    {
        _rigi = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError("Collider không tồn tại trên object này!", this);
        }
    }

    private void Update()
    {
        _rigi.isKinematic = !_rigi.useGravity;
    }

    private void SnapTo(GameObject target)
    {
        if (_isSnapped) return; // Nếu đã snap thì không làm gì
        
        this.transform.position = target.transform.position;
        this.transform.rotation = target.transform.rotation;

        _rigi.useGravity = false;
        _rigi.isKinematic = true;
        this.transform.SetParent(target.transform);

        Debug.Log("Snapped to: " + target.name);
    }

    private void UnSnap()
    {
        _rigi.useGravity = true;
        _rigi.isKinematic = false;
        this.transform.SetParent(null);
        Debug.Log("UnSnapped!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == _targetName)
        {
            SnapTo(other.gameObject);
            _isSnapped = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == _targetName)
        {
            UnSnap();
            _isSnapped = false;
        }
    }
    
}
