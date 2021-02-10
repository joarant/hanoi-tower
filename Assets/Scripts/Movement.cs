using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool animateRingMovement = true;
    State state;
    Dictionary<GameObject, IEnumerator> coroutines = new Dictionary<GameObject, IEnumerator>();
    private int speed = 5;


    public void GetComponents(bool animOn, int ringSpeed)
    {
        state = GetComponent<State>();
        animateRingMovement = animOn;
        speed = ringSpeed;
    }

    public void MoveToPosition(GameObject ring, GameObject tower)
    {
        if (animateRingMovement)
        {
            if (coroutines.ContainsKey(ring))
            {
                StopCoroutine(coroutines[ring]);
                coroutines.Remove(ring);
            }
            Vector3 newPosition = new Vector3(tower.transform.position.x,
             tower.transform.position.y - tower.GetComponent<Renderer>().bounds.size.y / 2 + ring.GetComponent<Renderer>().bounds.size.y / 2 + ring.GetComponent<Renderer>().bounds.size.y * state.towers[tower].Count,
             tower.transform.position.z);
            float tempHeight = tower.GetComponent<Renderer>().bounds.size.y * 1.25f / 2 + tower.transform.position.y;
            Vector3 tempPos = new Vector3(tower.transform.position.x, ring.transform.position.y, tower.transform.position.z);
            coroutines.Add(ring, MoveToPosWithAnimation(ring, tempHeight, tempPos, newPosition));
            StartCoroutine(coroutines[ring]);
        }
        else
        {
            Vector3 newPosition = new Vector3(tower.transform.position.x,
                tower.transform.position.y - tower.GetComponent<Renderer>().bounds.size.y / 2 + ring.GetComponent<Renderer>().bounds.size.y / 2 + ring.GetComponent<Renderer>().bounds.size.y * state.towers[tower].Count,
                tower.transform.position.z);
            ring.transform.position = newPosition;
        }


    }

    private IEnumerator MoveToPosWithAnimation(GameObject ring, float tempHeight, Vector3 tempPos, Vector3 newPosition)
    {
        float closeEnough = 0.05f;

        while (ring.transform.position != newPosition)
        {
            if (Vector3.Distance(new Vector3(0, ring.transform.position.y, 0), new Vector3(0, tempHeight, 0)) > closeEnough &&
                Vector3.Distance(new Vector3(ring.transform.position.x, 0, ring.transform.position.z), new Vector3(tempPos.x, 0, tempPos.z)) > closeEnough) // check if correct vertical position
            {
                Vector3 newStep = Step(new Vector3(0, ring.transform.position.y, 0), new Vector3(0, tempHeight, 0), speed, false);
                ring.transform.Translate(newStep, Space.World);
            }
            else if (Vector3.Distance(new Vector3(ring.transform.position.x, 0, ring.transform.position.z), new Vector3(tempPos.x, 0, tempPos.z)) > closeEnough) // check if correct horizontal position
            {
                Vector3 newStep = Step(new Vector3(ring.transform.position.x, 0, ring.transform.position.z), new Vector3(tempPos.x, 0, tempPos.z), speed, false);
                ring.transform.Translate(newStep, Space.World);
            }
            else if (Vector3.Distance(new Vector3(ring.transform.position.x, 0, ring.transform.position.z), new Vector3(tempPos.x, 0, tempPos.z)) <= closeEnough &&
                Vector3.Distance(ring.transform.position, newPosition) > closeEnough) // check if correct vertical position
            {
                ring.transform.position = new Vector3(newPosition.x, ring.transform.position.y, newPosition.z);
                Vector3 newStep = Step(ring.transform.position, newPosition, speed, false);
                ring.transform.Translate(newStep, Space.World);
            }
            else if (Vector3.Distance(ring.transform.position, newPosition) <= closeEnough) // snap to correct position
            {
                ring.transform.position = newPosition;
            }
            yield return null;
        }
    }

    private Vector3 Step(Vector3 current, Vector3 target, float speed, bool overStep)
    {
        Vector3 step = Vector3.Normalize((target - current)) * speed * Time.deltaTime;
        if (!overStep && Vector3.Normalize(target - (current + step)) * speed * Time.deltaTime != step)
        {
            step = target - current;
        }
        return step;
    }


}
