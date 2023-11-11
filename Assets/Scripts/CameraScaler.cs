using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Board board;
    public float cameraOffset;
    public float aspectRatio = 0.625f;
    public float padding = 2;
    void Start()
    {
        board = FindObjectOfType<Board>();
        if(board != null)
        {
            RepositionCamera(board.GetWidth()-1,board.GetHeight()-1);
        }
        
    }
    void RepositionCamera(float x, float y)
    {
        Vector3 tempPosition = new Vector3(x / 2, y / 2,cameraOffset);
        transform.position = tempPosition;
        if (board.GetWidth() >= board.GetHeight())
        {
            Camera.main.orthographicSize = (board.GetWidth() / 2 + padding) / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = board.GetHeight() / 2 + padding;
        }
    }

}
