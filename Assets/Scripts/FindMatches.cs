using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllmatchesCo());
    }

    private List<GameObject> IsAdjacentBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb)
        {
            currentDots.Union(GetAdjacentPieces(dot1.column,dot1.row));
        }
        if (dot2.isAdjacentBomb)
        {
            currentDots.Union(GetAdjacentPieces(dot1.column, dot2.row));
        }
        if (dot3.isAdjacentBomb)
        {
            currentDots.Union(GetAdjacentPieces(dot1.column, dot3.row));
        }
        return currentDots;
    }
    private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb)
        {
            currentDots.Union(GetRowPieces(dot1.row));
        }
        if (dot2.isRowBomb)
        {
            currentDots.Union(GetRowPieces(dot2.row));
        }
        if (dot3.isRowBomb)
        {
            currentDots.Union(GetRowPieces(dot3.row));
        }
        return currentDots;
    }
    private List<GameObject> IsColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb)
        {
            currentDots.Union(GetColumnPieces(dot1.column));
        }
        if (dot2.isColumnBomb)
        {
            currentDots.Union(GetColumnPieces(dot2.column));
        }
        if (dot3.isColumnBomb)
        {
            currentDots.Union(GetColumnPieces(dot3.column));
        }
        return currentDots;
    }
    private void AddToListAndMatch(GameObject dot)
    {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }
    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3)
    {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }
    private IEnumerator FindAllmatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.GetWidth(); i++)
        {
            for (int j = 0; j < board.GetHeight(); j++)
            {
                GameObject currentDot = board.allDots[i, j];

                if (currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.GetWidth() - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];

                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            if (leftDot != null && rightDot != null)
                            {
                                if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                                {

                                    currentMatches.Union(IsRowBomb(currentDotDot, leftDotDot, rightDotDot));

                                    currentMatches.Union(IsColumnBomb(currentDotDot, leftDotDot, rightDotDot));

                                    currentMatches.Union(IsAdjacentBomb(currentDotDot, leftDotDot, rightDotDot));

                                    GetNearbyPieces(currentDot, leftDot, rightDot);
                                }
                            }
                        }

                    }
                    if (j > 0 && j < board.GetHeight() - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];

                        GameObject downDot = board.allDots[i, j - 1];

                        if (upDot != null && downDot != null)
                        {
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            if (upDot != null && downDot != null)
                            {
                                if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                                {
                                    currentMatches.Union(IsColumnBomb(currentDotDot, upDotDot, downDotDot));

                                    currentMatches.Union(IsRowBomb(currentDotDot, upDotDot, downDotDot));

                                    currentMatches.Union(IsAdjacentBomb(currentDotDot, upDotDot, downDotDot));

                                    GetNearbyPieces(currentDot, upDot, downDot);

                                }
                            }
                        }

                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(string color)
    {
        for (int i = 0; i < board.GetWidth(); i++)
        {
            for (int j = 0; j < board.GetHeight(); j++)
            {
                if (board.allDots[i, j] != null)
                {
                    if (board.allDots[i, j].tag == color)
                    {
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = column-1; i <= column+1; i++)
        {
            for(int j = row-1; j <= row+1; j++)
            {
                if(i>=0 && i<board.GetWidth() && j>=0 && j<board.GetHeight())
                {
                    dots.Add(board.allDots[i, j]);
                    board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                }
            }
        }
        return dots;
    }


    private List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.GetHeight(); i++)
        {
            if (board.allDots[column, i] != null)
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }
    private List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.GetWidth(); i++)
        {
            if (board.allDots[i, row] != null)
            {
                dots.Add(board.allDots[i, row]);
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }

    public void CheckBombs()
    {
        if (board.currentDot != null)
        {
            if (board.currentDot.isMatched)
            {
                board.currentDot.isMatched = false;
                if ((board.currentDot.swipeAngle <= 45 && board.currentDot.swipeAngle > -45)
                  || (board.currentDot.swipeAngle >= 135 || board.currentDot.swipeAngle < -135))
                {
                    board.currentDot.MakeRowBomb();
                }
                else
                {
                    board.currentDot.MakeColumnBomb();
                }
            }
            else if (board.currentDot.otherDot != null)
            {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                if ((board.currentDot.swipeAngle <= 45 && board.currentDot.swipeAngle > -45)
                  || (board.currentDot.swipeAngle >= 135 || board.currentDot.swipeAngle < -135))
                {
                    otherDot.MakeRowBomb();
                }
                else
                {
                    otherDot.MakeColumnBomb();
                }
            }
        }
    }
}
