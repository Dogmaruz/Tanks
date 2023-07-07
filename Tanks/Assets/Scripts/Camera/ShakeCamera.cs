using DG.Tweening;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Camera _camera;

    private Tween _tween;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void Shake()
    {
        _camera.DOKill();

        _tween = _camera.transform.DOShakePosition(1f);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
