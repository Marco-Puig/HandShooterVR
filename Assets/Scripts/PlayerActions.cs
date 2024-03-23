using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerActions : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform playerTransform;
    [Header("Left Hand")]
    [SerializeField] Transform leftHandTransform;
    [Header("Right Hand")]
    [SerializeField] GameObject modelShow;
    [Header("Both Hands")]
    [SerializeField] Material handMaterial;

    // Current HandStates
    private delegate void HandState();
    private HandState rightHandState;
    private HandState leftHandState;

    // Set States in Event Handler
    private string _leftHandStateName;
    private string _rightHandStateName;
    public string rightHandStateName
    {
        get => _rightHandStateName;
        set
        {
            _rightHandStateName = value;
            HandleStates();
        }
    }
    public string leftHandStateName
    {
        get => _leftHandStateName;
        set
        {
            _leftHandStateName = value;
            HandleStates();
        }
    }

    // Cooldown
    private float count = 0f;
    private bool cooldown = false;
    private const float maxCount = 300f;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        // if Photonview isnt owned, then dont allow actions
        // if (!photonView.IsMine) return;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        HandleCooldown();
        rightHandState?.Invoke();
        leftHandState?.Invoke();
    }

    /// <summary>
    /// Moves the player in the direction of the left hand transform.
    /// </summary>
    void MoveDirectionPoint()
    {
        // += Vector3(x, y, z);
        playerTransform.position += leftHandTransform.forward * 3.0f * Time.deltaTime;
    }

    /// <summary>
    /// Fires the action if the cooldown is not active.
    /// </summary>
    private void Firing()
    {
        if (!cooldown)
        {
            handMaterial.color = new Color(1f, 1f, 1f, 0f);
            count += 1f;
            modelShow.SetActive(true);
        }
        else
        {
            handMaterial.color = Color.red;
            modelShow.SetActive(false);
        }
    }

    private void StopFiring()
    {
        handMaterial.color = new Color(1f, 1f, 1f, 1f);
        count -= 1f;
        modelShow.SetActive(false);
    }

    /// <summary>
    /// Handles the cooldown logic.
    /// </summary>
    private void HandleCooldown()
    {
        if (count >= maxCount)
        {
            cooldown = true;
        }

        if (count != 0f && cooldown)
        {
            count -= 1f;
        }

        if (count <= 0)
        {
            count = 0;
            cooldown = false;
        }
    }

    private void HandleStates()
    {
        switch (rightHandStateName)
        {
            case "Firing":
                rightHandState = Firing;
                break;
            case "StopFiring":
                rightHandState = StopFiring;
                break;
            default:
                rightHandState = null;
                break;
        }

        switch (leftHandStateName)
        {
            case "MoveDirectionPoint":
                leftHandState = MoveDirectionPoint;
                break;
            default:
                leftHandState = null;
                break;
        }
    }
}

