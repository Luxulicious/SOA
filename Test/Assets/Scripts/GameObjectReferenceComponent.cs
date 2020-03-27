using SOA.Common.CustomTypes;
using UnityEngine;

public class GameObjectReferenceComponent : MonoBehaviour
{
    [SerializeField]
    private GameObjectReference _gameobject;
    [SerializeField]
    private bool _assignThisOnAwake;

    void Awake()
    {
        if (_assignThisOnAwake)
            _gameobject.Value = this.gameObject;
    }
}
