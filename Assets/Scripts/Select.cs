using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Select : MonoBehaviour
{

    GameObject fromTower = null;
    GameObject toTower = null;
    State state;
    //Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    public Material seleted;
    public Material canBeMovedTo;
    public Material cannotBeMovedTo;
    public Material defaultMaterial;


    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<State>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            if (Input.touchCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Physics.Raycast(ray, out hit);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
            }
            if (hit.collider != null && hit.collider.gameObject != fromTower && state.towers.ContainsKey(hit.collider.gameObject))
            {
                if (fromTower == null && state.towers[hit.collider.gameObject].Count > 0)
                {
                    fromTower = hit.collider.gameObject;
                }
                else if (fromTower != null && (state.towers[hit.collider.gameObject].Count == 0 
                    || state.towers[hit.collider.gameObject][state.towers[hit.collider.gameObject].Count - 1] >
                    state.towers[fromTower][state.towers[fromTower].Count - 1]))
                {
                    toTower = hit.collider.gameObject;

                    state.MoveRing(fromTower, toTower);
                    fromTower = null;
                    toTower = null;
                }
            }
            else if (hit.collider == null || hit.collider.gameObject == fromTower)
            {

                fromTower = null;
                toTower = null;
            }
            CanBeSelected();
        }
    }
    void CanBeSelected()
    {
        foreach (var pair in state.towers)
        {
            if (fromTower == null)
            {
                pair.Key.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
            else
            {
                if (pair.Key == fromTower)
                {
                    pair.Key.GetComponent<MeshRenderer>().material = seleted;
                }
                else if (pair.Value.Count == 0 || pair.Value[pair.Value.Count - 1]  >
                    state.towers[fromTower][state.towers[fromTower].Count - 1])
                {
                    pair.Key.GetComponent<MeshRenderer>().material = canBeMovedTo;
                }
                else
                {
                    pair.Key.GetComponent<MeshRenderer>().material = cannotBeMovedTo;
                }
            }

        }
    }

    //public void GetOriginalMaterials()
    //{
    //    foreach (var pair in state.towers)
    //    {
    //        originalMaterials.Add(pair.Key, pair.Key.gameObject.GetComponent<MeshRenderer>().material);
    //    }
    //}
}
