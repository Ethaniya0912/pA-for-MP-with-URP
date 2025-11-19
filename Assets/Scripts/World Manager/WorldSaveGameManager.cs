using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    public static WorldSaveGameManager Instance { get; set; }

    [Header("Save/Load")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    [Header("World Scene Index")]
    [SerializeField] int worldSceneIndex = 1;

    [Header("Save Data Writer")]
    private SaveFileDataWriter saveFileDataWriter;

    [Header("Current Character Data")]
    public CharacterSlots currentCharacterSlotBeingUsed;
    public CharacterSaveData currentCharacterData;
    private string saveFileName;

    [Header("Character Slots")]
    public CharacterSaveData characterSlots01;
    public CharacterSaveData characterSlots02;
    public CharacterSaveData characterSlots03;
    public CharacterSaveData characterSlots04;
    public CharacterSaveData characterSlots05;

    private void Awake()
    {
        // 해당 스크립트의 인스턴스는 하나만 존재할 수 있으며, 다른게 존재시 파괴.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadAllCharacterProfiles();
    }

    private void Update()
    {
        if (saveGame)
        {
            saveGame = false;
            SaveGame();
        }

        if (loadGame)
        {
            loadGame = false;
            LoadGame();
        }
    }

    public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots characterSlots)
    {
        string fileName = "";
        switch (characterSlots)
        {
            case CharacterSlots.CharacterSlots_01:
                fileName = "characterSlots_01";
                break;
            case CharacterSlots.CharacterSlots_02:
                fileName = "characterSlots_02";
                break;
            case CharacterSlots.CharacterSlots_03:
                fileName = "characterSlots_03";
                break;
            case CharacterSlots.CharacterSlots_04:
                fileName = "characterSlots_04";
                break;
            case CharacterSlots.CharacterSlots_05:
                fileName = "characterSlots_05";
                break;
            default:
                break;
        }

        return fileName;
    }

    public void NewGame()
    {
        // 새 파일을 만듬, 어떤 슬롯을 사용하는 지에 따라 파일 이름이 달림.
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        currentCharacterData = new CharacterSaveData();

    }

    public void LoadGame()
    {
        // 이전 파일을 부름, 어떤 슬롯을 사용하는지에 따라 파일이름이 갈림.
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        saveFileDataWriter = new SaveFileDataWriter();
        // 일반적으로 다양한 기계 타입에 작동됨(Application.persistentDataPath)
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFilename = saveFileName;
        currentCharacterData = saveFileDataWriter.LoadSaveFile();

        StartCoroutine(LoadWorldScene());
    }

    public void SaveGame()
    {
        // 현재 파일을 저장함, 어떤 슬롯을 이용하고 있는지 파일이름에 따라.
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFilename = saveFileName;

        // 플레이어인포를 넘겨주고, 세이브파일화함.
        player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

        // saveFileDataWriter에 createCharacterSaveFile 을 불러오고, currentCharacterData를 넘김
        saveFileDataWriter.CreateCharacterSaveFile(currentCharacterData);
    }

    // 게임을 시작하면 기계에 있는 모든 캐릭터 프로필을 불러오기
    private void LoadAllCharacterProfiles()
    {
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

        saveFileDataWriter.saveFilename = 
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlots_01);
        characterSlots01 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFilename =
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlots_02);
        characterSlots02 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFilename =
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlots_03);
        characterSlots03 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFilename =
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlots_04);
        characterSlots04 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFilename =
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlots_05);
        characterSlots05 = saveFileDataWriter.LoadSaveFile();
    }

    // 코루틴역할을 하는 IEnumerator 를 사용.
    public IEnumerator LoadWorldScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
