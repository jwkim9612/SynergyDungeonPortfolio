public class AccountData
{ 
    public string id;
    public string pw;
    public bool isLoginToGoogle;
}

public enum Ability
{ 
    None,
    Attack,
    MagicalAttack,
    Health,
    Defence,
    MagicDefence,
    Shield,
    Accuracy,
    Evasion,
    Critical,
    AttackSpeed
}

public enum Tier
{ 
    None,
    One,
    Two,
    Three,
    Four,
    Five
}

public enum InGameState
{ 
    None,
    Prepare,
    Battle,
    Complete,
    Lose
}

public enum PawnType
{ 
    Character,
    Enemy
}

public enum Tribe
{
    None,
    Beast,
    Devil,
    Dragon,
    Elemental,
    Elf,
    Human,
    Machine,
    Undead
}

public enum Origin
{
    None,
    Archer,
    Paladin,
    Thief,
    Warrior,
    Wizard
}

public enum PurchaseCurrency
{ 
    None,
    Gold,
    Diamond,
    Heart
}


public enum RewardCurrency
{ 
    None,
    Gold,
    Rune,
    RandomRune,
    RandomPotion,
    Relic,
    Artifact,
    Coin,
    Status,
    RandomArtifactPiece,
    Heart,
    Nothing
}

public enum RuneGrade
{
    None,
    F_0,
    D_0,
    C_0,
    B_0,
    A_0,
    S_0,
    S_1,
    SS_0,
    SS_1,
    SS_2,
    SSS_0,
    SSS_1,
    SSS_2,
    SSS_3
}

public enum RuneRating
{ 
    None,
    Normal,
    Unique
}

public enum WayOfCalculate
{ 
    None,
    Value,
    Percentage
}

public enum PotionGrade
{ 
    None,
    F,
    D,
    C,
    B,
    A
}

public enum AbilityEffectData
{ 
    None,
    Potion,
    Scenario
}

public enum SortBy
{ 
    None,
    Grade,
    Socket
}
