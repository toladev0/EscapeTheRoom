using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    // Speed at which the skybox rotates (degrees per second)
    public float skySpeed = 1f;

    // Current rotation value of the skybox
    private float rotation = 0f;

    void Update()
    {
        // Increase the rotation based on time and speed
        rotation += Time.deltaTime * skySpeed;

        // Keep the rotation value between 0 and 360 to prevent overflow
        rotation %= 360f;

        // Apply the updated rotation to the skybox
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
        }
    }
}
