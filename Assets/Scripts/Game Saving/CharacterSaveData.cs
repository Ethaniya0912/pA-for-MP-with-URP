using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// 해당 데이터를 모든 세이브 파일에서 레퍼하려하기 떄문에, 모노비헤비어가 아니며 serealizable임
public class CharacterSaveData
{
    [Header("Character Name")]
    public string characterName;

    [Header("Time Played")]
    public float secondsPlayed;

    // 기본적인 변수만 저장 가능하기에
    // Vector3 같은 고기능 변수는 저장불가
    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
