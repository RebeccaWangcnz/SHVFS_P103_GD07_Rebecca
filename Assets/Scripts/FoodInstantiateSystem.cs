using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstantiateSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] foods;
    public GameObject prop;
    public InstantiatePos[] instantiatePos;
    public PropInstantiatePos[] propInstantiatePos;
    public int instantiateNumber_min=2;
    public int instantiateNumber_max=5;

    private float timer_food;
    private float timer_prop;
    [Header("Time Range")]
    public float minTime;
    public float maxTime;
    private float food_time;
    public float prop_time;
    private void Awake()
    {
        instantiatePos = FindObjectsOfType<InstantiatePos>();
        propInstantiatePos = FindObjectsOfType<PropInstantiatePos>();
        InstantiateFood();
        food_time = Random.Range(minTime, maxTime);
    }
    private void Update()
    {
        timer_food += Time.deltaTime;
        timer_prop += Time.deltaTime;
        if(timer_food>food_time)
        {
            timer_food = 0;
            food_time = Random.Range(minTime, maxTime);
            InstantiateFood();
        }
        if(timer_prop>prop_time)
        {
            timer_prop = 0;
            InstantiateProp();
        }
    }
    void InstantiateFood()
    {
        //the number of food to instantiate
        var number = Random.Range(instantiateNumber_min, instantiateNumber_max);
        for(int i=0;i<number;i++)
        {
            Instantiate(foods[Random.Range(0, foods.Length)],instantiatePos[Random.Range(0,instantiatePos.Length)].transform.position,Quaternion.identity);
        }
    }
    void InstantiateProp()
    {
        for(int i=0;i<propInstantiatePos.Length;i++)
        {
            Instantiate(prop, propInstantiatePos[i].transform.position, Quaternion.identity);
        }
    }
}
