using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float shakes = 0.0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 3.0f;

    private bool CameraShaking;
    private Vector3 offset;
    private Vector3 offset2;

    void Start()
    {
        CameraShaking = false;
        offset = transform.position - player.transform.position;
    }
    void FixedUpdate()
    {
        if (CameraShaking)
        {
            if (shakes > 0)
            {
                offset2 = Random.insideUnitSphere * shakeAmount;
                shakes -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakes = 0f;
                offset2 = new Vector3(0, 0, 0);
                CameraShaking = false;
            }
        }
    }
    void LateUpdate()
    {       
        transform.position = player.transform.position + offset + offset2;
    }
    public void ShakeCamera(float shaking)
    {
        shakes = shaking;
        CameraShaking = true;
    }
}