using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInput _playerInput;
    private InputActionAsset _actionAsset;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        _actionAsset = _playerInput.actions;
    }

    public InputAction GetAction(string actionName)
    {
        return _actionAsset?.FindAction(actionName, true);
    }

}
