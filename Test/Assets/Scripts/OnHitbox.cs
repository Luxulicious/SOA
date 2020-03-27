using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using UnityEngine;
using UnityEngine.Events;

public class OnHitbox : MonoBehaviour
{
    [SerializeField] private UnityEvent _onHitboxEnterEvent = new UnityEvent();
    [SerializeField] private UnityEvent _onHitboxExitEvent = new UnityEvent();

    void OnTriggerEnter2D(Collider2D col)
    {
        _onHitboxEnterEvent.Invoke();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _onHitboxExitEvent.Invoke();
    }
}