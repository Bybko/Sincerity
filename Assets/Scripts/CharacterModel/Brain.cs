using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;

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

        Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
        //���������� ���������� � ��������
    }
}
