using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private Memory _memory;

    //���� ��������� �������� �� ������������, ��� ������ ����������� ����������� �� ����������.
    //�� � � ����� ������� ���� ���������
    public void AnalizeForeignObjects(Goal foreignObject)
    {
        if (_memory.TryingToRemember(foreignObject))
        {
            Debug.Log("I remember it!");
        }
        else
        {
            _memory.MemorizeObject(foreignObject);
        }

        //����� �������� ����������� � ����� �������� ����� ��� ��������
        //����� ���������� ���������� � ��������
    }
}
