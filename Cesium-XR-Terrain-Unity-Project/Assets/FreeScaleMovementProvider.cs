using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FreeScaleMovementProvider : LocomotionProvider
{
    // Controllers for input
    public List<XRController> controllers = null;
    private CharacterController characterController = null;
    // Head aka being our head cam
    private GameObject head = null;
    
    // adjustable speed made public so you can adjust however you want
    public float speed = 1;
    public float gravityMultiplier = 1;
    // Start is called before the first frame update
    protected override void Awake()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }
    private void Start()
    {
        PositionController();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PositionController();
        CheckInput();
        ApplyGravity();
    }

    private void PositionController()
    {
        // Get head height
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        //Get Center
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        // Move capsule in local space
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        characterController.center = newCenter;
    }

    private void CheckInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForMovement(controller.inputDevice);
        }
    }

    private void CheckForMovement(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            StartMove(position);
    }

    private void StartMove(Vector2 position)
    {
        // Get movement
        Vector3 direction = new Vector3(position.x, 0f, position.y);
        Vector3 headRotation = new Vector3(0f, head.transform.eulerAngles.y, 0);

        // Rotate Head
        direction = Quaternion.Euler(headRotation) * direction;

        // Apply Movement
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
        // gravity.y *= Time.deltaTime;

        characterController.Move(gravity * Time.deltaTime);
    }
}
