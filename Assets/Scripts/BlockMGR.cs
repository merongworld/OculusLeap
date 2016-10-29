using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public enum GrabMode
{
    Empty,
    LeftOnly,
    RightOnly,
    Both
}

public class BlockMGR : MonoBehaviour {

    List<GameObject> block_prefab;
    List<GameObject> blocks;
    GameObject hitObject;

    [Header("Reference")]
    public CapsuleHand left_hand;
    public CapsuleHand right_hand;

    private GrabMode grabMode = GrabMode.Empty;

    // Use this for initialization
    void Start()
    {
        block_prefab = new List<GameObject>(); 
        block_prefab.Add(Resources.Load("BookShelfBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("DirtBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("GrassBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("WoolBlock", typeof(GameObject)) as GameObject);

        blocks = new List<GameObject>();
    }

    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, Vector3.Normalize(camera.rotation * Vector3.forward));
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log(hitObject);
        }

        if (left_hand.GetLeapHand().GrabStrength > 0.9 && right_hand.GetLeapHand().GrabStrength < 0.9) grabMode = GrabMode.LeftOnly;
        else if (left_hand.GetLeapHand().GrabStrength < 0.9 && right_hand.GetLeapHand().GrabStrength > 0.9) grabMode = GrabMode.RightOnly;
        else if (left_hand.GetLeapHand().GrabStrength > 0.9 && right_hand.GetLeapHand().GrabStrength > 0.9) grabMode = GrabMode.Both;
        else grabMode = GrabMode.Empty;

        switch (grabMode)
        {
            case GrabMode.Empty:
                break;

            case GrabMode.LeftOnly:
                break;

            case GrabMode.RightOnly:
                Debug.Log("right");
                if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition)) < 0.15f)
                {
                    Vector3 newPosition = UnityVectorExtension.ToVector3(
                        right_hand.GetLeapHand().PalmPosition) + UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal * 0.03f);

                    Debug.Log("before : " + newPosition);


                    hitObject.transform.position = new Vector3(truncate(newPosition.x),
                           truncate(newPosition.y),
                           truncate(newPosition.z));

                    Debug.Log("after : " + hitObject.transform.position.x + " " +
                                       hitObject.transform.position.y + " " +
                                       hitObject.transform.position.z);
                }
                break;

            case GrabMode.Both:
                break;
        }
    }

    public void onClickEvent(int listnum) {
        GameObject obj = Instantiate(block_prefab[listnum], new Vector3(0, 0.3f, 0.5f), Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;
        blocks.Add(obj);
    }

    public float truncate(float target)
    {
        int temp = (int)(target * 20);
        return (float)(temp) / 20;
    }
}
