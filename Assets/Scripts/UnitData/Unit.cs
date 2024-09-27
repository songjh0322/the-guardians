using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 각 유닛의 정보
[Serializable]
public class BasicStats
{
    public string unitName;
    public string passiveName;
    public string skillName1;
    public string skillName2;

    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;
}

// 각 유닛의 인터페이스
public interface IUnit
{
    void move();
    void castSkill1(string unitName);   // 1번 스킬
    void castSkill2(string unitName);   // 2번 스킬
}

// 인게임에서 객체로 만들 Unit 클래스
[Serializable]
public class Unit : IUnit
{
    // 변경되지 않는 능력치 (단순 문자열)
    public string unitName; // Key값으로 활용할 캐릭터명
    public string passiveName;
    public string skillName1;
    public string skillName2;

    // 원본 캐릭터 능력치 (학습을 통해 변경될 수 있지만, 플레이 도중에는 변경되지 않음)
    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;

    // 인게임에서 활용될 능력치 (패시브나 스킬 등을 통해 플레이 도중 변경될 수 있음)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;


    // Unit 생성자: unitName을 통해 JSON 데이터에서 정보를 가져옴
    public Unit(string unitName, Dictionary<string, BasicStats> basicStatsData)
    {
        if (basicStatsData.ContainsKey(unitName))
        {
            BasicStats stats = basicStatsData[unitName];

            // 변경되지 않는 능력치
            this.unitName = stats.unitName;
            this.passiveName = stats.passiveName;
            this.skillName1 = stats.skillName1;
            this.skillName2 = stats.skillName2;
            this.maxHealth = stats.maxHealth;
            this.attackPoint = stats.attackPoint;
            this.defensePoint = stats.defensePoint;
            this.moveRange = stats.moveRange;

            // 인게임에서 사용될 능력치 설정 (모두 기본 능력치를 기반으로 설정)
            this.currentHealth = maxHealth;
            this.currentAttackPoint = attackPoint;
            this.currentDefensePoint = defensePoint;
            this.currentMoveRange = moveRange;
        }
        else
        {
            Debug.LogError("CharacterStats.JSON에 해당 캐릭터가 없습니다: " + unitName);
        }
    }

    public void move()
    {
        throw new NotImplementedException();
    }

    public void castSkill1(string unitName)
    {
        throw new NotImplementedException();
    }

    public void castSkill2(string unitName)
    {
        throw new NotImplementedException();
    }
}