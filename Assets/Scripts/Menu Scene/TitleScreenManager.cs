using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject titleScreenMainMenu;
    [SerializeField] GameObject titleScreenLoadMenu;

    [Header("Buttons")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuLoadGameButton;

    public void StartNetworkAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    public void StartNewGame()
    {
        WorldSaveGameManager.Instance.NewGame();
        StartCoroutine(WorldSaveGameManager.Instance.LoadWorldScene());
    }

    public void OpenLoadGameMenu()
    {
        // 메인메뉴 닫기
        titleScreenLoadMenu.SetActive(false);

        // 로딩 메뉴 열기
        titleScreenLoadMenu.SetActive(true); 

        // 리턴 슬롯을 찾고 자동 셀렉트하기.
        loadMenuReturnButton.Select();
    }

    public void CloseLoadGameMenu()
    {
        // 로드 메뉴 닫기.
        titleScreenLoadMenu.SetActive(false);

        // 메인 메뉴 열기.
        titleScreenMainMenu.SetActive(true);

        // 로드 버튼 고르기.
        mainMenuLoadGameButton.Select();
    }
}
