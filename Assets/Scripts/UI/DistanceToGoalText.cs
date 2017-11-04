using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoalText : MonoBehaviour
{
    Text distanceToGoal;

    void Start()
    {
        distanceToGoal = GetComponent<Text>();
    }

    void Update()
    {
        distanceToGoal.text = string.Format("残り{0:0000}m", GameManager.Instance.DistanceToGoal);
    }
}
