using UnityEngine;


public class VRGestureSystem : MonoBehaviour
{
    public GameObject visualSysRight;
    public GameObject visualSysLeft;
    public Rigidbody player;
    public GameObject leftHand;
    public GameObject modelShow;
    public Material handMaterial;

    private float count = 0f;
    private bool cooldown = false;
    private const float maxCount = 300f;

    private void Update()
    {
        HandleRightHand();
        HandleLeftHand();
        HandleCooldown();
    }

    private void HandleRightHand()
    {
        if (!visualSysRight.activeSelf)
        {
            if (count < maxCount && !cooldown)
            {
                modelShow.SetActive(true);
                handMaterial.color = new Color(1f, 1f, 1f, 0f);
                count += 1f;
            }
            else
            {
                modelShow.SetActive(false);
            }
        }
        else
        {
            modelShow.SetActive(false);
            count -= 1f;
            if (!cooldown)
            {
                handMaterial.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    private void HandleLeftHand()
    {
        if (!visualSysLeft.activeSelf)
        {
            player.transform.position += leftHand.transform.forward * 3.0f * Time.deltaTime;
        }
    }

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

