using UnityEngine;

public class MoveSettings : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isMoved = false;

    void Start()
    {
        originalPosition = transform.position;
        if (!isMoved)
        {
            // Если объект не был перемещен, перемещаем его вниз на 10 единиц
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            isMoved = true;
        }
    }

    public void MoveSettingsObject()
    {
        if (!isMoved)
        {
            // Если объект не был перемещен, перемещаем его вниз на 10 единиц
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            isMoved = true;
        }
        else
        {
            // Если объект уже был перемещен, возвращаем его на исходную позицию
            transform.position = originalPosition;
            isMoved = false;
        }
    }


}
