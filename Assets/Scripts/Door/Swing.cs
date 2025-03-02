using System.Collections;
using UnityEngine;

public class Swing : MonoBehaviour, IDoor
{
    [SerializeField] private float newPosition = 2f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool right = true;
    [SerializeField] private bool left;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.GetChild(0).GetChild(0).position;
    }

    public void Open()
    {
        StopAllCoroutines();
        if (right)
        {
            StartCoroutine(LerpController(transform.GetChild(0).position,
                new Vector3(transform.position.x, transform.position.y, transform.position.z + newPosition), duration));
        }
        else if (left)
        {
            StartCoroutine(LerpController(transform.GetChild(0).position,
                new Vector3(transform.position.x, transform.position.y, transform.position.z - newPosition), duration));
        }
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(LerpController(transform.GetChild(0).position, defaultPosition, duration));
    }


    private IEnumerator LerpController(Vector3 from,
        Vector3 to, float coroutineDuration)
    {
        float elapsed = 0f;
        while (elapsed < coroutineDuration)
        {
            transform.GetChild(0).position = Vector3.Lerp(from, to, elapsed / coroutineDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.GetChild(0).position = to;
    }
}