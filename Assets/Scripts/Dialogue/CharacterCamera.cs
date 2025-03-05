using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField]private Transform camera;
    [SerializeField]private float sensivity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        
        transform.Rotate(Vector3.up * mouseX);
        

    }
}
