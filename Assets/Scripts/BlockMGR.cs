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

public class BlockMGR : MonoBehaviour
{
    [Header("Reference")]
    public CapsuleHand left_hand;
    public CapsuleHand right_hand;

    [Header("Panels")]
    public GameObject block_panel;
    public GameObject geometry_panel;
    public GameObject toggle_panel;
    public Text mount_panel;

    List<GameObject> block_prefab;
    //List<GameObject> blocks;
    GameObject hitObject;
    GameObject cubeAlphaObj;
    GameObject cubeCanvas;

    Toggle blockToggle;

    private int handcount;
    private string handtext;

    private GrabMode grabMode;

    // Timer to track focus
    public float timeToSelect;
    private float countdown;

    GameObject geometryTargetObj;

    // Use this for initialization
    void Start()
    {
        block_prefab = new List<GameObject>();
        block_prefab.Add(Resources.Load("BookShelfBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("DirtBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("GrassBlock", typeof(GameObject)) as GameObject);
        block_prefab.Add(Resources.Load("WoolBlock", typeof(GameObject)) as GameObject);

        block_panel.SetActive(false);
        geometry_panel.SetActive(false);
        toggle_panel.SetActive(false);

        grabMode = GrabMode.Empty;
        /*blockToggle = null;

        blocks = new List<GameObject>();
        */
    }

    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, Vector3.Normalize(camera.rotation * Vector3.forward));
        RaycastHit hit;

        mount_panel.text = setMountPanel();

        if (cubeCanvas != null && geometryTargetObj != null)
        {
            cubeCanvas.transform.rotation = Quaternion.identity;
            cubeCanvas.transform.position = geometryTargetObj.transform.position;
            cubeCanvas.transform.Translate(0, 0, -0.05f);
        }

        if (left_hand.isActiveAndEnabled && right_hand.isActiveAndEnabled)
        {
            if (left_hand.GetLeapHand().GrabStrength > 0.99 && right_hand.GetLeapHand().GrabStrength > 0.99) grabMode = GrabMode.Both;
            else if (left_hand.GetLeapHand().GrabStrength > 0.99) grabMode = GrabMode.LeftOnly;
            else if (right_hand.GetLeapHand().GrabStrength > 0.99) grabMode = GrabMode.RightOnly;
            else grabMode = GrabMode.Empty;
        }

