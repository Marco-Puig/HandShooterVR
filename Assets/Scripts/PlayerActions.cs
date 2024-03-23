using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Material handMaterial;

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
    }

    /// <summary>
    /// Moves the player in the direction of the left hand transform.
    /// </summary>
    public void MoveDirectionPoint()
    {
        // += Vector3(x, y, z);
        playerTransform.position += leftHandTransform.forward * 3.0f * Time.deltaTime;
    }

    /// <summary>
    /// Fires the action if the cooldown is not active.
    /// </summary>
    public void Firing()
    {
        if (!cooldown)
        {
            handMaterial.color = new Color(1f, 1f, 1f, 0f);
            count += 1f;
        }
    }

    public void StopFiring()
    {
        handMaterial.color = new Color(1f, 1f, 1f, 1f);
        count -= 1f;   
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
            handMaterial.color = Color.red;
            count -= 1f;
        }

        if (count <= 0)
        {
            count = 0;
            cooldown = false;
        }
    }
}
