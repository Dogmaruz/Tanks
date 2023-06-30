using UnityEngine;

public class FP_CameraController : MonoBehaviour
{
    [Header("Scroll")]
    [SerializeField] private float m_mouseWheelSpeed;

    [SerializeField] private float m_minScroll;

    [SerializeField] private float m_maxScroll;

    [Header("Other settings")]
    [SerializeField] private float m_lerpRate;

    [SerializeField] private float m_rotationSensitivity; // Чувствидельность мышки.

    private Transform _cameraFollowPoint;

    private Transform _pivotCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateWithInput(float scrollInput)
    {
        if (_pivotCamera == null) return;

        transform.position = Vector3.Lerp(transform.position, _pivotCamera.position, Time.deltaTime * m_lerpRate);

        transform.LookAt(_cameraFollowPoint);

        CameraScroll(scrollInput);
    }

    public void UpdateRotate(Transform characterTransform, float mouseX)
    {
        characterTransform.Rotate(0f, mouseX * m_rotationSensitivity, 0f);
    }

    //Приближение или отдаление камкры в диапазоне.
    public void CameraScroll(float scroll)
    {
        _pivotCamera.position += _pivotCamera.forward * scroll * m_mouseWheelSpeed;

        Vector3 pivotCameraXY = new Vector3(_pivotCamera.transform.position.x, 0, _pivotCamera.transform.position.z);

        Vector3 targetLookAtXY = new Vector3(_cameraFollowPoint.transform.position.x, 0, _cameraFollowPoint.transform.position.z);

        float distance = Vector3.Distance(pivotCameraXY, targetLookAtXY);

        if (distance < m_minScroll || distance > m_maxScroll)
        {
            _pivotCamera.position -= _pivotCamera.forward * scroll * m_mouseWheelSpeed;
        }
    }

    public void SetFollowAndPivotTransform(Transform cameraFollowPoint, Transform pivotCamera)
    {
        _cameraFollowPoint = cameraFollowPoint;

        _pivotCamera = pivotCamera;
    }
}
