using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    Collider2D[] _colliders;
    public Action<Collision2D> CollisionEnter;
    public Action<Collision2D> CollisionExit;

    public Action<Collider2D> TriggerEnter;
    public Action<Collider2D> TriggerExit;

    private void Awake()
    {
        _colliders = GetComponents<Collider2D>();
    }
    public void SetAsTrigger(bool isTrigger)
    {
        if(_colliders == null || _colliders.Length==0)
            _colliders = GetComponents<Collider2D>();
        foreach (var t in _colliders)
        {
            t.isTrigger = isTrigger;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionExit?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerEnter?.Invoke(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        TriggerExit?.Invoke(collider);
    }
}
