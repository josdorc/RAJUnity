using UnityEngine;
using System;
using System.Collections;

public static class Constants
{
    public const int MAX_GAMES = 3;
    public const int MAX_LEVELS = 20;
    public const int MAX_HEALTH = 4;
    public const int MAX_ENERGY = 10;
    public const int MAX_GEMS = 20;
    public const int ENERGY_RATIO = 3;
}

public enum EItemType
{
	None,
	Star,
	Heart,
	Coin,
    KeyYellow,
    KeyBlue,
    KeyGreen,
    KeyRed,
    Gem,
    MapForest,
    TenCoins,
    Shuriken,
    Bomb,
    FireBeam
}

public enum EAttackType
{
    None,
    Shuriken,
    Bomb,
    FireBeam
}

public enum ESnailShellState
{
	Idle, 
	Motion
}

public enum EEnemyState
{
    Idle,
    Attack,
    Rest
}

public enum EStartPosition
{
	Warp,
	CheckPoint,
	LevelStart,
	None
}

public enum LockColor
{
    Yellow,
    Red, 
    Blue,
    Green
}

[Serializable]
public struct ItemDefinition
{
	public EItemType Type;
	public Sprite ItemSprite;
	public GameObject ItemPrefab;
}





