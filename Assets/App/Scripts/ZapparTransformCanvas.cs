using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zappar;

public class ZapparTransformCanvas : MonoBehaviour
{
    [SerializeField] private ZapperTransformObserver _zapparObserver;
    [SerializeField] private TMP_Text _cameraText;
    [SerializeField] private TMP_Text _trackTargetText;
    private StringBuilder _sb = new StringBuilder();
    private void Start()
    {
        _zapparObserver.CameraTransformObservable
        .Pairwise()
        .Where(pair => CheckTransformChanged(pair.Current, pair.Previous))
        .Subscribe(transform =>
        {
            _cameraText.text = TransformToString(transform.Current);
        }).AddTo(this);

        _zapparObserver.TrackTargetTransformObservable
        .Pairwise()
        .Where(pair => CheckTransformChanged(pair.Current, pair.Previous))
        .Subscribe(transform =>
        {
            _trackTargetText.text = TransformToString(transform.Current);
        }).AddTo(this);
    }

    private bool CheckTransformChanged((Vector3 position, Quaternion rotation) current, (Vector3 position, Quaternion rotation) previours)
    {
        bool result = false;
        result |= current.position.x != previours.position.x || current.position.y != previours.position.y || current.position.z != previours.position.z;
        result |= current.rotation.x != previours.rotation.x || current.rotation.y != previours.rotation.y || current.rotation.z != previours.rotation.z || current.rotation.w != previours.rotation.w;
        return result;
    }

    private string TransformToString((Vector3 position, Quaternion rotation) transform)
    {
        _sb.Clear();
        _sb.Append("Pos:");
        _sb.Append(transform.position);
        _sb.Append("Rot:");
        _sb.Append(transform.rotation.eulerAngles);
        return _sb.ToString();
    }
}
