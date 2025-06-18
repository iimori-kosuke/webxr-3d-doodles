using UnityEngine;
public interface IARMarkerObject
{
    void SetScale(Vector3 scale);
    void SetPosition(Vector3 position);
    void SetRotation(Quaternion rotation);
    bool GetActiveSelf();
    void SetActive(bool active);
    void OnSceneEvent();
    void OnNotSeneEvet();
}
