using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    [SerializeField] NumberField score;
    [SerializeField] int pointsToAdd = 5;


    public void AddPoints(GameObject killed)
    {
        // add defferent score based on the killed object  
        score.AddNumber(pointsToAdd);

    }
}
