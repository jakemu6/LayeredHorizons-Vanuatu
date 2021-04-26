using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlapTrigger03 : MonoBehaviour
{

    public Color myColor;
    public float rFloat = 0.1f;
    public float rNonFloat = 0.9f;

    public Renderer myRenderer;


    bool m_Started;
    //Layer of which to detect objects set to everything if unsure
    public LayerMask m_LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        //used to draw Gizmos
        m_Started = false;

        MyCollisions();

    }

    private void FixedUpdate()
    {
    }

    void MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Hit: " + hitColliders[i].name + i);
            i++;
        }
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
