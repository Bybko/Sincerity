using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Подключен к подсознанию и отвечает за передачу информации о физическом состоянии персонажа. Его голод, энергия и т.д.
// Соответственно будет влиять на потребности отдыха, еды и т.д.
// По поводу энергии - будет условно 100 единиц, которые будут тратиться, она будет увеличиваться в том числе и с едой.
// Таким образом энергия будет влиять или на голод или на желание сна
public class PhysicalStatus : MonoBehaviour
{
    [SerializeField] private float _foodEnergySpending = 5f;
    [SerializeField] private float _requestedFoodResources = 100f;
    private float _currentFoodResources;


    private void Start()
    {
        _currentFoodResources = _requestedFoodResources;

        //Таймер и другие махинации со временем лучше потом сделать в отдельном классе. Класс, который в целом следит за временем.
        //Можно реально сделать его как глобальные часы и сделать метод checkTime и на основе его что-то делать.
        StartCoroutine(DecreaseEnergyOverTime());
    }


    public float GetCurrentFoodResources()
    {
        return _currentFoodResources;
    }

    public float GetRequestedFoodResources()
    {
        return _requestedFoodResources;
    }


    private IEnumerator DecreaseEnergyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            DecreaseEnergy();
        }
    }


    private void DecreaseEnergy()
    {
        _currentFoodResources = Mathf.Clamp(_currentFoodResources - _foodEnergySpending, 0f, _requestedFoodResources);
        Debug.Log("Текущая энергия: " + _currentFoodResources);
    }
}
