using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class ARObjectSetupCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField _width = default;
    [SerializeField] private TMP_InputField _length = default;
    [SerializeField] private TMP_InputField _height = default;
    private Vector3 _current = new Vector3(1f, 1f, 1f);
    private void Awake()
    {
        _width.text = "1";
        _length.text = "1";
        _height.text = "1";
        
        _width.onValueChanged.AsObservable()
        .Subscribe(width =>
        {
            SetScale(ScaleElement.Width, width);
        }).AddTo(this);
        _length.onValueChanged.AsObservable()
        .Subscribe(length =>
        {
            SetScale(ScaleElement.Length, length);
        }).AddTo(this);
        _height.onValueChanged.AsObservable()
        .Subscribe(height =>
        {
            SetScale(ScaleElement.Height, height);
        }).AddTo(this);
    }

    private void SetScale(ScaleElement element, string value)
    {
        if (float.TryParse(value, out var val))
        {
            Vector3 vector3;
            switch (element)
            {
                case ScaleElement.Width:
                default:
                    vector3 = new Vector3(val, _current.y, _current.z);
                    break;
                case ScaleElement.Length:
                    vector3 = new Vector3(_current.x, _current.y, val);
                    break;
                case ScaleElement.Height:
                    vector3 = new Vector3(_current.x, val, _current.z);
                    break;
            }
            _current = vector3;
            MainSceneManager.Instance.SetARMarkerScale(vector3);
        }
        else
        {
            Debug.LogWarning("cannot set unnumeric value");
        }
    }

    enum ScaleElement
    {
        Width,
        Length,
        Height
    }
}
