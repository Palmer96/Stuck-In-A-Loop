using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public enum Direction
    {
        None,
        North,
        South,
        East,
        West
    }

    [SerializeField]
    private BoxCollider m_segmentTrigger;
    

    public GameObject m_myPrefab;

    public Direction m_direction;
    public Segment m_northSegment;
    public Segment m_southSegment;
    public Segment m_eastSegment;
    public Segment m_westSegment;
    

    // Start is called before the first frame update
    void Start()
    {
        m_myPrefab = SegmentManager.Instance.m_segmentPrefab; 
    }

    public void SegmentEntered()
    {
        SegmentManager.Instance.m_currentSegment.DeleteSegments(m_direction);
        switch (m_direction)
        {
            case Direction.North:
                GenerateSegments(Direction.South);
                m_southSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.South;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - South";
                break;
            case Direction.South:
                GenerateSegments(Direction.North);
                m_northSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.North;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - North";
                break;
            case Direction.East:
                GenerateSegments(Direction.West);
                m_westSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.West;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - West";
                break;
            case Direction.West:
                GenerateSegments(Direction.East);
                m_eastSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.East;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - East";
                break;
        }

        SegmentManager.Instance.m_currentSegment = this;
        SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - Current";
    }

    public void DeleteSegments(Direction excludeDirection)
    {
        if (excludeDirection != Direction.North && m_northSegment != null)
        {
            Destroy(m_northSegment.gameObject);
        }

        if (excludeDirection != Direction.South && m_southSegment != null)
        {
            Destroy(m_southSegment.gameObject);
        }

        if (excludeDirection != Direction.East && m_eastSegment != null)
        {
            Destroy(m_eastSegment.gameObject);
        }

        if (excludeDirection != Direction.West && m_westSegment != null)
        {
            Destroy(m_westSegment.gameObject);
        }

        m_direction = Direction.None;
    }

    public void GenerateSegments(Direction excludeDirection)
    {
        if (excludeDirection != Direction.North)
        {
            m_northSegment = Instantiate(m_myPrefab, transform.position + new Vector3(0, 0, SegmentManager.Instance.GetOffset()), Quaternion.identity).GetComponent<Segment>();
           //GameObject obj = Instantiate(m_myPrefab, transform.position + new Vector3(0, 0, 100), Quaternion.identity);
           //m_northSegment = obj.GetComponent<Segment>();


            m_northSegment.m_direction = Direction.North;
            m_northSegment.gameObject.name = "Segment - North";
        }

        if (excludeDirection != Direction.South)
        {
            m_southSegment = Instantiate(m_myPrefab, transform.position + new Vector3(0, 0, -SegmentManager.Instance.GetOffset()), Quaternion.identity).GetComponent<Segment>();
            m_southSegment.m_direction = Direction.South;
            m_southSegment.gameObject.name = "Segment - South";
        }

        if (excludeDirection != Direction.East)
        {
            m_eastSegment = Instantiate(m_myPrefab, transform.position + new Vector3(SegmentManager.Instance.GetOffset(), 0, 0), Quaternion.identity).GetComponent<Segment>();
            m_eastSegment.m_direction = Direction.East;
            m_eastSegment.gameObject.name = "Segment - East";
        }

        if (excludeDirection != Direction.West)
        {
            m_westSegment = Instantiate(m_myPrefab, transform.position + new Vector3(-SegmentManager.Instance.GetOffset(), 0, 0), Quaternion.identity).GetComponent<Segment>();
            m_westSegment.m_direction = Direction.West;
            m_westSegment.gameObject.name = "Segment - West";
        }

        m_direction = Direction.None;
    }
}
