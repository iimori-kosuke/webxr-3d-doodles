using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ZapperTransformObserver : MonoBehaviour
{
    [SerializeField] private Transform _zapparCamera = default;
    [SerializeField] private Transform _zapparTrackTarget = default;
    private Subject<(Vector3 position, Quaternion rotation)> _cameraTransformObservable = new Subject<(Vector3, Quaternion)>();
    public IObservable<(Vector3 position, Quaternion rotation)> CameraTransformObservable => _cameraTransformObservable;

    private Subject<(Vector3 position, Quaternion rotation)> _trackTargetTransformObservable = new Subject<(Vector3, Quaternion)>();
    public IObservable<(Vector3 position, Quaternion rotation)> TrackTargetTransformObservable => _trackTargetTransformObservable;
    private void Update()
    {
        _cameraTransformObservable?.OnNext((_zapparCamera.transform.position, _zapparCamera.transform.rotation));
        _trackTargetTransformObservable?.OnNext((_zapparTrackTarget.transform.position, _zapparTrackTarget.transform.rotation));
    }
}
