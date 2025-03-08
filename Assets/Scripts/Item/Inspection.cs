using System.Collections;
using UnityEngine;

public class Inspection : MonoBehaviour
{
    private Camera _camera;
    private Quaternion currentRotation;
    private Vector3 currentPosition;
    
    private void Awake()
    {
        _camera = Camera.main;
        currentRotation = transform.rotation;
        currentPosition = transform.position;
    }

    public void EnterInspection()
    {
        if (this ==null) return;
        StopAllCoroutines();
        StartCoroutine(StartInspection());
    }
    
    public void UpdateInspection(float inspectionSensitivity)
    {
        if (this ==null) return;
        float mouseX = Input.GetAxis("Mouse X")*inspectionSensitivity;
        float mouseY = Input.GetAxis("Mouse Y")*inspectionSensitivity;
        Quaternion currentRotationT = transform.rotation;
            
        Quaternion horizontalRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion verticalRotation = Quaternion.AngleAxis(-mouseY, transform.right);
            
        transform.rotation = horizontalRotation * currentRotationT * verticalRotation;
    }

    public void ExitInspection()
    {
        if (this ==null) return;
        StopAllCoroutines();
        StartCoroutine(StartCurrent());
    }
    private IEnumerator StartInspection()
    {
        Vector3 newPosition = _camera.transform.position + _camera.transform.forward;
        Vector3 lookRotation = _camera.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(lookRotation,Vector3.up);
        float elapsedTime = 0;
        float duration = 1f;
        while (elapsedTime<duration)
        {
            elapsedTime +=Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(transform.position,newPosition,t);
            transform.rotation = Quaternion.Lerp(transform.rotation,newRotation,t);
            yield return null;
        }
    }

    private IEnumerator StartCurrent()
    {
        float elapsedTime = 0;
        float duration = 1f;
        while (elapsedTime<duration)
        {
            elapsedTime +=Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(transform.position,currentPosition,t);
            transform.rotation = Quaternion.Lerp(transform.rotation,currentRotation,t);
            yield return null;
        }
    }
    
    
}