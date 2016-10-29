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
    GameObject childObj;

    private int handcount;
    private string handtext;

    [Header("Reference")]
    public CapsuleHand left_hand;
    public CapsuleHand right_hand;

    [Header("Panels")]
    public GameObject block_panel;
    public GameObject geometry_panel;
    public GameObject toggle_panel;
    public Text mount_panel;

    private GrabMode grabMode;

    // Use this for initialization
    void Start()
    {
        grabMode = GrabMode.Empty;

        block_panel.SetActive(false);
        geometry_panel.SetActive(false);
        toggle_panel.SetActive(false);

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

        mount_panel.text = setMountPanel();

        Vector3 upNormal = new Vector3(0, 1, 0);
        if (Vector3.Dot(UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmNormal), upNormal) > 0 && Vector3.Dot(UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal), upNormal) > 0 && right_hand.GetLeapHand().GrabStrength > 0.9)
        {
            block_panel.SetActive(true);
        }
        else if (left_hand.GetLeapHand().GrabStrength > 0.9)
        {
            geometry_panel.SetActive(true);
        }

        if (left_hand.GetLeapHand().GrabStrength > 0.95 && right_hand.GetLeapHand().GrabStrength > 0.95) grabMode = GrabMode.Both;
        else if (left_hand.GetLeapHand().GrabStrength > 0.95) grabMode = GrabMode.LeftOnly;
        else if (right_hand.GetLeapHand().GrabStrength > 0.95) grabMode = GrabMode.RightOnly;
        else grabMode = GrabMode.Empty;

        switch (grabMode)
        {
            case GrabMode.Empty:
                if (Physics.Raycast(ray, out hit))
                {
                    if (hitObject != hit.collider.gameObject && hitObject != null)
                    {
                        hitObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        childObj.SetActive(false);
                        childObj = null;
                    }
                    hitObject = hit.collider.gameObject;
                    hitObject.transform.localScale = new Vector3(1f, 1f, 1f);

                    childObj = hitObject.transform.GetChild(6).gameObject;
                    if (childObj.name == "Cube")
                        childObj.SetActive(true);
                }
                else if(hitObject != null)
                {
                    hitObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    childObj.SetActive(false);
                    hitObject = null;
                }

                break;

            case GrabMode.LeftOnly:
                Debug.Log("left");
                if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmPosition)) < 0.11f)
                {
                    hitObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    Vector3 newPosition = UnityVectorExtension.ToVector3(
                        left_hand.GetLeapHand().PalmPosition) + UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmNormal * 0.03f);

                    hitObject.transform.position = new Vector3(truncate(newPosition.x), truncate(newPosition.y), truncate(newPosition.z));
                }
                break;

            case GrabMode.RightOnly:
                Debug.Log("right");
                if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition)) < 0.11f)
                {
                    hitObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    Vector3 newPosition = UnityVectorExtension.ToVector3(
                        right_hand.GetLeapHand().PalmPosition) + UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal * 0.03f);

                    hitObject.transform.position = new Vector3(truncate(newPosition.x), truncate(newPosition.y), truncate(newPosition.z));
                }
                break;

            case GrabMode.Both:
                //float palm_distance = Vector3.Distance(UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmPosition), UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition));
                //hitObject.transform.localScale = new Vector3(truncate(palm_distance), truncate(palm_distance), truncate(palm_distance));             
                break;
        }
    }

    public void onClickEvent(int listnum) {
        toggle_panel.SetActive(true);
        GameObject obj = Instantiate(block_prefab[listnum], new Vector3(0, 0.3f, 0.5f), Quaternion.identity) as GameObject;
        GameObject toggleobj = Instantiate(GameObject.Find("Toggle Canvas"), new Vector3(0, 0.3f, 0.45f), Quaternion.identity) as GameObject;
        toggleobj.transform.parent = obj.transform;
        toggle_panel.SetActive(false);

        blocks.Add(obj);
    }

    public float truncate(float target)
    {
        int temp = (int)(target * 20);
        return (float)(temp) / 20;
    }
    
    string setMountPanel()
    {
        handcount = 0;
        if (left_hand.isActiveAndEnabled)
            handcount += 1;
        if (right_hand.isActiveAndEnabled)
            handcount += 1;

        switch (handcount)
        {
            case 0:
                handtext = "Empty";
                break;
            case 1:
                if (left_hand.isActiveAndEnabled)
                    handtext = "Left";
                if (right_hand.isActiveAndEnabled)
                    handtext = "Right";
                break;
            case 2:
                handtext = "Both";
                break;
        }

        return "Hand(" + handcount + ") / " + handtext;
    }
}
