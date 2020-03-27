using System.Collections;
using System.Collections.Generic;
using SOA.Common.Primitives;
using UnityEngine;
using UnityEngine.UI;

public class DisplayIntToToText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private IntReference _integer;

    void OnEnable()
    {
        if (!_text)
            _text = this.GetComponent<Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = $"HP: {_integer.Value.ToString()}";
    }

    void OnDisable()
    {
        _text.text = "HP: N/A";
    }

}