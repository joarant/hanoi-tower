using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public Dictionary<GameObject, List<int>> towers = new Dictionary<GameObject, List<int>>();
    Movement movement;
    int moveCount = 0;
    int ringCount;
    public GameUI gameUI;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    public void GetComponents(int hanoiRingCount) 
    {
        ringCount = hanoiRingCount;
    }

    public void MoveRing(GameObject fromTower, GameObject toTower)
    {
        moveCount++;

        int num = towers[fromTower][towers[fromTower].Count - 1];
        GameObject ring;

        for (int i = 0; i < fromTower.transform.childCount; i++)
        {
            if (fromTower.transform.GetChild(i).gameObject.GetComponent<RingValue>().value == num)
            {
                ring = fromTower.transform.GetChild(i).gameObject;
                ring.transform.parent = toTower.transform;
                movement.MoveToPosition(ring, toTower);
            }
        }
        towers[toTower].Add(num);
        towers[fromTower].Remove(num);
        gameUI.UpdateMovesText(moveCount);

        if (CheckIfWon())
        {
            gameUI.GameWonText();
        }

    }

    public void ConnectTowers()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            towers.Add(transform.GetChild(i).gameObject, new List<int>());
            for (int q = transform.GetChild(i).childCount - 1; q >= 0; q--)
            {
                towers[transform.GetChild(i).gameObject].Add(transform.GetChild(i).GetChild(q).gameObject.GetComponent<RingValue>().value);
            }
        }
        //transform.GetComponent<Select>().GetOriginalMaterials();

    }


    bool CheckIfWon()
    {
        if (towers[transform.GetChild(transform.childCount - 1).gameObject].Count == ringCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
