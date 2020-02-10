using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Camera1 : MonoBehaviour
{
    private Transform lookat;
    private Vector3 startOffset;
    // Start is called before the first frame update
    void Start()
    {
        lookat = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookat.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, lookat.position.z + startOffset.z);
        transform.position = lookat.position + startOffset;

    }
    public void Shake()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
    }
}
