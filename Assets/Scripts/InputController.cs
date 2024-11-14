using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void StateChanged(string state);
    public static event StateChanged OnStateChanged;
    
    private CharacterController charController;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get input values
        int vertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);

        charController.ForwardInput = vertical;
        charController.StrafeInput = horizontal;
        charController.JumpInput = jump;

        string state = string.Format("{0} {1} {2} {3} {4}", vertical != 0 ? "Walking" : "", horizontal != 0 ? "Strafing" : "", jump ? "Jumping" : "", Input.GetMouseButtonDown(0) ? "Shooting" : "", Input.GetMouseButtonDown(0) ? "BIG GUN SHOT" : "");

        OnStateChanged?.Invoke(state);
    }
}
