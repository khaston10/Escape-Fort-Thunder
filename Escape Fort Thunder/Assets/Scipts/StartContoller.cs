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
        StartGame, CreatFort, EnterSeed
    }

    SelectionSate currentSelectionState;
    int selctionIndex = 2;

    public Button[] startGameButtons;
    public Button[] createFortButtons;
    public Button[] seedButtons;


    List<Button[]> selectionButtons = new List<Button[]>();

    #endregion

    #region Variables - Panels and Text

    public GameObject newGamePanel;
    MapCreator mapCreator;
    string[] map;

    public GameObject seedPanel;
    public Text seed1000sText;
    public Text seed100sText;
    public Text seed10sText;
    public Text seed1sText;
    readonly string acceptableSeedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int[] seedIndices = new int[] {0, 0, 0, 0};
    string seedString = "0000";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentSelectionState = SelectionSate.StartGame;

        // Initialize the selectionButtons array.
        selectionButtons.Add(startGameButtons);
        selectionButtons.Add(createFortButtons);
        selectionButtons.Add(seedButtons);
    }

    // Update is called once per frame
    void Update()
    {
        // Get Keyboard Inputs (This is for developement and can be removed fo mobile release)
        PressKeyboardKey();
    }

    #region Functions - Buttons - StartGame

    void PressNewGame()
    {
        newGamePanel.SetActive(true);
        selctionIndex = 1;
        currentSelectionState = SelectionSate.CreatFort;
        SelectCurrentButton();
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

    void PressStart()
    {
        CreateNewMap();
    }

    void PressSeed()
    {
        currentSelectionState = SelectionSate.EnterSeed;
        selctionIndex = 0;
        newGamePanel.SetActive(false);
        seedPanel.SetActive(true);
        SelectCurrentButton();
    }

    void BackToStartGameState()
    {
        currentSelectionState = SelectionSate.StartGame;
        selctionIndex = 2;
        SelectCurrentButton();
        newGamePanel.SetActive(false);

    }

    #endregion

    #region Functions - Buttons - Enter Seed

    void UpdateSeedText()
    {
        seed1000sText.text = acceptableSeedChars[seedIndices[0]].ToString();
        seed100sText.text = acceptableSeedChars[seedIndices[1]].ToString();
        seed10sText.text = acceptableSeedChars[seedIndices[2]].ToString();
        seed1sText.text = acceptableSeedChars[seedIndices[3]].ToString();
    }

    void PressSeedButton()
    {
        if (seedIndices[selctionIndex] < acceptableSeedChars.Length - 1) seedIndices[selctionIndex] += 1;
        else seedIndices[selctionIndex] = 0;
        UpdateSeedText();
        SelectCurrentButton();
    }

    void BackToCreateFortState()
    {
        currentSelectionState = SelectionSate.CreatFort;
        selctionIndex = 0;
        SelectCurrentButton();
        seedPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    void StartFromSeed()
    {
        seedString = "";
        for (int i = 0; i < seedIndices.Length; i++)
        {
            seedString += acceptableSeedChars[seedIndices[i]].ToString();
        }

        CreateNewMap();
    }

    #endregion

    #region Functions - Main Controller

    public void SelectCurrentButton()
    {
        selectionButtons[((int)currentSelectionState)][selctionIndex].Select();
    }

    public void PressKeyboardKey()
    {
        // (This is for developement and can be removed fo mobile release)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PressUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PressDown();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PressLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PressRight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PressA();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PressB();
        }
    }

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
            if (selctionIndex == 0) PressSeed();
            else if (selctionIndex == 1) PressStart();

            if (selctionIndex != 0)
            {
                SelectCurrentButton();
            }
            
        }

        else if (currentSelectionState == SelectionSate.EnterSeed)
        {
            if (selctionIndex == 4) StartFromSeed();
            else PressSeedButton();
        }
    }

    public void PressB()
    {
        if (currentSelectionState == SelectionSate.CreatFort)
        {
            BackToStartGameState();
        }

        else if (currentSelectionState == SelectionSate.EnterSeed)
        {
            BackToCreateFortState();
        }
    }

    public void PressUp()
    {
        if (currentSelectionState == SelectionSate.StartGame || currentSelectionState == SelectionSate.CreatFort)
        {
            IncreaseDecreaseSelectionIndex(true);
            SelectCurrentButton();

        }

        SelectCurrentButton();
    }

    public void PressDown()
    {

        if (currentSelectionState == SelectionSate.StartGame || currentSelectionState == SelectionSate.CreatFort)
        {
            IncreaseDecreaseSelectionIndex(false);
            SelectCurrentButton();
        }

        SelectCurrentButton();
    }

    public void PressLeft()
    {
        if (currentSelectionState == SelectionSate.EnterSeed)
        {
            IncreaseDecreaseSelectionIndex(false);
            SelectCurrentButton();
        }

        SelectCurrentButton();

    }

    public void PressRight()
    {
        if (currentSelectionState == SelectionSate.EnterSeed)
        {
            IncreaseDecreaseSelectionIndex(true);
            SelectCurrentButton();
        }

        SelectCurrentButton();
    }

    #endregion

    #region Functions - Map Generation

    void CreateNewMap()
    {
        // Create custom object - Map Creator.
        mapCreator = new MapCreator("Assets/Save/Map.txt", seedString);

        // Write the map to save file.
        mapCreator.CreateMap();
    }

    #endregion

}
