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
    [SerializeField]
    private Transform m_spawnPoints;

    public GameObject m_myPrefab;

    public Direction m_direction;
    public Segment m_northSegment;
    public Segment m_southSegment;
    public Segment m_eastSegment;
    public Segment m_westSegment;

    public Item itemSpawned;


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
                GenerateAdjacentSegments(Direction.South);
                m_southSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.South;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - South";
                break;
            case Direction.South:
                GenerateAdjacentSegments(Direction.North);
                m_northSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.North;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - North";
                break;
            case Direction.East:
                GenerateAdjacentSegments(Direction.West);
                m_westSegment = SegmentManager.Instance.m_currentSegment;
                SegmentManager.Instance.m_currentSegment.m_direction = Direction.West;
                SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - West";
                break;
            case Direction.West:
                GenerateAdjacentSegments(Direction.East);
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
            if (m_northSegment.itemSpawned != null)
            {
                ItemManager.Instance.ReturnItem(m_northSegment.itemSpawned);
            }
            Destroy(m_northSegment.gameObject);
        }

        if (excludeDirection != Direction.South && m_southSegment != null)
        {
            if (m_southSegment.itemSpawned != null)
            {
                ItemManager.Instance.ReturnItem(m_southSegment.itemSpawned);
            }
            Destroy(m_southSegment.gameObject);
        }

        if (excludeDirection != Direction.East && m_eastSegment != null)
        {
            if (m_eastSegment.itemSpawned != null)
            {
                ItemManager.Instance.ReturnItem(m_eastSegment.itemSpawned);
            }
            Destroy(m_eastSegment.gameObject);
        }

        if (excludeDirection != Direction.West && m_westSegment != null)
        {
            if (m_westSegment.itemSpawned != null)
            {
                ItemManager.Instance.ReturnItem(m_westSegment.itemSpawned);
            }
            Destroy(m_westSegment.gameObject);
        }

        m_direction = Direction.None;
    }

    public void GenerateAdjacentSegments(Direction excludeDirection)
    {
        if (excludeDirection != Direction.North)
        {
            GenerateSegment(Direction.North, out m_northSegment, new Vector3(0, 0, SegmentManager.Instance.GetOffset()));
        }

        if (excludeDirection != Direction.South)
        {
            GenerateSegment(Direction.South, out m_southSegment, new Vector3(0, 0, -SegmentManager.Instance.GetOffset()));
        }

        if (excludeDirection != Direction.East)
        {
            GenerateSegment(Direction.East, out m_eastSegment, new Vector3(SegmentManager.Instance.GetOffset(), 0, 0));
        }

        if (excludeDirection != Direction.West)
        {
            GenerateSegment(Direction.West, out m_westSegment, new Vector3(-SegmentManager.Instance.GetOffset(), 0, 0));
        }

        m_direction = Direction.None;
    }

    void GenerateSegment(Direction direction, out Segment segment, Vector3 positionOffset)
    {
        segment = Instantiate(m_myPrefab, transform.position + positionOffset, Quaternion.identity).GetComponent<Segment>();

        segment.m_direction = direction;
        segment.gameObject.name = "Segment - " + direction.ToString();
        segment.itemSpawned = ItemManager.Instance.GetRandomItem();
        if (segment.itemSpawned != null)
        {
            segment.itemSpawned.transform.position = segment.GetSpawnPoint();
        }
    }
    public Vector3 GetSpawnPoint()
    {
        if (m_spawnPoints != null && m_spawnPoints.childCount > 0)
        {
            int index = Random.Range(0, m_spawnPoints.childCount);
            return m_spawnPoints.GetChild(index).position;
        }
        return transform.position;

    }

    public Vector3 GetRandomAdjacentSegment()
    {
        List<Segment> segments = new List<Segment>();

        if (m_northSegment != null)
            segments.Add(m_northSegment);
        if (m_southSegment != null)
            segments.Add(m_southSegment);
        if (m_eastSegment != null)
            segments.Add(m_eastSegment);
        if (m_westSegment != null)
            segments.Add(m_westSegment);

        return segments[Random.Range(0, segments.Count)].transform.position;
    }
}
