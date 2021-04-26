using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Not working will try this again.
public class overlapTrigger : MonoBehaviour
{
    #region Attributes


    public Color myColor;
    public float rFloat = 0.1f;
    public float gFloat = 0.1f;
    public float bFloat = 0.1f;
    public float aFloat = 0.1f;
    //0 to 1
    public float rNonFloat = 0.9f;
    public float gNonFloat = 0.9f;
    public float bNonFloat = 0.9f;
    public float aNonFloat = 0.9f;
    //public Renderer rb;


    public Renderer myRenderer;

    private bool overlapped = false;

    #endregion


    // Start is called before the first frame update
    private void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        rFloat = 0.01f;
        gFloat = 1;
        bFloat = 1;
        aFloat = 0.01f;

        //rb = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (overlapped)
        {
            aFloat = 1;
            gFloat = 1;
            bFloat = 1;
            if (rFloat < 1)
            {
                rFloat += 0.01f;
            }
            else
            {
                rFloat = 0;
            }
            myColor = new Color(rFloat, gFloat, bFloat, aFloat);
            myRenderer.material.color = myColor;

        }
        else
        {
            aFloat = 0.01f;
            rFloat = 0;
            gFloat = 0;
            bFloat = 0;
            myColor = new Color(rFloat, gFloat, bFloat, aFloat);
            myRenderer.material.color = myColor;

        }




        //This code below changes the color of the material depending on the key pressed RGBA
        //Using this to change the trigger values.

        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (aFloat < 1)
        //    {
        //        aFloat += 0.01f;
        //    }
        //    else
        //    {
        //        aFloat = 0;
        //    }
        //}
        //if (Input.GetKey(KeyCode.R))
        //{
        //    if ( rFloat < 1)
        //    {
        //        rFloat += 0.01f;
        //    }
        //    else
        //    {
        //        rFloat = 0;
        //    }
        //}
        //if (Input.GetKey(KeyCode.G))
        //{
        //    if (gFloat < 1)
        //    {
        //        gFloat += 0.01f;
        //    }
        //    else
        //    {
        //        gFloat = 0;
        //    }
        //}
        //if (Input.GetKey(KeyCode.B))
        //{
        //    if (bFloat < 1)
        //    {
        //        bFloat += 0.01f;
        //    }
        //    else
        //    {
        //        bFloat = 0;
        //    }
        //}

        //myColor = new Color(rFloat, gFloat, bFloat, aFloat);

        //myRenderer.material.color = myColor;
    }


    //issue trigger can not collide with another trigger, only collider + trigger or collider + collider
    //May be reconciled with the empty trigger object to be seen. Will remove when I have tested.
    //object not triggering will need to investigate.
    private void onTriggerOverlap(Collider other)
    {
        if (other.tag == "Box")
        {
            isOverlapping();
        }
    }


    //it'll never not overlap so closing this for now for troubleshooting purposes
    //private void onTriggerNotOverlap(Collider other)
    //{
    //    if (other.tag == "Box")
    //    {
    //        notOverlapping();
    //    }
    //}


    private void notOverlapping()
    {
        overlapped = false;
    }
    private void isOverlapping()
    {
        overlapped = true;
    }
}
