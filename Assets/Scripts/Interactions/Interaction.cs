using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;
    public float inspectionSensitivity = 3f;
    public PlayerMovement PlayerMovement { get; private set; }
    private Camera Camera { get; set; }
    public UIController UIController { get; private set; }
    public Inventory Inventory { get; private set; }
    public Rigidbody PlayerRb { get; private set; }
    public Inspection CurrentInspection { get; private set; }
    private IInteractionState _currentState;

    private void Awake()
    {
        Camera = Camera.main;
        UIController = GetComponent<UIController>();
        Inventory = GetComponent<Inventory>();
        PlayerMovement = FindAnyObjectByType<PlayerMovement>();
        PlayerRb = PlayerMovement.gameObject.GetComponent<Rigidbody>();
        ChangeState(new NormalState());
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void ChangeState(IInteractionState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void InteractionAnyObject()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Interaction");
        if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
        {
            if (hit.transform.TryGetComponent(out Inspection inspection))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    CurrentInspection = inspection;
                    switch (_currentState)
                    {
                        case InspectionState:
                            ChangeState(new NormalState());
                            break;
                        case NormalState:
                            ChangeState(new InspectionState());
                            break;
                    }
                }
            }

            if (hit.transform.TryGetComponent(out PickUpAble pickUpAble))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    pickUpAble.Pickup(this);
                    ChangeState(new NormalState());
                }
            }

            UIController.crossImage.sprite = UIController.interactCross;
            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.magenta);
        }
        else
        {
            UIController.crossImage.sprite = UIController.defaultCross;
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.black);
        }
    }
    
    public void GetInteractionStatus(bool status)
    {
        UIController.crossImage.enabled = !status;
        PlayerMovement.enabled = !status;
        PlayerRb.isKinematic = status;
    }
}