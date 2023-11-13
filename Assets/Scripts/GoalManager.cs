using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlankGoal
{
    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
}
public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public List<GoalPanel> currentGoals = new List<GoalPanel>();
    public GameObject goalPrefab;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;

    void Start()
    {
        SetUpGoals();
    }
    void SetUpGoals()
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity, goalIntroParent.transform);

            GoalPanel panel = goal.GetComponent<GoalPanel>();
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;

            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity, goalGameParent.transform);

            panel = gameGoal.GetComponent<GoalPanel>();
            currentGoals.Add(panel);
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;
        }
    }

    public void UpdateGoals()
    {
        int goalsCompleted = 0;
        for(int i = 0; i < levelGoals.Length; i++)
        {
            currentGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;
            if(levelGoals[i].numberCollected >= levelGoals[i].numberNeeded)
            {
                goalsCompleted++;
                currentGoals[i].thisText.text= "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
            }
            if (goalsCompleted >= levelGoals.Length)
            {
                Debug.Log("You win!");
            }
        }
    }

    public void CompareGoal(string goalToCompare)
    {
        for(int i=0;i< levelGoals.Length; i++)
        {
            if(goalToCompare== levelGoals[i].matchValue)
            {
                levelGoals[i].numberCollected++;
            }
        }
    }
}
