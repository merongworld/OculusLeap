using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EventManager : MonoBehaviour {

    [Header("References")]
    public GameObject prefab;
    // Use this for initialization
    void Start()
    {
    }

    void Update() {
    }

    public void onClickEvent() {    
        GameObject obj = Instantiate(prefab, new Vector3(0, 0.3f, 0.3f), Quaternion.identity) as GameObject;
    }
}
