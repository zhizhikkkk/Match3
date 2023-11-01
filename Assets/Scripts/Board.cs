using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    wait,
    move
}

public class Board : MonoBehaviour
{
    public GameState currentState=GameState.move;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject[] dots;
    public Dot currentDot;
    public GameObject destroyEffect;
    public GameObject[,] allDots;
    private BackgroundTile[,] allTiles;
    private int offset=20;
    private FindMatches findMatches;
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        findMatches = FindObjectOfType<FindMatches>();
        SetUp();
    }

    private void SetUp()
    {
        for(int i=0; i < width; i++)
        {
            for(var j=0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i,j + offset);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity,this.transform);
                backgroundTile.name ="(" + i + ", " + j + ")";

                int dotToUse = Random.Range(0, dots.Length);
                int maxIterations = 0;
                while (MatchesAt(i, j, dots[dotToUse]) && maxIterations<100)
                {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity, this.transform);
                dot.GetComponent<Dot>().row = j;
                dot.GetComponent<Dot>().column = i;
                dot.name = "(" + i + ", " + j + ")";
                allDots[i,j] = dot;
            }
        }
    }
    private bool MatchesAt(int column,int row, GameObject piece)
    {
        if(column>1 && row > 1)
        {
            if (allDots[column-1,row].tag == piece.tag &&
                allDots[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column,row-1].tag == piece.tag &&
                allDots[column , row-2].tag == piece.tag)
            {
                return true;
            }
        }
        else if(column<=1 || row <= 1)
        {
            if (row > 1)
            {
                if(allDots[column,row-1].tag == piece.tag &&
                    allDots[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if(allDots[column-1,row].tag == piece.tag &&
                    allDots[column-2, row ].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column,int row)
    {
        if (allDots[column, row].GetComponent<Dot>().isMatched)
        {
            if(findMatches.currentMatches.Count==4 || findMatches.currentMatches.Count == 7)
            {
                findMatches.CheckBombs();
            }
            findMatches.currentMatches.Remove(allDots[column, row]);
            GameObject particle = Instantiate(destroyEffect, allDots[column, row].transform.position, Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(allDots[column, row]);
            allDots[column, row] = null;
        }
    }
    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j=0; j < height; j++)
            {
                if(allDots[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for(int i=0;i< width; i++)
        {
            for(int j=0; j< height; j++)
            {
                if (allDots[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] == null )
                {
                    Vector2 tempPosition = new Vector2(i, j + offset);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i, j] = piece;
                    piece.GetComponent<Dot>().row = j;
                    piece.GetComponent<Dot>().column = i;

                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(allDots[i,j] != null)
                {
                    if (allDots[i, j].GetComponent<Dot>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        findMatches.currentMatches.Clear();
        currentDot = null;
        yield return new WaitForSeconds(.5f);
        currentState = GameState.move;
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}
