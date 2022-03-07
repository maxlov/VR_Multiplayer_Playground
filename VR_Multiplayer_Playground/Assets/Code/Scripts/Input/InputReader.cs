using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="InputReader", menuName ="Gameplay/Input Reader")]
public class InputReader : ScriptableObject, PlayerInput.IFireballActions
{
    // Assign delegate{} to events to skip null checks.

    public event UnityAction chargeEvent = delegate { };
    public event UnityAction chargeCancelledEvent  = delegate { };

    private PlayerInput _playerInput;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();

            _playerInput.Fireball.SetCallbacks(this);
            _playerInput.Fireball.Enable();
        }
    }

    private void OnDisable()
    {
        _playerInput.Fireball.Disable();
    }

    #region Fireball

    public void OnCharge(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                chargeEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                chargeCancelledEvent.Invoke();
                break;
        }
    }

    #endregion
}
