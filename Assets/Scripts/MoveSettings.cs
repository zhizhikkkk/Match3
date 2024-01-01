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
            // ���� ������ �� ��� ���������, ���������� ��� ���� �� 10 ������
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            isMoved = true;
        }
    }

    public void MoveSettingsObject()
    {
        if (!isMoved)
        {
            // ���� ������ �� ��� ���������, ���������� ��� ���� �� 10 ������
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            isMoved = true;
        }
        else
        {
            // ���� ������ ��� ��� ���������, ���������� ��� �� �������� �������
            transform.position = originalPosition;
            isMoved = false;
        }
    }


}
