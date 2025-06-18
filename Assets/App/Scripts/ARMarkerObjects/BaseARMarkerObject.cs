using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseARMarkerObject  : MonoBehaviour, IARMarkerObject
{
    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public virtual void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
    public virtual bool GetActiveSelf()
    {
        return gameObject.activeSelf;
    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public virtual void OnSceneEvent()
    {
        SetActive(true);
    }
    public virtual void OnNotSeneEvet()
    {
        SetActive(false);
    }
}

