//
//  GameController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BlockType
{
    None,
    Grass,
    Dirt,
    Wool,
    Brick,
    Bookshelf,
    Pumpkin,
    Hay,
    Chest,
    Tnt
}

public enum MenuState
{
    None,
    Main,
    Add,
    Edit,
    Settings
}

public enum ActionState
{
    None,
    UI,
    Attach,
    Move,
    Pick
}

public class GameController : MonoBehaviour
{
    private MenuState menuState;
    private ActionState actionState;

    private BlockType pickedBlockType;
    private GameObject pickedBlock;

    private bool didMenuChange;
    private float elapsedTime;
    private int blockCount;

    private Camera mainCamera;
    private GameObjectController gameObjectController;
    private StatePanelController statePanelController;

    private GameObject raycastPoint;
    private GameObject leapEventSystem;
    private GameObject canvas;
    private GameObject mainPanel;
    private GameObject settingsPanel;
    private GameObject addPanel;
    private GameObject editPanel;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.None;
        actionState = ActionState.None;

        pickedBlockType = BlockType.None;
        pickedBlock = null;

        didMenuChange = false;
        elapsedTime = 0.0f;
        blockCount = 0;

        mainCamera = Camera.main;
        gameObjectController = FindObjectOfType<GameObjectController>();
        statePanelController = FindObjectOfType<StatePanelController>();

        raycastPoint = GameObject.Find("RaycastPoint");
        leapEventSystem = GameObject.Find("LeapEventSystem");
        canvas = GameObject.Find("Canvas");
        mainPanel = GameObject.Find("MainPanel");
        settingsPanel = GameObject.Find("SettingsPanel");
        addPanel = GameObject.Find("AddPanel");
        editPanel = GameObject.Find("EditPanel");

        UpdateBackgroundColor(0.125f, 0.125f, 0.125f);

        raycastPoint.SetActive(false);
        canvas.SetActive(false);
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        addPanel.SetActive(false);
        editPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (didMenuChange)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 0.15f)
            {
                didMenuChange = false;
                elapsedTime = 0.0f;
                leapEventSystem.SetActive(true);
            }
        }

        switch (actionState)
        {
            case ActionState.Pick:
                HandlePickState();
                break;

            default:
                // Do nothing
                break;
        }
    }

    public MenuState MenuState
    {
        get { return menuState; }
        set
        {
            if (menuState != value) { UpdateMenuState(value); }
        }
    }

    public ActionState ActionState
    {
        get { return actionState; }
        set
        {
            if (actionState != value) { UpdateActionState(value); }
        }
    }

    public BlockType PickedBlockType
    {
        get { return pickedBlockType; }
        set { pickedBlockType = value; }
    }

    public GameObject PickedBlock
    {
        get { return pickedBlock; }
        set { pickedBlock = value; }
    }

    public int BlockCount
    {
        get { return blockCount; }
        set { blockCount = (value < 0) ? 0 : value; }
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    public GameObject RaycastPoint
    {
        get { return raycastPoint; }
    }

    private void HandlePickState()
    {
        if (menuState == MenuState.Edit)
        {
            GameObject blocks = GameObject.Find("Blocks");
            if (blockCount == 0 || blocks == null) { return; }

            Transform mainCameraT = mainCamera.transform;
            Ray ray = new Ray(mainCameraT.position,
                Vector3.Normalize(mainCameraT.rotation * Vector3.forward));
            RaycastHit hit;

            raycastPoint.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitGameObject = hit.collider.gameObject;
                List<GameObject> blocksChildren = gameObjectController.GetChildrenList(blocks);

                GameObject searchResult = null;
                foreach (GameObject blocksChild in blocksChildren)
                {
                    if (hitGameObject == blocksChild)
                    {
                        searchResult = blocksChild;
                        break;
                    }
                }

                if (searchResult != null)
                {
                    if (pickedBlock == null)
                    {
                        Dictionary<string, GameObject> hitGameObjectChildren =
                            gameObjectController.GetChildren(hitGameObject);
                        hitGameObjectChildren["Cube"].SetActive(true);

                        PickedBlock = searchResult;
                        elapsedTime = 0.0f;
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime;

                        if (elapsedTime > 2.0f)
                        {
                            ActionState = ActionState.UI;
                            raycastPoint.SetActive(false);
                            Dictionary<string, GameObject> pickedBlockChildren =
                                gameObjectController.GetChildren(pickedBlock);
                            pickedBlockChildren["Cube"].SetActive(false);
                            return;
                        }
                    }

                    Vector3 target = mainCameraT.position;
                    Vector3 hitPosition = hitGameObject.transform.position;
                    float distance = Vector3.Distance(target, hitPosition);

                    if (distance < 0.5)
                    {
                        float newZ = distance - 0.09f;
                        if (newZ < 0) { newZ = 0.0f; }

                        raycastPoint.transform.localPosition = new Vector3(0.0f, 0.0f, newZ);
                    }
                }
            }
            else if (pickedBlock != null)
            {
                Dictionary<string, GameObject> pickedBlockChildren =
                        gameObjectController.GetChildren(pickedBlock);
                pickedBlockChildren["Cube"].SetActive(false);
                PickedBlock = null;
                elapsedTime = 0.0f;
            }
        }
    }

    private void UpdateMenuState(MenuState newMenuState)
    {
        switch (menuState)
        {
            case MenuState.Main:
                mainPanel.SetActive(false);
                break;

            case MenuState.Settings:
                settingsPanel.SetActive(false);
                break;

            case MenuState.Add:
                addPanel.SetActive(false);
                break;

            case MenuState.Edit:
                editPanel.SetActive(false);
                break;
        }

        if (newMenuState != MenuState.None)
        {
            leapEventSystem.SetActive(false);
            didMenuChange = true;
        }

        switch (newMenuState)
        {
            case MenuState.None:
                canvas.SetActive(false);
                ActionState = ActionState.None;
                break;

            case MenuState.Main:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("MAIN MENU");
                mainPanel.SetActive(true);
                ActionState = ActionState.UI;
                PickedBlock = null;
                PickedBlockType = BlockType.None;
                break;

            case MenuState.Settings:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("SETTINGS");
                settingsPanel.SetActive(true);
                ActionState = ActionState.UI;
                break;

            case MenuState.Add:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("ADD BLOCK");
                addPanel.SetActive(true);
                ActionState = ActionState.UI;
                break;

            case MenuState.Edit:
                raycastPoint.SetActive(true);
                canvas.SetActive(false);
                statePanelController.UpdateTitleText("EDIT BLOCK");
                editPanel.SetActive(true);
                ActionState = ActionState.Pick;
                break;
        }

        menuState = newMenuState;
    }

    private void UpdateActionState(ActionState newActionState)
    {
        // Debug.Log(newActionState);

        switch (newActionState)
        {
            case ActionState.None:
                break;

            case ActionState.Attach:
                if (menuState == MenuState.Add) { canvas.SetActive(false); }
                break;

            case ActionState.UI:
                if (menuState == MenuState.Edit) { canvas.SetActive(true); }
                break;
        }

        actionState = newActionState;
    }

    public void UpdateBackgroundColor(float r, float g, float b)
    {
        mainCamera.backgroundColor = new Color(r, g, b);
    }
}
