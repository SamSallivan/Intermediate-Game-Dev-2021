
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PathSystem : MonoBehaviour
{
    public enum SeedType
    {
        Random,
        Custom
    }

    public SeedType seedType = SeedType.Random;
    [SerializeField]
    private int seed = 0;

    private Random random;

    [Space]
    public bool animatedPath;


    public List<MyGridCell> gridCellList = new List<MyGridCell>();

    public int pathLength = 10;

    [Range(1f, 10f)]
    public float cellSize = 1;

    public Transform startLocation;


    private void Start()
    {
        SetSeed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animatedPath)
            {
                StartCoroutine(CreatePathRoutine());
            }
            else
            {
                CreatePath();
            }
        }
    }

    private void SetSeed()
    {
        if (seedType == SeedType.Random)
        {
            random = new Random();
        }
        else if (seedType == SeedType.Custom)
        {
            random = new Random(seed);
        }
    }

    private void CreatePath()
    {
        gridCellList.Clear();
        Vector2 currentPosition = startLocation.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++)
        {
            int n = random.Next(100);

            if (n.IsBetween(0, 49))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
        }
    }

    IEnumerator CreatePathRoutine()
    {
        gridCellList.Clear();
        Vector2 currentPosition = startLocation.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++)
        {
            int n = random.Next(100);

            if (n.IsBetween(0, 49))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < gridCellList.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);
            Gizmos.color = new Color(0.9f, 0.9f, 0.9f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector2.one * cellSize);

        }
    }
}