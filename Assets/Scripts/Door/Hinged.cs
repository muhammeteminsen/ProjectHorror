using System.Collections;
using UnityEngine;

public class Hinged : MonoBehaviour, IDoor
{
    [SerializeField] private float angleRotate = 90f;
    [SerializeField] private float duration=1f;
    private Quaternion defaultRotation;
    private Transform _player;

    private void Awake()
    {
        defaultRotation = transform.GetChild(0).transform.GetChild(0).rotation;
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void Open()
    {
        
        float distance = _player.transform.transform.position.x - transform.position.x;
        StopAllCoroutines();
        StartCoroutine(distance <= 0
            ? LerpController(transform.GetChild(0).rotation, Quaternion.Euler(0, -angleRotate, 0), duration)
            : LerpController(transform.GetChild(0).rotation, Quaternion.Euler(0, angleRotate, 0), duration));
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(LerpController(transform.GetChild(0).rotation, defaultRotation, duration));
    }

    private IEnumerator LerpController(Quaternion from,
        Quaternion to,float coroutineDuration)
    {
        float elapsed = 0f;
        while (elapsed < coroutineDuration)
        {
            transform.GetChild(0).rotation = Quaternion.Lerp(from, to, elapsed/coroutineDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.GetChild(0).rotation = to;
    }
}