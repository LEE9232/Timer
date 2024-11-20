using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuListManager : MonoBehaviour
{
    public static MenuListManager Instance { get; private set; }
    [Serializable]
    public class MenuData
    {
        public string menuName;
        public int minutes;
        public int seconds;
    }
    public List<MenuData> menuList = new List<MenuData>();
    public GameObject menuBtnPrefab;
    public Button menuBtn;
    public Transform menuButtonParent;
    public GameObject menuPanel;
    private bool menuVisible = false;

    private void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        menuBtn.onClick.AddListener(MenuListBtnClick);


    }
    private void Start()
    {
        PopulateMenuButtons();
    }

    private void PopulateMenuButtons()
    {
        foreach (var menu in menuList)
        {
            GameObject buttonObj = Instantiate(menuBtnPrefab, menuButtonParent);
            MenuButton menuButton = buttonObj.GetComponent<MenuButton>();
            menuButton.Setup(menu.menuName, menu.minutes, menu.seconds);
        }
        menuPanel.SetActive(false);
    }
    private void MenuListBtnClick()
    {
        menuVisible = !menuVisible;
        menuPanel.SetActive(menuVisible);
    }
}
