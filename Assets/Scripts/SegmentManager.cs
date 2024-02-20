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
    public int segmentOffset { get { return m_segmentOffset; } }

    public delegate void SegmentEvent();
    public SegmentEvent segmentGenerated;


    // Start is called before the first frame update
    void Start()
    {
       m_currentSegment = Instantiate(m_segmentPrefab, Vector3.zero, Quaternion.identity).GetComponent<Segment>();
       m_currentSegment.GenerateAdjacentSegments(Segment.Direction.None);
    }

    public int GetOffset()
    {
        return m_segmentOffset;
    }

    public void SegmentGenerated()
    {
        Debug.Log("Triggered");
        segmentGenerated?.Invoke();
    }

}
