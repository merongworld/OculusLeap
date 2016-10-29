using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class BlockMGR : MonoBehaviour {

    List<GameObject> block_prefab;
    List<GameObject> blocks;
    GameObject hitObject;

    [Header("Reference")]
    public CapsuleHand left_hand;
    public CapsuleHand right_hand;

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
        }

        if (left_hand.GetLeapHand().GrabStrength > 0.9 || right_hand.GetLeapHand().GrabStrength > 0.9)
        {
            if(hitObject.gameObject.GetComponent<SphereCollider>() == null) { 
                hitObject.gameObject.AddComponent<SphereCollider>();
                Destroy(hitObject.gameObject.GetComponent<BoxCollider>());
            }
            if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition)) < 0.1f) { 
                hitObject.transform.position = UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition) + UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal * 0.06f);
            }
        }
    }

    public void onClickEvent(int listnum) {
        GameObject obj = Instantiate(block_prefab[listnum], new Vector3(0, 0.8f, 0.5f), Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;
        blocks.Add(obj);
    }
}
