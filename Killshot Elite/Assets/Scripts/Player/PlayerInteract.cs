using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float distance = 3f;
    [SerializeField] LayerMask mask;

    private PlayerUI _playerUI;
    private Camera _camera;
    private InputManager _inputManager;
    private void Start()
    {
        _camera = GetComponent<PlayerLook>().cam;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }
    private void Update()
    {
        _playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(interactable.promptMessage);
                if (_inputManager.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        };
    }
}
