using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : SingletonBase<SegmentManager>
{
    [SerializeField]
    public GameObject m_segmentPrefab;
    public Segment m_currentSegment;
    [SerializeField]
    private int m_segmentOffset = 100;


    // Start is called before the first frame update
    void Start()
    {
       m_currentSegment = Instantiate(m_segmentPrefab, Vector3.zero, Quaternion.identity).GetComponent<Segment>();

       m_currentSegment.GenerateSegments(Segment.Direction.None);

       // m_northSegment = Instantiate(m_myPrefab, transform.position + new Vector3(0, 0, 100), Quaternion.identity).GetComponent<Segment>();
       // m_northSegment.m_direction = Direction.North;
       // 
       // m_southSegment = Instantiate(m_myPrefab, transform.position + new Vector3(0, 0, -100), Quaternion.identity).GetComponent<Segment>();
       // m_southSegment.m_direction = Direction.South;
       // 
       // m_eastSegment = Instantiate(m_myPrefab, transform.position + new Vector3(100, 0, 0), Quaternion.identity).GetComponent<Segment>();
       // m_eastSegment.m_direction = Direction.East;
       // 
       // m_westSegment = Instantiate(m_myPrefab, transform.position + new Vector3(-100, 0, 0), Quaternion.identity).GetComponent<Segment>();
       // m_westSegment.m_direction = Direction.West;

    }

    public int GetOffset()
    {
        return m_segmentOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
