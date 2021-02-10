using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamPerspective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePerspective()
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographic = false;
            float horizontalFov = 2 * Mathf.Atan(Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2) * Camera.main.aspect) * Mathf.Rad2Deg;
            float cameraDistance = GetComponent<Renderer>().bounds.size.x / 2 / Mathf.Tan(horizontalFov / 2 * Mathf.Deg2Rad);
            float cameraHeight = cameraDistance * Mathf.Tan(Camera.main.fieldOfView / 2 * Mathf.Deg2Rad);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.x + cameraHeight * 1.6f, transform.position.z + (cameraDistance + GetComponent<Renderer>().bounds.size.z / 2) * -1f);
            Camera.main.transform.rotation = Quaternion.Euler(20, 0, 0);
        }
        else
        {
            Camera.main.orthographic = true;
            Camera.main.orthographicSize = 6;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.GetChild(0).transform.position.y, Camera.main.transform.position.z);

        }
    }
}
