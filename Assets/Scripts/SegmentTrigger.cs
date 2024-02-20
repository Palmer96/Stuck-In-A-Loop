using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentTrigger : MonoBehaviour
{
    private Segment m_parentSegment;

    // Start is called before the first frame update
    void Start()
    {
        m_parentSegment = GetComponentInParent<Segment>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (m_parentSegment != null)
            {
                if (m_parentSegment != SegmentManager.Instance.m_currentSegment)
                {
                    m_parentSegment.SegmentEntered();
                }
            }
            else
            {
                Debug.Log("no parent segment");
            }

        }
    }
}
