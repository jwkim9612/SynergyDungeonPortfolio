﻿using UnityEngine;

public class InGameService : MonoBehaviour
{
    //public const int RATE_AT_WHICH_AFTERIMAGES_DISAPPEAR = 200;
    public const int RATE_AT_WHICH_AFTERIMAGES_DISAPPEAR = 50;
    //public const float SIZE_TO_EXPAND_THE_BATTLE_AREA = 100.0f;
    public const float DISTANCE_TO_MOVE_AT_START_OF_BATTLE = 1.0f;
    public const float SIZE_TO_BLUR = 0.5f;
    public const int NUMBER_OF_BACKAREA = 4;
    public const int NUMBER_OF_FRONTAREA = 3;
    public const int MAX_NUMBER_OF_CAN_PLACED = 7;
    public const int MIN_NUMBER_OF_CAN_PLACED = 0;
    public const int CAN_BUY_EXP = 1;

    public const int MAX_NUMBER_OF_CHARACTER_STATUS = 6;

    public const int DEFAULT_COIN = 0;
    public const int DEFAULT_LEVEL = 1;
    public const int DEFAULT_EXP = 0;
    public const int DEFAULT_EVENT_PROBABILITY = 0;
    public const int DEFAULT_NUM_OF_CAN_PLACED_IN_BATTLEAREA = 1;

    public const int MIN_NUMBER_OF_COIN = 0;
    public const int MAX_NUMBER_OF_COIN = 10000;

    public const float Z_VALUE_OF_PAWN = 5.0f;
    public const float Z_VALUE_OF_PARTICLE = 1.0f;

    public const int CRITICAL_DAMAGE_FONT_SIZE = 60;
    public const int DEFAULT_DAMAGE_FONT_SIZE = 40;
    public const int MISS_FONT_SIZE = 40;

    public const float PAUSE_SPEED = 0.0f;
    public const float DEFAULT_SPEED = 1.0f;
    public const float DOUBLE_SPEED = 2.0f;

    public const float ATTACK_DELAY = 1.0f;
    public const float DEAD_DELAY = 1.0f;

    /////////////////
    /// 시너지 리스트
    public const float LEFT_PADDING_OF_SYNERGY_LIST_AT_ANCHOR = 0.03f;
    public const float LENGTH_OF_SYNERGY_ICON_AT_ANCHOR = 0.073f;
    /////////////////

    /////////////////
    /// 시나리오
    public const int INDEX_OF_SCENARIO_TITLE = 0;
    public const int NUMBER_OF_SCENARIO_STARTING_WAVE = 4;
    public const float TITLE_READ_SPEED = 0.05f;
    /////////////////

    public static Character defaultCharacter;
    public static Enemy defaultEnemy;

    public static void Initialize()
    {
        defaultCharacter = GameObject.Find("DefaultCharacter").GetComponent<Character>();
        defaultEnemy = GameObject.Find("DefaultEnemy").GetComponent<Enemy>();
    }

    public const string COIN_IMAGE_PATH = "Images/InGame/Coin";
    public static Sprite COIN_IMAGE = Resources.Load<Sprite>(COIN_IMAGE_PATH);
}
