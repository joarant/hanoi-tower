using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Attach this script to cube
    private GameObject tower1;
    private GameObject tower2;
    private GameObject tower3;

    [SerializeField]
    private int ringSpeed = 5;
    [SerializeField]
    private bool animateRingMovement = true;
    [SerializeField]
    private int ringCount = 4;

    private float ringHeightScale = 0.09f;
    private float ringBaseDiameterScale = 1;
    private float ringDiameterScaleIncrease = 0.2f;
    private Vector3 defaulTowerScale = new Vector3(0.5f, 1.3f, 0.5f);
    private Vector3 defaulPlatformScale = new Vector3(1, 0.3f, 6.6f);
    public GameUI gameUI;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("animOn"))
        {
            ringSpeed = PlayerPrefs.GetInt("animSpeed");
            animateRingMovement = PlayerPrefs.GetInt("animOn") == 1 ? true : false;
            ringCount = PlayerPrefs.GetInt("ringCount");
        }
        tower1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        CapsuleCollider cCollider = (CapsuleCollider)tower1.GetComponent(typeof(CapsuleCollider));
        tower1.transform.localScale = new Vector3(defaulTowerScale.x, ringHeightScale * (ringCount + 5), defaulTowerScale.z);
        cCollider.radius = 2 + ringCount * .08f;
        cCollider.height = 2 + ringCount * .08f;
        tower2 = Instantiate(tower1);
        tower3 = Instantiate(tower1);
        Vector3 ringDims = createRings(ringCount);
        float correctWidth = ringDims.z * 4;
        float correctDepth = ringDims.x * 1.1f;
        float correctWidthActual = defaulPlatformScale.z > correctWidth ?
             defaulPlatformScale.z : correctWidth;
        transform.localScale = new Vector3(correctDepth, defaulPlatformScale.y, correctWidthActual);
        GameObject[] towers = { tower1, tower2, tower3 };
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < towers.Length; i++)
        {
            towers[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y + (towers[i].GetComponent<Renderer>().bounds.size.y / 2)
                    + GetComponent<Renderer>().bounds.size.y / 2,
                transform.position.z - GetComponent<Renderer>().bounds.size.z / 2
                    + GetComponent<Renderer>().bounds.size.z / 6
                    + GetComponent<Renderer>().bounds.size.z / 3 * i);
            towers[i].transform.parent = transform;
        }
        transform.position = position;
        transform.rotation = rotation;
        //float horizontalFov = 2 * Mathf.Atan(Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2) * Camera.main.aspect) * Mathf.Rad2Deg;
        float horizontalFov = Camera.VerticalToHorizontalFieldOfView(Camera.main.fieldOfView, Camera.main.aspect);
        float cameraDistance = GetComponent<Renderer>().bounds.size.x / 2 / Mathf.Tan(horizontalFov / 2 * Mathf.Deg2Rad);
        float cameraHeight = cameraDistance * Mathf.Tan(Camera.main.fieldOfView / 2 * Mathf.Deg2Rad);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.x + cameraHeight * 1.6f, transform.position.z + (cameraDistance + GetComponent<Renderer>().bounds.size.z / 2) * -1f);
        State state = GetComponent<State>();
        state.GetComponents(ringCount);
        GetComponent<Movement>().GetComponents(animateRingMovement, ringSpeed);
        gameUI.CountMinMoves(ringCount);
        state.ConnectTowers();


    }

    private Vector3 createRings(int ringCount)
    {
        Vector3 largestRingDimesions = Vector3.zero;
        for (int i = 0; i < ringCount; i++)
        {
            GameObject ring = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            ring.transform.localScale = new Vector3(ringBaseDiameterScale + (i * ringDiameterScaleIncrease),
                ringHeightScale, ringBaseDiameterScale + (i * ringDiameterScaleIncrease));
            Destroy(ring.GetComponent(typeof(CapsuleCollider)));
            ring.transform.SetParent(tower1.transform);
            float correctPosInTower = tower1.transform.position.y - tower1.GetComponent<Renderer>().bounds.size.y / 2 + ring.GetComponent<Renderer>().bounds.size.y / 2;
            ring.transform.position = new Vector3(ring.transform.parent.position.x, correctPosInTower + ((ringCount - 1 - i) * ring.GetComponent<Renderer>().bounds.size.y), ring.transform.parent.position.z);
            ring.AddComponent(typeof(RingValue));
            ring.GetComponent<RingValue>().value = i;
            ring.name = "C" + i;
            largestRingDimesions = ring.GetComponent<Renderer>().bounds.size;
            ring.GetComponent<MeshRenderer>().material.color = i % 2 == 0 ? Color.yellow : Color.cyan;

        }
        return largestRingDimesions;

    }


}
