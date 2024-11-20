using System;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject menuButtonPrefab;
    public Transform menuButtonParent;
    private void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        PopulateMenuButtons();
    }

    private void PopulateMenuButtons()
    {
        foreach (var menu in menuList)
        {
            GameObject buttonObj = Instantiate(menuButtonPrefab, menuButtonParent);
            MenuButton menuButton = buttonObj.GetComponent<MenuButton>();
            menuButton.Setup(menu.menuName, menu.minutes, menu.seconds);
        }
    }
}
