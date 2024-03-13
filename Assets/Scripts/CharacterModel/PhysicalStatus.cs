using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� � ����������� � �������� �� �������� ���������� � ���������� ��������� ���������. ��� �����, ������� � �.�.
// �������������� ����� ������ �� ����������� ������, ��� � �.�.
// �� ������ ������� - ����� ������� 100 ������, ������� ����� ���������, ��� ����� ������������� � ��� ����� � � ����.
// ����� ������� ������� ����� ������ ��� �� ����� ��� �� ������� ���
public class PhysicalStatus : MonoBehaviour
{
    [SerializeField] private float _foodEnergySpending = 5f;
    [SerializeField] private float _requestedFoodResources = 100f;
    private float _currentFoodResources;
    //�� ������� ��������� ��, ������� ������ ��������. ������� ������������ � �.�. �� ���� ���� ���� ��, �� ��� ������
    //�� ���������������� ��������� � �������� �� �������
    [SerializeField] private float _health = 100f; 

    private void Start()
    {
        _currentFoodResources = _requestedFoodResources;

        //������ � ������ ��������� �� �������� ����� ����� ������� � ��������� ������. �����, ������� � ����� ������ �� ��������.
        //����� ������� ������� ��� ��� ���������� ���� � ������� ����� checkTime � �� ������ ��� ���-�� ������.
        StartCoroutine(DecreaseEnergyOverTime());
    }


    private IEnumerator DecreaseEnergyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            DecreaseEnergy();
        }
    }


    public void SetRandomValues()
    {
        _currentFoodResources = Random.Range(0f, 1f);
        _health = Random.Range(15f, 100f);
    }


    public void ChangeFoodResources(float foodValue) { _currentFoodResources += foodValue; }
    public void ChangeHealth(float hpValue) { _health += hpValue; }


    private void DecreaseEnergy()
    {
        _currentFoodResources = Mathf.Clamp(_currentFoodResources - _foodEnergySpending, 0f, _requestedFoodResources);
        //Debug.Log("������� �������: " + _currentFoodResources);

        float hungerDamage = 5f;
        if (_currentFoodResources < 10f)
        {
            _health = Mathf.Clamp(_health - hungerDamage, 0f, 100f);
        }

        if (_currentFoodResources > 90f)
        {
            _health = Mathf.Clamp(_health + hungerDamage, 0f, 100f);
        }
        //Debug.Log("������� ��������: " + _health);
    }


    public float GetCurrentFoodResources() { return _currentFoodResources; }
    public float GetRequestedFoodResources() { return _requestedFoodResources; }
    public float GetHealth() { return _health; }
}
