using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    bool phone;
    public Transform player;
    private Vector3 fireVector;
    public float rotationSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        phone = Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        setFireVector();
    }

    public bool shouldAccelerate()
    {
        if (phone)
        {
            // touch is down on second touch
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase != TouchPhase.Began)
                {
                    return true;
                }
            }
            return false;
        } else
        {
            return Input.anyKey;
        }
    }

    public bool shouldFireTether()
    {
        if (phone)
        {
            // on second touch
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    return true;
                }
            }
            return false;

        } else
        {
            return Input.anyKeyDown;
        }

    }

    private Vector2 rotate(Vector2 v, float rad)
    {
        return new Vector3(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad),
            0
        );
    }

    private void setFireVector()
    {
        Vector3 current = player.GetComponent<PlayerController>().fireVector;
        if (phone)
        {
            // first touch slide
            if (Input.touchCount > 0)
            {
                if(Input.GetTouch(0).deltaPosition.x > 0.1)
                {
                    // rotate right
                    fireVector = rotate(current, rotationSpeed * Time.deltaTime);
                }
                if (Input.GetTouch(0).deltaPosition.x < -0.1)
                {
                    // rotate left
                    fireVector = rotate(current, -rotationSpeed * Time.deltaTime);
                }
            }
            fireVector = current;
        } else
        {
            Vector3 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position);
            mouseDir.z = 0;
            mouseDir.Normalize();
            fireVector = mouseDir;
        }
    }

    public Vector3 getFireVector()
    {
        return fireVector;
    }

    public bool shouldReset()
    {
        if (phone)
        {
            return false;
        } else
        {
            return false;
        }

    }


}