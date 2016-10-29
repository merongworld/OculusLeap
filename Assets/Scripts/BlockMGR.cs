using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BlockMGR : MonoBehaviour {

    List<GameObject> block_prefab;
    List<GameObject> blocks;

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

    public void onClickEvent(int listnum) {
        GameObject obj = Instantiate(block_prefab[listnum], new Vector3(0, 0.8f, 0.5f), Quaternion.identity) as GameObject;
        blocks.Add(obj);
    }
}
