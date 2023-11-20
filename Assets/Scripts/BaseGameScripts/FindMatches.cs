using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        StartCoroutine(FindAllMatchesCo());
    }

    //private List<GameObject> IsAdjacentBomb(Dot dot1, Dot dot2, Dot dot3)
    //{
    //    List<GameObject> currentDots = new List<GameObject>();
    //    if (dot1.isAdjacentBomb)
    //    {
    //        currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
    //    }

    //    if (dot2.isAdjacentBomb)
    //    {
    //        currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
    //    }

    //    if (dot3.isAdjacentBomb)
    //    {
    //        currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
    //    }
    //    return currentDots;
    //}

    private List<GameObject> IsAdjacentBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb)
        {
            currentDots.AddRange(GetAdjacentPieces(dot1.column, dot1.row));
        }

        if (dot2.isAdjacentBomb)
        {
            currentDots.AddRange(GetAdjacentPieces(dot2.column, dot2.row));
        }

        if (dot3.isAdjacentBomb)
        {
            currentDots.AddRange(GetAdjacentPieces(dot3.column, dot3.row));
        }
        return currentDots.Distinct().ToList();
    }

    //private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3)
    //{
    //    List<GameObject> currentDots = new List<GameObject>();
    //    if (dot1.isRowBomb)
    //    {
    //        currentMatches.Union(GetRowPieces(dot1.row));
    //    }

    //    if (dot2.isRowBomb)
    //    {
    //        currentMatches.Union(GetRowPieces(dot2.row));
    //    }

    //    if (dot3.isRowBomb)
    //    {
    //        currentMatches.Union(GetRowPieces(dot3.row));
    //    }
    //    return currentDots;
    //}
    private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb)
        {
            currentDots.AddRange(GetRowPieces( dot1.row));
        }

        if (dot2.isRowBomb)
        {
            currentDots.AddRange(GetRowPieces( dot2.row));
        }

        if (dot3.isRowBomb)
        {
            currentDots.AddRange(GetRowPieces( dot3.row));
        }
        return currentDots.Distinct().ToList();
    }
    //private List<GameObject> IsColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    //{
    //    List<GameObject> currentDots = new List<GameObject>();
    //    if (dot1.isColumnBomb)
    //    {
    //        currentMatches.Union(GetColumnPieces(dot1.column));
    //    }

    //    if (dot2.isColumnBomb)
    //    {
    //        currentMatches.Union(GetColumnPieces(dot2.column));
    //    }

    //    if (dot3.isColumnBomb)
    //    {
    //        currentMatches.Union(GetColumnPieces(dot3.column));
    //    }
    //    return currentDots;
    //}

    private List<GameObject> IsColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb)
        {
            currentDots.AddRange(GetColumnPieces(dot1.column));
        }

        if (dot2.isColumnBomb)
        {
            currentDots.AddRange(GetColumnPieces(dot2.column));
        }

        if (dot3.isColumnBomb)
        {
            currentDots.AddRange(GetColumnPieces(dot3.column));
        }
        return currentDots.Distinct().ToList();
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

    //private IEnumerator FindAllMatchesCo()
    //{
    //    yield return new WaitForSeconds(.2f);
    //    for (int i = 0; i < board.width; i++)
    //    {
    //        for (int j = 0; j < board.height; j++)
    //        {
    //            GameObject currentDot = board.allDots[i, j];

    //            if (currentDot != null)
    //            {
    //                Dot currentDotDot = currentDot.GetComponent<Dot>();
    //                if (i > 0 && i < board.width - 1)
    //                {
    //                    GameObject leftDot = board.allDots[i - 1, j];

    //                    GameObject rightDot = board.allDots[i + 1, j];


    //                    if (leftDot != null && rightDot != null)
    //                    {
    //                        Dot rightDotDot = rightDot.GetComponent<Dot>();
    //                        Dot leftDotDot = leftDot.GetComponent<Dot>();
    //                        if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
    //                        {

    //                            currentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));

    //                            currentMatches.Union(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));

    //                            currentMatches.Union(IsAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));


    //                            GetNearbyPieces(leftDot, currentDot, rightDot);

    //                        }
    //                    }

    //                }

    //                if (j > 0 && j < board.height - 1)
    //                {
    //                    GameObject upDot = board.allDots[i, j + 1];

    //                    GameObject downDot = board.allDots[i, j - 1];


    //                    if (upDot != null && downDot != null)
    //                    {
    //                        Dot downDotDot = downDot.GetComponent<Dot>();
    //                        Dot upDotDot = upDot.GetComponent<Dot>();
    //                        if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
    //                        {

    //                            currentMatches.Union(IsColumnBomb(upDotDot, currentDotDot, downDotDot));

    //                            currentMatches.Union(IsRowBomb(upDotDot, currentDotDot, downDotDot));

    //                            currentMatches.Union(IsAdjacentBomb(upDotDot, currentDotDot, downDotDot));


    //                            GetNearbyPieces(upDot, currentDot, downDot);

    //                        }
    //                    }
    //                }


    //            }
    //        }
    //    }

    //}
    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];

                if (currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];

                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                currentMatches.AddRange(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.AddRange(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.AddRange(IsAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));
                                GetNearbyPieces(leftDot, currentDot, rightDot);
                            }
                        }
                    }

                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];

                        if (upDot != null && downDot != null)
                        {
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                currentMatches.AddRange(IsColumnBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.AddRange(IsRowBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.AddRange(IsAdjacentBomb(upDotDot, currentDotDot, downDotDot));
                                GetNearbyPieces(upDot, currentDot, downDot);
                            }
                        }
                    }
                }
            }
        }

        currentMatches = currentMatches.Distinct().ToList();
    }

    public void MatchPiecesOfColor(string color)
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
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
        for (int i = column - 1; i <= column + 1; i++)
        {
            for (int j = row - 1; j <= row + 1; j++)
            {
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allDots[i, j] != null)
                    {
                        dots.Add(board.allDots[i, j]);
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
        return dots;
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if (board.allDots[column, i] != null)
            {
                Dot dot = board.allDots[column, i].GetComponent<Dot>();
                if (dot.isRowBomb) {
                    dots.Union(GetRowPieces(i)).ToList();
                }

                dots.Add(board.allDots[column, i]);
                dot.isMatched = true;
            }
        }
        return dots;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i, row] != null)
            {
                Dot dot = board.allDots[i,row].GetComponent<Dot>();
                if (dot.isColumnBomb)
                {
                    dots.Union(GetColumnPieces(i)).ToList();
                }
                dots.Add(board.allDots[i, row]);
                dot.isMatched = true;
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
                /*
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50){
                    board.currentDot.MakeRowBomb();
                }else if(typeOfBomb >= 50){
                    board.currentDot.MakeColumnBomb();
                }
                */
                if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135))
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
                if (otherDot.isMatched)
                {
                    otherDot.isMatched = false;
                    /*
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        otherDot.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        otherDot.MakeColumnBomb();
                    }
                    */
                    if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135))
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

}