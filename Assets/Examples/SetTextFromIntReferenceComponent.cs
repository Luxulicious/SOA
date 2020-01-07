using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextFromIntReferenceComponent : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private IntReferenceComponent _component;
    [SerializeField] private bool _useUpdate = false;

    void Update()
    {
        if (!_useUpdate) return;
        if (_text != null && _component != null)
            _text.text = _component.reference1.Value.ToString();
    }

    public void SetText(int i)
    {
        if (_text != null && _component != null)
            _text.text = i.ToString();
    }

    public void SetText(int i, int j)
    {
        if (_text != null && _component != null)
            _text.text = $"{i.ToString()} + {j.ToString()}";
    }
}