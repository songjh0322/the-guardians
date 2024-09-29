using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

// JSON 파일에서 유닛 목록을 불러오기 위한 클래스 (이곳에서만 사용)
[Serializable]
public class UnitStatsList
{
    public BasicStats[] units;  // BasicStats 배열
}

public class UnitManager
{
    public static UnitManager Instance { get; private set; }
    GameManager gameManager = GameManager.Instance;

    public const int maxUnits = 6;      // 최대 유닛 선택 수

    public Dictionary<string, BasicStats> guwol_basicStatsData;  // [구월산 목단설] 기본 능력치 딕셔너리
    public Dictionary<string, BasicStats> seo_basicStatsData;    // [서씨 가문] 기본 능력치 딕셔너리

    public List<Unit> player1Units = new List<Unit>();      // Player1이 인게임에서 사용할 유닛 리스트 (참조)
    public List<Unit> player2Units = new List<Unit>();      // Player2가 인게임에서 사용할 유닛 리스트 (참조)

    // 싱글톤 인스턴스 설정
    public UnitManager()
    {
        Debug.Log("UnitManager 생성 시도");

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("UnitManager가 없으므로 생성함");
        }
        else
            Debug.Log("UnitManager가 이미 있음");
    }

    // JSON 데이터를 불러오고 딕셔너리에 저장하는 메서드
    public void LoadBasicStatsFromJSON()
    {
        Debug.Log("LoadBasicStatsFromJSON 진입");
        TextAsset jsonTextFile1 = Resources.Load<TextAsset>("GuwolStats");
        TextAsset jsonTextFile2 = Resources.Load<TextAsset>("SeoStats");

        if (jsonTextFile1 != null && jsonTextFile2 != null)
        {
            UnitStatsList statsList1 = JsonUtility.FromJson<UnitStatsList>(jsonTextFile1.text);
            UnitStatsList statsList2 = JsonUtility.FromJson<UnitStatsList>(jsonTextFile2.text);

            guwol_basicStatsData = new Dictionary<string, BasicStats>();
            seo_basicStatsData = new Dictionary<string, BasicStats>();

            // JSON 데이터를 딕셔너리로 변환하여 저장
            foreach (BasicStats stats in statsList1.units)
            {
                guwol_basicStatsData[stats.unitName] = stats;
            }
            foreach (BasicStats stats in statsList2.units)
            {
                seo_basicStatsData[stats.unitName] = stats;
            }

            Debug.Log("성공적으로 JSON 파일을 로드했습니다.");
        }
        else
        {
            Debug.LogError("CharacterStats.json 파일을 찾을 수 없습니다.");
        }
    }

    // 사용 : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?] -> [예] 버튼 클릭 시 호출
    // 파라미터 : UI에서 선택한 유닛들의 이름을 담은 List
    // 기능 : Player1이 사용할 유닛들을 최종 결정
    public void ConfirmPlayer1Units(List<string> unitNames)
    {
        if (gameManager.player1Camp == Player1Camp.Guwol)
            for (int i = 0; i < unitNames.Count; i++)
                player1Units.Add(new Unit(unitNames[i], guwol_basicStatsData));
        else if (gameManager.player1Camp == Player1Camp.Seo)
            for (int i = 0; i < unitNames.Count; i++)
                player1Units.Add(new Unit(unitNames[i], seo_basicStatsData));
    }

    // 현재 미사용 함수
    public void ConfirmPlayer2Units()
    {
        List<string> randomCharacterNames;

        if (gameManager.player1Camp == Player1Camp.Guwol)
        {
            // 딕셔너리에서 랜덤으로 6개의 유닛 이름을 가져옴
            randomCharacterNames = seo_basicStatsData.Keys
                .OrderBy(x => UnityEngine.Random.Range(0, seo_basicStatsData.Count))
                .Take(6)
                .ToList();

            // 가져온 이름으로 유닛을 생성하여 player2Units 리스트에 추가
            foreach (string characterName in randomCharacterNames)
            {
                Unit unit = new Unit(characterName, seo_basicStatsData); // Unit 클래스 생성
                player2Units.Add(unit);
            }
        }
        else if (gameManager.player1Camp == Player1Camp.Seo)
        {
            // 딕셔너리에서 랜덤으로 6개의 유닛 이름을 가져옴
            randomCharacterNames = guwol_basicStatsData.Keys
                .OrderBy(x => UnityEngine.Random.Range(0, seo_basicStatsData.Count))
                .Take(6)
                .ToList();

            // 가져온 이름으로 유닛을 생성하여 player2Units 리스트에 추가
            foreach (string characterName in randomCharacterNames)
            {
                Unit unit = new Unit(characterName, guwol_basicStatsData); // Unit 클래스 생성
                player2Units.Add(unit);
            }
        }
    }

    // 사용 : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?] -> [예] 버튼 클릭 시 호출
    // 기능 : Player2가 사용할 유닛들을 랜덤으로 결정 (반대 진영에서 임의로 차출)
    public void RandomizePlayer2Units()
    {
        player2Units.Clear();

        if (gameManager.player1Camp == Player1Camp.Guwol)
        {
            
        }
        else if (gameManager.player1Camp == Player1Camp.Seo)
        {

        }
    }


}

