using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zappar;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private ZapperTransformObserver _transformObserver;
    [SerializeField] private BaseARMarkerObject _arMarkerObject = default;
    [SerializeField] private ARObjectSetupCanvas _arObjectSetupCanvas = null;
    private static MainSceneManager s_instance;
    public static MainSceneManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                Instance = FindObjectOfType<MainSceneManager>();
                if (s_instance == null)
                {
                    Debug.LogError($"{nameof(MainSceneManager)} not found in this scene");
                    return null;
                }
            }
            return s_instance;
        }
        private set
        {
            if (s_instance == null)
            {
                s_instance = value;
            }
            else
            {
                Debug.LogWarning("this class is singleton, so delete multiple instance");
                DestroyImmediate(value);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _transformObserver.TrackTargetTransformObservable
        .Pairwise()
        .Where(pair => CheckTransformChanged(pair.Current, pair.Previous))
        .Subscribe(transform =>
        {
            _arMarkerObject.SetPosition(transform.Current.position);
            _arMarkerObject.SetRotation(transform.Current.rotation);
        }).AddTo(this);
    }
    public void OnSceneEvent()
    {
        _arMarkerObject.OnSceneEvent();
    }
    public void OnNotSceneEvent()
    {
        _arMarkerObject.OnNotSeneEvet();
    }
    public void SetARMarkerScale(Vector3 scale)
    {
        _arMarkerObject.SetScale(scale);
    }
        private bool CheckTransformChanged((Vector3 position, Quaternion rotation) current, (Vector3 position, Quaternion rotation) previours)
    {
        bool result = false;
        result |= current.position.x != previours.position.x || current.position.y != previours.position.y || current.position.z != previours.position.z;
        result |= current.rotation.x != previours.rotation.x || current.rotation.y != previours.rotation.y || current.rotation.z != previours.rotation.z || current.rotation.w != previours.rotation.w;
        return result;
    }

}
