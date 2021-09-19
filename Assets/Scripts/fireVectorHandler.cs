using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireVectorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void renderVector(Vector3 start, Vector3 end)
    {

        if (gameObject.GetComponent<LineRenderer>() == null) // no component
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        }
        LineRenderer gotLineRenderer = GetComponent<LineRenderer>();
        gotLineRenderer.widthMultiplier = 0.2f;
        gotLineRenderer.SetPosition(0, start);
        gotLineRenderer.SetPosition(1, start + end * 3);

    }
}
