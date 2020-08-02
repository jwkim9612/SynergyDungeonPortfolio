using System.Collections.Generic;
using UnityEngine;

// 완성되면 설명 적기.
public class SynergySystem
{
    public delegate void OnTribeChangedDelegate();
    public OnTribeChangedDelegate OnTribeChanged { get; set; }

    public delegate void OnOriginChangedDelegate();
    public OnOriginChangedDelegate OnOriginChanged { get; set; }

    public Dictionary<TribeInfo, int> deployedTribes;
    public Dictionary<OriginInfo, int> deployedOrigins;
    public Dictionary<Tribe, int> appliedTribes;
    public Dictionary<Origin, int> appliedOrigins;

    private CharacterDataSheet characterDataSheet;

    public SynergySystem()
    {
        deployedTribes = new Dictionary<TribeInfo, int>();
        deployedOrigins = new Dictionary<OriginInfo, int>();
        appliedTribes = new Dictionary<Tribe, int>();
        appliedOrigins = new Dictionary<Origin, int>();
    }

    public void Initialize()
    {
        characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
            return;
        }
    }

    public void AddCharacter(CharacterInfo characterInfo)
    {
        if(characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
        {
            TribeInfo tribeInfo = new TribeInfo(tribe, characterInfo.id);

            if (deployedTribes.ContainsKey(tribeInfo))
            {
                ++deployedTribes[tribeInfo];
            }
            else
            {
                deployedTribes.Add(tribeInfo, 1);
                AddAppliedTribe(tribeInfo.tribe);
            }
        }

        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            OriginInfo originInfo = new OriginInfo(origin, characterInfo.id);

            if (deployedOrigins.ContainsKey(originInfo))
            {
                ++deployedOrigins[originInfo];
            }
            else
            {
                deployedOrigins.Add(originInfo, 1);
                AddAppliedOrigin(originInfo.origin);
            }
        }
    }

    public void SubCharacter(CharacterInfo characterInfo)
    {
        if (characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
        {
            TribeInfo tribeInfo = new TribeInfo(tribe, characterInfo.id);

            if (deployedTribes.ContainsKey(tribeInfo))
            {
                --deployedTribes[tribeInfo];

                if (deployedTribes[tribeInfo] == 0)
                {
                    deployedTribes.Remove(tribeInfo);
                    SubAppliedTribe(tribeInfo.tribe);
                }
            }
            else
            {
                Debug.Log("Error No Tribes");
            }
        }

        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            OriginInfo originInfo = new OriginInfo(origin, characterInfo.id);

            if (deployedOrigins.ContainsKey(originInfo))
            {
                --deployedOrigins[originInfo];

                if (deployedOrigins[originInfo] == 0)
                {
                    deployedOrigins.Remove(originInfo);
                    SubAppliedOrigin(originInfo.origin);
                }
            }
            else
            {
                Debug.Log("Error No Origins");
            }
        }
    }

    public void AddAppliedTribe(Tribe tribe)
    {
        if (appliedTribes.ContainsKey(tribe))
        {
            ++appliedTribes[tribe];
        }
        else
        {
            appliedTribes.Add(tribe, 1);
        }

        OnTribeChanged();
    }

    public void AddAppliedOrigin(Origin origin)
    {
        if (appliedOrigins.ContainsKey(origin))
        {
            ++appliedOrigins[origin];
        }
        else
        {
            appliedOrigins.Add(origin, 1);
        }

        OnOriginChanged();
    }


    public void SubAppliedTribe(Tribe tribe)
    {
        if (appliedTribes.ContainsKey(tribe))
        {
            --appliedTribes[tribe];

            if (appliedTribes[tribe] == 0)
            {
                appliedTribes.Remove(tribe);
            }
        }
        else
        {
            Debug.Log("Error No AppliedTribes");
        }

        OnTribeChanged();
    }

    public void SubAppliedOrigin(Origin origin)
    {
        if (appliedOrigins.ContainsKey(origin))
        {
            --appliedOrigins[origin];

            if (appliedOrigins[origin] == 0)
            {
                appliedOrigins.Remove(origin);
            }
        }
        else
        {
            Debug.Log("Error No AppliedOrigins");
        }

        OnOriginChanged();
    }

    public void SubCharacterFromCombinations(UICharacter uiCharacter, bool isFirstCharacter)
    {
        if (isFirstCharacter)
            return;

        if (uiCharacter.GetArea<UIBattleArea>() != null)
            InGameManager.instance.synergySystem.SubCharacter(uiCharacter.characterInfo);
    }
}
