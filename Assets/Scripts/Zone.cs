using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [HideInInspector]
    public int scoreContain;
    // Start is called before the first frame update
    private void Start()
    {
        scoreContain = 0;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<FoodLogic>())
        {
            scoreContain += other.GetComponent<FoodLogic>().foodScore;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FoodLogic>())
        {
            scoreContain -= other.GetComponent<FoodLogic>().foodScore;
        }
    }
}
