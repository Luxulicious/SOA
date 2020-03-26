using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    private Color _originalColor;

    void OnEnable()
    {
        if (!_sp)
            _sp = this.GetComponent<SpriteRenderer>();
        _originalColor = _sp.color;
    }

    public void SetColorToRed()
    {
        if (!_sp)
            _sp = this.GetComponent<SpriteRenderer>();
        _sp.color = Color.red;
    }

    public void SetColorToBlue()
    {
        if (!_sp)
            _sp = this.GetComponent<SpriteRenderer>();
        _sp.color = Color.blue;
    }

    public void RevertColor()
    {
        if (!_sp)
            _sp = this.GetComponent<SpriteRenderer>();
        _sp.color = _originalColor;
    }
}