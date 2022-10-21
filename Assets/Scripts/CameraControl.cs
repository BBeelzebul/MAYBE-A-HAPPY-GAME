using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector2 mouseMovement;
    Vector2 smoothnessV;

    public float sensibility;
    public float smoothness;

    GameObject player;
    
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensibility * smoothness, sensibility * smoothness));

        smoothnessV.x = Mathf.Lerp(smoothnessV.x, md.x, 1f / smoothness);
        smoothnessV.y = Mathf.Lerp(smoothnessV.y, md.y, 1f / smoothness);

        mouseMovement += smoothnessV;
        mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(-mouseMovement.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseMovement.x, player.transform.up);

    }
}
