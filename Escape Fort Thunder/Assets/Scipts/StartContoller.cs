using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StartContoller : MonoBehaviour
{
    #region Variables - Buttons
    enum SelectionSate : int
    {
        StartGame, CreatFort,
    }

    SelectionSate currentSelectionState;
    int selctionIndex = 2;

    public Button[] startGameButtons;
    public Button[] createFortButtons;


    List<Button[]> selectionButtons = new List<Button[]>();

    #endregion

    #region Variables - Panels and Text

    public GameObject newGamePanel;
    public Text newGameDescriptionText;
    public Text newGameValueText;
    MapCreator mapCreator;
    string[] map;
    int fortSize = 500;
    int fortHostility = 5;
    int lootPercentage = 10;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentSelectionState = SelectionSate.StartGame;

        // Initialize the selectionButtons array.
        selectionButtons.Add(startGameButtons);
        selectionButtons.Add(createFortButtons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Functions - Buttons - StartGame

    void PressNewGame()
    {
        newGamePanel.SetActive(true);
        selctionIndex = 3;
        currentSelectionState = SelectionSate.CreatFort;
        selectionButtons[((int)currentSelectionState)][selctionIndex].Select();
        UpdateCreateFortText();
    }

    void PressLoadGame()
    {
        print("Load Game");
    }

    void PressQuitGame()
    {
        print("Quit Game");
    }

    #endregion

    #region Functions - Buttons - CreateFort

    void PressFortSize()
    {
        if (fortSize < 1000) fortSize += 100;
        else fortSize = 500;
        UpdateCreateFortText();
    }

    void PressFortHostility()
    {
        if (fortHostility < 10) fortHostility += 1;
        else fortHostility = 1;
        UpdateCreateFortText();
    }

    void PressLootPercentage()
    {
        if (lootPercentage < 100) lootPercentage += 10;
        else lootPercentage = 10;
        UpdateCreateFortText();
    }

    void PressStart()
    {
        print("Creating Map...");
        CreateNewMap();
    }

    void UpdateCreateFortText()
    {
        if (selctionIndex == 3)
        {
            newGameDescriptionText.text = "Fort Size";
            newGameValueText.text = fortSize.ToString();
        }

        else if (selctionIndex == 2)
        {
            newGameDescriptionText.text = "Fort Hostility";
            newGameValueText.text = fortHostility.ToString();
        }

        else if (selctionIndex == 1)
        {
            newGameDescriptionText.text = "Loot Percentage";
            newGameValueText.text = lootPercentage.ToString() + "%";
        }

        else if (selctionIndex == 0)
        {
            newGameDescriptionText.text = "Start";
            newGameValueText.text = "";
        }


    }

    void BackToStartGameState()
    {
        currentSelectionState = SelectionSate.StartGame;
        selctionIndex = 2;
        selectionButtons[((int)currentSelectionState)][selctionIndex].Select();
        newGamePanel.SetActive(false);

    }

    #endregion

    #region Functions - Main Controller

    void IncreaseDecreaseSelectionIndex(bool forward)
    {

        // If the selection index needs to increase.
        if (forward)
        {
            if (selctionIndex < selectionButtons[((int)currentSelectionState)].Length - 1)
            {
                selctionIndex += 1;
            }
        }

        // If the selection index needs to decrease.
        else
        {
            if (selctionIndex > 0)
            {
                selctionIndex -= 1;
            }
        }

    }

    public void PressA()
    {
        if (currentSelectionState == SelectionSate.StartGame)
        {
            if (selctionIndex == 0) PressQuitGame();
            else if (selctionIndex == 1) PressLoadGame();
            else if (selctionIndex == 2) PressNewGame();
        }

        else if (currentSelectionState == SelectionSate.CreatFort)
        {
            if (selctionIndex == 0) PressStart();
            else if (selctionIndex == 1) PressLootPercentage();
            else if (selctionIndex == 2) PressFortHostility();
            else if (selctionIndex == 3) PressFortSize();
            UpdateCreateFortText();
            selectionButtons[((int)currentSelectionState)][selctionIndex].Select();
        }
    }

    public void PressB()
    {
        if (currentSelectionState == SelectionSate.CreatFort)
        {
            BackToStartGameState();
        }
    }

    public void PressUp()
    {
        IncreaseDecreaseSelectionIndex(true);
        selectionButtons[((int)currentSelectionState)][selctionIndex].Select();

        if (currentSelectionState == SelectionSate.CreatFort)
        {
            UpdateCreateFortText();
        }
    }

    public void PressDown()
    {
        IncreaseDecreaseSelectionIndex(false);
        selectionButtons[((int)currentSelectionState)][selctionIndex].Select();

        if (currentSelectionState == SelectionSate.CreatFort)
        {
            UpdateCreateFortText();
        }
    }

    #endregion

    #region Functions - Map Generation

    void CreateNewMap()
    {
        // Create custom object - Map Creator.
        mapCreator = new MapCreator(fortSize, fortHostility, lootPercentage, "Assets/Save/Map.txt");

        // Write the map to save file.
        mapCreator.CreateMap();
    }

    #endregion

}
