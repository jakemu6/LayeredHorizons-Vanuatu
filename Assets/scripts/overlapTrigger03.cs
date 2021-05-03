using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlapTrigger03 : MonoBehaviour
{

    [Header("GO represents overlapping GOs")]
    public GameObject overlapGO;

    public Color myColor;
    public float rFloat = 0.1f;
    public float rNonFloat = 0.9f;

    public Renderer myRenderer;

    //bool for drawing the Gizmos
    bool m_Started;

    //variables for the overlapping bounding box
    float[] xPos = new float[4];
    float[] zPos = new float[4];

    //Layer of which to detect objects set to everything if unsure
    public LayerMask m_LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        m_LayerMask = LayerMask.GetMask("LocationCubes");

        //used to draw Gizmos
        m_Started = false;

        //Invoke("MyCollisions", 3.0f);

    }


    //void MyCollisions()
    //{
    //    Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
    //    int i = 0;
    //    while (i < hitColliders.Length)
    //    {
    //        Debug.Log("Hit: " + hitColliders[i].name + i);
    //        i++;
    //        myRenderer.material.color = myColor;
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        //print(collision.transform.tag);
        //myRenderer.material.color = myColor;

        if (collision.transform.tag == "collidable")
        {
            myRenderer.material.color = myColor;
        }


        // Make an empty list to hold contact points
        ContactPoint[] contacts = new ContactPoint[10];

        // Get the contact points for this collision
        int numContacts = collision.GetContacts(contacts);

        // Iterate through each contact point
        for (int i = 0; i < numContacts; i++)
        {
            //positions of the 4 contact points that make a bounding box.
            xPos[i] = contacts[i].point.x;
            zPos[i] = contacts[i].point.z;
        }

        //equation for centre of the rectangle to draw the position
        //the if statements are a hack to check that they're not the same value.
        float xCenter;
        //equation for size of the rectangle to draw scale
        float overlapWidth;

        if (xPos[0] == xPos[2])
        {
            xCenter = (xPos[0] + xPos[1]) / 2;
            overlapWidth = Mathf.Abs(xPos[0] - xPos[1]);
        }
        else
        {
            xCenter = (xPos[0] + xPos[2]) / 2;
            overlapWidth = Mathf.Abs(xPos[0] - xPos[2]);
        }

        float zCenter;
        float overlapDepth;

        if (zPos[0] == zPos[2])
        {
            zCenter = (zPos[0] + zPos[1]) / 2;
            overlapDepth = Mathf.Abs(zPos[0] - zPos[1]);

        }
        else
        {
            zCenter = (zPos[0] + zPos[2]) / 2;
            overlapDepth = Mathf.Abs(zPos[0] - zPos[2]);
        }





        //ContactPoint contact = collision.GetContact(0);
        //print(collision.contactCount);
        GameObject overlappingObject = Instantiate(overlapGO, new Vector3(xCenter, 2.0f, zCenter), Quaternion.Euler(0, 0, 0));
        overlappingObject.transform.localScale = new Vector3(overlapWidth, 0.1f, overlapDepth);


        //foreach (ContactPoint contact in collision.contacts)
        //{

        //    print(contact.point);
        //    GameObject overlappingObject = Instantiate(overlapGO, new Vector3(contact.point.x, 2.0f, contact.point.z), Quaternion.Euler(0, 0, 0));

        //}


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}
