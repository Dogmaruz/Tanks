using DG.Tweening;
using UnityEngine;

public class ShakeCamera : SingletonBase<ShakeCamera>
{
    private Camera _camera; 

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void Shake()
    {
        _camera.transform.DOShakePosition(1f);
    }
}
