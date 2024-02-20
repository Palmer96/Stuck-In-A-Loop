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
        // remove previous adjuacent segments, exluding the one the player has entered
        SegmentManager.Instance.m_currentSegment.DeleteSegments(m_direction);

        // update segment information
        switch (m_direction)
        {
            case Direction.North:
                UpdateSegment(Direction.South, out m_southSegment);
                break;
            case Direction.South:
                UpdateSegment(Direction.North, out m_northSegment);
                break;
            case Direction.East:
                UpdateSegment(Direction.West, out m_westSegment);
                break;
            case Direction.West:
                UpdateSegment(Direction.East, out m_eastSegment);
                break;
        }

        // assign new center segment
        SegmentManager.Instance.m_currentSegment = this;
        SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - Current";

        // trigger segment creation event
        SegmentManager.Instance.SegmentGenerated();
    }

    void UpdateSegment(Direction direction, out Segment segment)
    {
        GenerateAdjacentSegments(direction);
        segment = SegmentManager.Instance.m_currentSegment;
        SegmentManager.Instance.m_currentSegment.m_direction = direction;
        SegmentManager.Instance.m_currentSegment.gameObject.name = "Segment - " + direction.ToString();
    }

    // remove segments and clean up any items in them
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

    // create new segments around the current segmentm exclusing the segment you came from
    public void GenerateAdjacentSegments(Direction excludeDirection)
    {
        if (excludeDirection != Direction.North)
        {
            GenerateSegment(Direction.North, out m_northSegment, new Vector3(0, 0, SegmentManager.Instance.segmentOffset));
        }

        if (excludeDirection != Direction.South)
        {
            GenerateSegment(Direction.South, out m_southSegment, new Vector3(0, 0, -SegmentManager.Instance.segmentOffset));
        }

        if (excludeDirection != Direction.East)
        {
            GenerateSegment(Direction.East, out m_eastSegment, new Vector3(SegmentManager.Instance.segmentOffset, 0, 0));
        }

        if (excludeDirection != Direction.West)
        {
            GenerateSegment(Direction.West, out m_westSegment, new Vector3(-SegmentManager.Instance.segmentOffset, 0, 0));
        }

        m_direction = Direction.None;
    }

    // generate a segment, assign necessary inforation and spawn an item
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

    // return a random position based on given of potential transforms
    public Vector3 GetSpawnPoint()
    {
        if (m_spawnPoints != null && m_spawnPoints.childCount > 0)
        {
            int index = Random.Range(0, m_spawnPoints.childCount);
            return m_spawnPoints.GetChild(index).position;
        }
        return transform.position;

    }

    // randomly select a segment around this segment
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
