using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class PlayerControllerz : MonoBehaviour
{
    public float pullForce;
    public float maxSpeed;
    private Rigidbody2D rb;
    public GameObject lance;
    public bool tethered = false;
    public GameObject tetheredTo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(forwardSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (tethered)
        {
            renderTether();
            if (Input.anyKey)
            {
                // accellerate
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(pullForce * tetheredTo.transform.position);

                }
            }
            else
            {
                Debug.Log("try to untether");
                Destroy(gameObject.GetComponent<SpringJoint2D>());
                tethered = false;
            }

        }
        else
        {
            if (Input.anyKeyDown)
            {
                GameObject bullet = Instantiate(lance, transform.position + (transform.up * 1), Quaternion.identity);
            }

        }
    }

    public void renderTether()
    {
        if (gameObject.GetComponent<LineRenderer>() == null) // no component
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.1f;
        }
        LineRenderer gotLineRenderer = GetComponent<LineRenderer>();
        gotLineRenderer.SetPosition(0, gameObject.transform.position);
        gotLineRenderer.SetPosition(1, tetheredTo.transform.position);
    }

    public void setTethered(bool isTethered, GameObject tetheredTo)
    {
        this.tethered = isTethered;
        this.tetheredTo = tetheredTo;
    }
}
