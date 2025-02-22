using UnityEngine;
public class Interaction : MonoBehaviour
{
    private Camera _camera;
    private UIController _uiController;
    private void Awake()
    {
        _camera = Camera.main;
        _uiController = GetComponent<UIController>();
    }

    void Update()
    {
        InteractionAnyObject();
    }
    

    private void InteractionAnyObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Interaction");
        if (Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask))
        {
            _uiController.CrossImage.sprite = _uiController.interactCross;
            Debug.DrawRay(ray.origin,hit.point - ray.origin,Color.magenta);
        }
        else
        {
            _uiController.CrossImage.sprite = _uiController.defaultCross;
            Debug.DrawRay(ray.origin,ray.direction*1000,Color.black);
        }
    }
}