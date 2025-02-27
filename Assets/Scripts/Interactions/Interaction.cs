using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)] private float interactDistance = 5f;
    [SerializeField] private float inspectionSensitivity = 3f; 
    private PlayerMovement playerMovement;
    private Camera _camera;
    private UIController _uiController;
    private Rigidbody _playerRb;
    private bool _isInspection;
    private bool _hasCurrent;
    
    private void Awake()
    {
        _camera = Camera.main;
        _uiController = GetComponent<UIController>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        _playerRb = playerMovement.gameObject.GetComponent<Rigidbody>();
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
        if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
        {
            InspectionObject(hit);
            _uiController.crossImage.sprite = _uiController.interactCross;
            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.magenta);
        }
        else
        {
            _uiController.crossImage.sprite = _uiController.defaultCross;
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.black);
        }
    }
    
    private void InspectionObject(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Inspection inspection))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isInspection = !_isInspection;
                playerMovement.enabled = !_isInspection;
                _playerRb.isKinematic = _isInspection;
                if (_isInspection)
                {
                    inspection.GetInspectionCoroutine();
                }
            }
            inspection.GetInspection(ref _isInspection,inspectionSensitivity);
        }
    }
}