        switch (grabMode)
        {
            case GrabMode.Empty:
                if (Physics.Raycast(ray, out hit)) // Hit
                {
                    if (hitObject == hit.collider.gameObject) // 같은 블럭
                    {
                        if (grabMode == GrabMode.Empty)
                            hitObject.transform.localScale = new Vector3(1f, 1f, 1f);

                        if (!cubeAlphaObj.activeSelf)
                        {
                            countdown = timeToSelect;
                            cubeAlphaObj.SetActive(true);
                        }
                        else
                        {
                            countdown -= Time.deltaTime;
                            if (countdown < 0.0f)
                            {
                                cubeCanvas.SetActive(true);
                                countdown = timeToSelect;
                            }
                        }
                    }
                    else if (hitObject != null) // 다른 블럭
                    {
                        if (blockToggle.isOn)
                        {
                            //nothing
                        }
                        else
                        {
                            cubeAlphaObj.SetActive(false);
                            cubeCanvas.SetActive(false);
                            cubeAlphaObj = null;
                            cubeCanvas = null;
                            blockToggle = null;
                            hitObject = hit.collider.gameObject;
                            cubeAlphaObj = hitObject.transform.GetChild(6).gameObject;
                            cubeCanvas = hitObject.transform.GetChild(7).gameObject;
                            blockToggle = cubeCanvas.transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
                        }
                    }
                    else // null 블럭
                    {
                        hitObject = hit.collider.gameObject;
                        cubeAlphaObj = hitObject.transform.GetChild(6).gameObject;
                        cubeCanvas = hitObject.transform.GetChild(7).gameObject;
                        blockToggle = cubeCanvas.transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
                    }
                }
                else // no Hit
                {
                    hitObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    if (hitObject != null) // previous hit block exist
                    {
                        if (blockToggle.isOn)
                        {
                            //nothing
                        }
                        else
                        {
                            //hitObject = null, effect = false&null
                            hitObject = null;
                            cubeAlphaObj.SetActive(false);
                            cubeCanvas.SetActive(false);
                            cubeAlphaObj = null;
                            cubeCanvas = null;
                        }
                    }
                    else
                    {
                    }
                }
                break;

            case GrabMode.LeftOnly:
                Debug.Log("left");
                if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(left_hand.GetLeapHand().Fingers[0].TipPosition)) < 0.15f && !blockToggle.isOn)
                {
                    hitObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    Vector3 newPosition = UnityVectorExtension.ToVector3(left_hand.GetLeapHand().Fingers[0].TipPosition);

                    hitObject.transform.position = new Vector3(truncate(newPosition.x), truncate(newPosition.y), truncate(newPosition.z));
                }
                break;

            case GrabMode.RightOnly:
                Debug.Log("right");
                if (Vector3.Distance(hitObject.gameObject.transform.position, UnityVectorExtension.ToVector3(right_hand.GetLeapHand().Fingers[0].TipPosition)) < 0.15f && !blockToggle.isOn)
                {
                    hitObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    Vector3 newPosition = UnityVectorExtension.ToVector3(right_hand.GetLeapHand().Fingers[0].TipPosition);

                    hitObject.transform.position = new Vector3(truncate(newPosition.x), truncate(newPosition.y), truncate(newPosition.z));
                }
                break;

            case GrabMode.Both:
                if (Vector3.Dot(UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmNormal), Vector3.up) > 0 && Vector3.Dot(UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal), Vector3.up) > 0)
                {
                    block_panel.SetActive(true);
                }
                //float palm_distance = Vector3.Distance(UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmPosition), UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmPosition));
                //hitObject.transform.localScale = new Vector3(truncate(palm_distance), truncate(palm_distance), truncate(palm_distance));
                break;
        }
    }

    public void onClickEvent(int listnum)
    { 
        toggle_panel.SetActive(true);
        GameObject blockobj = Instantiate(block_prefab[listnum], new Vector3(0, block_panel.transform.position.y - 0.2f, block_panel.transform.position.z - 0.1f), Quaternion.identity) as GameObject;
        GameObject toggleobj = Instantiate(GameObject.Find("Toggle Canvas"), new Vector3(0, block_panel.transform.position.y - 0.2f, block_panel.transform.position.y - 0.15f), Quaternion.identity) as GameObject;
        toggle_panel.SetActive(false);

        toggleobj.transform.parent = blockobj.transform;
        toggleobj.SetActive(false);
    }

    public void onToggleButtonClick()
    {
        geometryTargetObj = cubeCanvas.transform.parent.gameObject;
        if (blockToggle.isOn)
            geometry_panel.SetActive(true);
        else
        {
            geometryTargetObj = null;
            geometry_panel.SetActive(false);
        }
    }

    public void onXButtonClick()
    {
        geometryTargetObj.transform.Rotate(90, 0, 0);
    }

    public void onYButtonClick()
    {
        geometryTargetObj.transform.Rotate(0, 90, 0);
    }

    public void onZButtonClick()
    {
        geometryTargetObj.transform.Rotate(0, 0, 90);
    }

    public void onDeleteButtonClick()
    {
        Destroy(geometryTargetObj);
        geometryTargetObj.SetActive(false);
        geometryTargetObj = null;
        geometry_panel.SetActive(false);
    }

    public void onCompleteButtonClick()
    {
        blockToggle.isOn = false;
        geometryTargetObj.SetActive(false);
        geometryTargetObj = null;
        geometry_panel.SetActive(false);
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

