using System.Collections;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)] private float lerpDuration;
    [SerializeField, Range(3f, 5f)] private float swingPosition;
    private bool _isCoroutineRunning;
    private Transform _childTransform;
    private SwingDoorBooleanController sdbc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OpenableSwingDoor"))
        {
            Debug.LogWarning("Trigger Swing Enter");
            sdbc = other.gameObject.GetComponent<SwingDoorBooleanController>();
            _isCoroutineRunning = false;
            if (_isCoroutineRunning) return;
            _childTransform = other.transform.Find("SwingDoorChild");
            if (_childTransform == null)
                Debug.LogError("Child Transform not Able");
            else
            {
                if (sdbc != null)
                {
                    if (sdbc.left)
                        StartCoroutine(LerpController(_childTransform.position, null, -swingPosition,
                            _childTransform.transform));
                    else if (sdbc.right)
                        StartCoroutine(LerpController(_childTransform.position, null, swingPosition,
                            _childTransform.transform));
                    else
                        Debug.LogError("Please Select Any Boolean!");
                }
                else
                    Debug.LogError("Boolean Controller null");

                _isCoroutineRunning = true;
            }
        }
        else if (other.gameObject.CompareTag("OpenableHingedDoor"))
        {
            Debug.LogWarning("Trigger Hinged Enter");
            _isCoroutineRunning = false;
            if (_isCoroutineRunning) return;
            _childTransform = other.transform.Find("HingedDoorChild");
            if (_childTransform == null)
                Debug.LogError("Child Transform not Able");
            else
            {
                float d = transform.position.x - _childTransform.position.x;
                if (d >= 0)
                    StartCoroutine(LerpController(null, _childTransform.rotation, 90,
                        _childTransform.transform));
                else
                    StartCoroutine(LerpController(null, _childTransform.rotation, -90,
                        _childTransform.transform));
                _isCoroutineRunning = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OpenableSwingDoor"))
        {
            Debug.LogWarning("Trigger Swing Exit");
            _isCoroutineRunning = false;
            if (_isCoroutineRunning) return;
            if (_childTransform == null) return;
            if (sdbc != null)
            {
                if (sdbc.left)
                    StartCoroutine(LerpController(_childTransform.position, null, swingPosition,
                        _childTransform.transform));
                else if (sdbc.right)
                    StartCoroutine(LerpController(_childTransform.position, null, -swingPosition,
                        _childTransform.transform));
                else
                    Debug.LogError("Please Select Any Boolean!");
            }
            else
                Debug.LogError("Boolean Controller null");
            _isCoroutineRunning = true;
            sdbc = null;
        }
        else if (other.gameObject.CompareTag("OpenableHingedDoor"))
        {
            Debug.LogWarning("Trigger Hinged Exit");
            _isCoroutineRunning = false;
            if (_isCoroutineRunning) return;
            if (_childTransform == null) return;
            StartCoroutine(LerpController(null, _childTransform.rotation, 0,
                _childTransform.transform));
            _isCoroutineRunning = true;
        }
    }


    private IEnumerator LerpController(Vector3? swingDoor, Quaternion? hingedDoor,
        float amount, Transform lerpObject)
    {
        Debug.LogWarning("Coroutine Running");
        float duration = lerpDuration;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            if (swingDoor.HasValue)
            {
                Vector3 dp = new Vector3(swingDoor.Value.x, swingDoor.Value.y, swingDoor.Value.z + amount);
                lerpObject.position = Vector3.Lerp(lerpObject.position, dp, t);
                Debug.LogWarning("Swing");
            }

            if (hingedDoor.HasValue)
            {
                Quaternion dr = Quaternion.Euler(hingedDoor.Value.x, hingedDoor.Value.y + amount, hingedDoor.Value.z);
                lerpObject.rotation = Quaternion.Lerp(lerpObject.rotation, dr, t);
                Debug.LogWarning("Hinged");
            }

            yield return null;
        }
    }
}