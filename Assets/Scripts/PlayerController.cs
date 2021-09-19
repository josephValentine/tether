using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float pullForce;
    public float maxSpeed;
    public float drag;
    private Rigidbody2D rb;
    public GameObject lance;
    public bool tethered = false;
    public GameObject tetheredTo;
    private bool accelerate;
    private bool letGo;
    private bool fireTether;
    private bool changeFireAngle;
    private InputHandler inputHandler;
    public Vector3 fireVector;
    private fireVectorHandler fireVectorHandler;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Lanceable"));
        inputHandler = gameObject.GetComponent<InputHandler>();
        fireVectorHandler = GameObject.Find("fireVector").GetComponent<fireVectorHandler>();
        fireVector = new Vector3();

        //rb.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        //if (
        //    //gameObject.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.NameToLayer("Lanceable"))
        //            Physics2D.IsTouchingLayers(gameObject.GetComponent<BoxCollider2D>(), LayerMask.NameToLayer("Lanceable"))

        //    )
        //{
        //    Debug.Log("touching layer");

        //}
        renderFireVector();
        renderTether();
        //rb.AddForce(forwardSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (inputHandler.shouldReset())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        if (tethered)
        {
            
            if (inputHandler.shouldAccelerate())
            {
                // accellerate
                gameObject.GetComponent<SpringJoint2D>().distance -= pullForce;

                // apply drag
                float dragForce = rb.velocity.magnitude * drag;
                rb.AddForce(-dragForce * gameObject.transform.forward);
            }
            else
            {
                Destroy(gameObject.GetComponent<SpringJoint2D>());
                tethered = false;
            }

        }
        else
        {
            if (inputHandler.shouldFireTether())
            {
                GameObject bullet = Instantiate(lance, transform.position + (transform.up * 1), Quaternion.identity);
            }

        }
    }

    public void renderFireVector()
    {
        fireVector = inputHandler.getFireVector();
        fireVectorHandler.renderVector(transform.position, fireVector);
    }

    public void renderTether()
    {
        if (tethered)
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
        else
        {
            if (gameObject.GetComponent<LineRenderer>() != null) // no component
            {
                Destroy(gameObject.GetComponent<LineRenderer>());
            }

        }

    }

    public void setTethered(bool isTethered, GameObject tetheredTo)
    {
        this.tethered = isTethered;
        this.tetheredTo = tetheredTo;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log("hit");
    //    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Lanceable"))
    //    {
    //        Debug.Log("hit layer");

    //        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Lanceable"));
    //    }
    //    else
    //    {
    //        Debug.Log(collision.collider.gameObject.layer);
    //        Debug.Log(LayerMask.NameToLayer("Lanceable"));


    //    }
    //}

    public bool isTouchingObjectTetheredTo()
    {
        if (tetheredTo)
        {
            float x = tetheredTo.transform.position.x;
            float y = tetheredTo.transform.position.y;
            //float width = tetheredTo.GetComponent<BoxCollider2D>().size.x;
            //float height = tetheredTo.GetComponent<BoxCollider2D>().size.y;
            var p1 = tetheredTo.transform.TransformPoint(0, 0, 0);
            var p2 = tetheredTo.transform.TransformPoint(1, 1, 0);
            var w = p2.x - p1.x;
            var h = p2.y - p1.y;
            float width = w;
            float height = h;
            Debug.Log(transform.position);
            Debug.Log("object, x +, y+, x-, y-");
            Debug.Log((x + width / 2));
            Debug.Log((y + width / 2));
            Debug.Log((x - width / 2));
            Debug.Log((y - width / 2));
            if (
                transform.position.x < (x + width / 2) &&
                transform.position.y < (y + height / 2) &&
                transform.position.x > (x - width / 2) &&
                transform.position.y > (y - height / 2)
                )
            {
                

                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
        
    }

    //void ControlsHandler()
    //{
    //    if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
    //    {
    //        HandleTouch();
    //    }
    //    else
    //    {
    //        HandleComputer();
    //    }
    //}

    //void HandleComputer()
    //{
    //    if (tethered)
    //    {

    //        if (ComputerInput.shouldAccelerate())
    //        {
    //            accelerate = true;
    //        }
    //        else
    //        {
    //            letGo = true;
    //        }

    //    }
    //    else
    //    {
    //        if (Input.anyKeyDown)
    //        {
    //            fireTether = true;
    //        }

    //    }
    //}

    //void HandleTouch()
    //{

    //}

}
