using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Tile Properties")]
    public Sprite[] tileSprites;
    public float[] tileSpriteValues;
    public float[] tileSpeeds;

    [Header("Grid Properties")]
    public Vector2 gridLength;
    public float tileSpacing;
    public Vector2 noiseScale;
    public Vector2 noiseOffset;
    public List<GameObject> allTiles = new List<GameObject>();
    public float heightAmp;

    [Header("Island Properties")]
    public bool useIsland;
    public Vector2 islandScale;
    public float islandEdge;

    [Header("Major Noise Properties")]
    public bool useMajNoise;
    public float majNoiseOpac;
    public Vector2 majNoiseScale;
    public Vector2 majNoiseOffset;

    [Header("Designations")]
    public GameObject tilePrefab;
    
    //Privates
    public GameObject[,] gridTiles;
    private Vector3 centerPoint;


    public Color testColor1;

    // Start is called before the first frame update
    void Start()
    {
        createGrid();
        centerPoint = Camera.main.transform.position;
        centerPoint = new Vector3(centerPoint.x, centerPoint.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            createGrid();
        }
    }

    public void createGrid()
    {
        if (allTiles.Count > 0)
        {
            foreach (GameObject currentTile in allTiles)
            {
                Destroy(currentTile);
            }
            allTiles.Clear();
        }

        noiseOffset = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
        majNoiseOffset = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
        gridTiles = new GameObject[Mathf.RoundToInt(gridLength.x), Mathf.RoundToInt(gridLength.y)];
        for (int y = 0; y < gridLength.y; y++)
        {
            for (int x = 0; x < gridLength.x; x++)
            {
                makeTile(x, y);
            }
        }
    }

    void makeTile(float xPos, float yPos)
    {
        //Make the new Tile
        GameObject newTile = Instantiate(tilePrefab);

        //int randTile = Random.Range(0, tileSprites.Length);

        //Sets sprite for new tile
        float tileValue = tileCalc(xPos, yPos);
        tileValue = Mathf.Clamp01(tileValue);

        int tileSprite = 0;
        for (int i = 0; i < tileSpriteValues.Length; i++)
        {
            if(tileValue <= tileSpriteValues[i])
            {
                tileSprite = i;
                break;
            }
        }
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[tileSprite];
        //newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[randTile];

        //Sets color for debugging
        //newTile.GetComponent<SpriteRenderer>().color = new Color(tileValue, tileValue, tileValue);

        //Positions the tile correctly
        newTile.transform.position = new Vector3(transform.position.x + xPos,transform.position.y + yPos, transform.position.z - (tileValue * heightAmp));

        //Sets up tile data
        tileData newData = newTile.GetComponent<tileData>();
        newData.tileSpeed = tileSpeeds[tileSprite];

        gridTiles[Mathf.RoundToInt(xPos), Mathf.RoundToInt(yPos)] = newTile;
        newData.tilePos = new Vector2(Mathf.RoundToInt(xPos), Mathf.RoundToInt(yPos));

        //Adds tile to overall list
        allTiles.Add(newTile);
    }

    float tileCalc(float xPos, float yPos)
    {
        float xCoord = ((xPos / gridLength.x) * noiseScale.x) + noiseOffset.x;
        float yCoord = ((yPos / gridLength.y) * noiseScale.y) + noiseOffset.y;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        float edgeMod = 0;
        if(useIsland)
        {
            Vector3 tilePos = new Vector3(transform.position.x + xPos, transform.position.y + yPos, transform.position.z);

            /*
            if(Mathf.Abs(Mathf.Abs(tilePos.x) - Mathf.Abs(centerPoint.x)) > (islandScale.x - islandEdge))
            {
                edgeMod = (Mathf.Abs(Mathf.Abs(tilePos.x) - Mathf.Abs(centerPoint.x)) - (islandScale.x - islandEdge)) / islandEdge;
            }
            if (Mathf.Abs(Mathf.Abs(tilePos.y) - Mathf.Abs(centerPoint.y)) > (islandScale.y - islandEdge))
            {
                edgeMod = (Mathf.Abs(Mathf.Abs(tilePos.y) - Mathf.Abs(centerPoint.y)) - (islandScale.y - islandEdge)) / islandEdge;
            }
            */

            if (Vector3.Distance(tilePos, centerPoint) > (islandScale.x - islandEdge))
            {
                edgeMod = (Vector3.Distance(tilePos, centerPoint) - (islandScale.x - islandEdge)) / islandEdge;
            }

            Debug.Log(edgeMod);
            if ((Vector3.Distance(tilePos, centerPoint) > (islandScale.x)) || (Vector3.Distance(tilePos, centerPoint) > (islandScale.y)))
            {
                edgeMod = 1;
            }

            /*
            if(Mathf.Abs(Mathf.Abs(tilePos.x) - Mathf.Abs(centerPoint.x)) > (islandScale.x) || Mathf.Abs(Mathf.Abs(tilePos.y) - Mathf.Abs(centerPoint.y)) > (islandScale.y))
            {
                //edgeMod = 1;
            }
            */
            sample -= edgeMod;

        }

        if(useMajNoise)
        {
            float xMajCoord = ((xPos / gridLength.x) * majNoiseScale.x) + majNoiseOffset.x;
            float yMajCoord = ((yPos / gridLength.y) * majNoiseScale.y) + majNoiseOffset.y;
            sample -= Mathf.PerlinNoise(xMajCoord, yMajCoord) * majNoiseOpac;
        }

        return sample;
    }

    public void islandToggle()
    {
        useIsland = !useIsland;
    }

    public void toggleScatter()
    {
        useMajNoise = !useMajNoise;
    }
    public void setIslandSize(sliderValue size)
    {
        islandScale = new Vector2(size.val, size.val);
    }
}
