using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartController : MonoBehaviour
{
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    private StackController _stackController;
    private Collider _collider;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _stackController = GetComponent<StackController>();
        _collider = GetComponent<Collider>();
    }

    public void Shatter()
    {
        _rb.isKinematic = false;
        _collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXPos = transform.parent.position.x;
        float xPos = _meshRenderer.bounds.center.x;

        Vector3 subDir = (parentXPos - xPos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        _rb.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        _rb.AddTorque(Vector3.left * torque);
        _rb.velocity = Vector3.down;

    }


    // public void RemoveAllChilds()
    // {
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         transform.GetChild(i).SetParent(null);
    //         i--;
    //     }
    // }
}
