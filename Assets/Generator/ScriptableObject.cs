using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int playerScore;
    public int resultScore;
    public int deathCount;
    public bool isDied;
    public float life; // パートナーの生命値
    public float lifeMax;
    public float playTime;
}
