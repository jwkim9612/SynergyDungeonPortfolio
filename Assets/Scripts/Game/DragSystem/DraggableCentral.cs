﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableCentral : MonoBehaviour
{
    public UICharacterArea uiCharacterArea;
    public UIPrepareArea uiPrepareArea;

    [SerializeField] private UICharacter invisibleCharacter = null;
    [SerializeField] private UISellArea sellArea = null;

    private List<Arranger> arrangers;
    private UICharacter swappedCharacter;
    private UISlot parentWhenBeginDrag;
    private float originalSize;
    private CharacterInfo selledCharacterInfo;
    private bool isSelling;
    private bool isSwapped;

    [SerializeField] private Camera cam;
    [SerializeField] private UIInGameCharacterInfo uiInGameCharacterInfo = null;

    public void Initialize()
    {
        InitializeArrangers();

        isSelling = false;
        isSwapped = false;
        swappedCharacter = null;
        originalSize = 0.0f;
    }

    // 캐릭터 이외의 곳을 클릭했을 때 캐릭터 정보를 꺼주는 Update문
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var characterAreaListWithCharacters = uiCharacterArea.GetUICharacterListWithCharacters();
            var prepareAreaListWithCharacters = uiPrepareArea.GetUICharacterListWithCharacters();

            List<UICharacter> characterListWithCharacters = new List<UICharacter>();
            characterListWithCharacters.AddRange(characterAreaListWithCharacters);
            characterListWithCharacters.AddRange(prepareAreaListWithCharacters);

            var uiCharacter = characterListWithCharacters.Find(t => TransformService.ContainPos(t.transform as RectTransform, Input.mousePosition, cam));
            if (uiCharacter == null)
            {
                if (uiInGameCharacterInfo.gameObject.activeSelf)
                {
                    uiInGameCharacterInfo.OnHide();
                }
            }
        }
    }

    private void InitializeArrangers()
    {
        uiCharacterArea.Initialize();
        uiPrepareArea.Initialize();

        arrangers = new List<Arranger>();
        arrangers.Add(uiCharacterArea.backArea);
        arrangers.Add(uiCharacterArea.frontArea);
        arrangers.Add(uiPrepareArea);
    }

    void SwapCharacters(UICharacter source, UICharacter destination)
    {
        Transform sourceTransform = source.transform;
        Transform destinationTransform = destination.transform;

        Transform sourceParent = sourceTransform.parent;
        Transform destinationParent = destinationTransform.parent;

        sourceTransform.SetParent(destinationParent);
        destinationTransform.SetParent(sourceParent);

        Vector3 sourcePosition = sourceTransform.position;
        Vector3 destinationPosition = destinationTransform.position;

        sourceTransform.position = destinationPosition;
        destinationTransform.position = sourcePosition;

        source.FollowCharacter();
        destination.FollowCharacter();

        TransformService.SetFullSize(source.transform as RectTransform);
        TransformService.SetFullSize(destination.transform as RectTransform);

        foreach (var arranger in arrangers)
        {
            arranger.UpdateChildren();
        }
    }

    private void PointerDown(UICharacter uiCharacter)
    {
        uiInGameCharacterInfo.SetInGameCharacterInfo(uiCharacter);
        uiInGameCharacterInfo.OnShow();
    }

    private void BeginDrag(UICharacter uiCharacter)
    {
        parentWhenBeginDrag = uiCharacter.GetComponentInParent<UISlot>();
        SwapCharacters(invisibleCharacter, uiCharacter);
        uiCharacter.SetDefaultImage();
        originalSize = uiCharacter.character.GetSize();
        sellArea.UpdatePrice(uiCharacter.characterInfo);
        sellArea.gameObject.SetActive(true);
    }

    private void Drag(UICharacter uiCharacter)
    {
        Arranger whichArrangersCharacter;

        whichArrangersCharacter = arrangers.Find(t => TransformService.ContainPos(t.transform as RectTransform, uiCharacter.transform.position));

        if (whichArrangersCharacter != null)
        {
            UICharacter targetCharacter = whichArrangersCharacter.GetCharacterByPosition(uiCharacter);

            if (targetCharacter != null)
            {
                if (!isSwapped)
                {
                    // 첫 번째로 다른 자리로 드래그했으면
                    if (targetCharacter != invisibleCharacter)
                    {
                        SwapCharacters(invisibleCharacter, targetCharacter);
                        SetCharacterImage(targetCharacter);
                        swappedCharacter = targetCharacter;
                        isSwapped = true;
                    }
                }
                else
                {
                    // 드래그하다가 원래 자리로
                    if(targetCharacter == swappedCharacter)
                    {
                        SwapCharacters(invisibleCharacter, targetCharacter);
                        SetCharacterImage(targetCharacter);
                        isSwapped = false;
                    }
                    // 다른 자리로 드래그 후 또 다른 자리로 갔을 경우
                    else if(targetCharacter != invisibleCharacter)
                    {
                        SwapCharacters(invisibleCharacter, swappedCharacter);
                        SetCharacterImage(swappedCharacter);
                        SwapCharacters(invisibleCharacter, targetCharacter);
                        SetCharacterImage(targetCharacter);
                        swappedCharacter = targetCharacter;
                    }
                }

                // 캐릭터 크기 조정
                if (TransformService.ContainPos(uiCharacterArea.transform as RectTransform, uiCharacter.transform.position))
                {
                    uiCharacter.character.SetSize(CharacterService.SIZE_IN_BATTLE_AREA);
                }
                else if (TransformService.ContainPos(uiPrepareArea.transform as RectTransform, uiCharacter.transform.position))
                {
                    uiCharacter.character.SetSize(CharacterService.SIZE_IN_PREPARE_AREA);
                }
            }
        }
        else
        {
            // 드래그 중에 CharacterArea 또는 PrepareArea 이외의 공간에 있는 경우
            if (isSwapped)
            {
                uiCharacter.character.SetSize(originalSize);
                SwapCharacters(swappedCharacter, invisibleCharacter);
                SetCharacterImage(swappedCharacter);
                isSwapped = false;
            }
        }

        // Sell에 드래그했을 때
        if (TransformService.ContainPos(sellArea.transform as RectTransform, uiCharacter.transform.position))
        {
            isSelling = true;
            sellArea.gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            isSelling = false;
            sellArea.gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    private void EndDrag(UICharacter uiCharacter)
    {
        uiInGameCharacterInfo.OnHide();

        sellArea.gameObject.SetActive(false);

        if (isSelling)
        {
            selledCharacterInfo = uiCharacter.DeleteCharacterBySell();
            sellArea.gameObject.GetComponent<Image>().color = Color.white;
        }

        if(!IsNotChanged())
        {
            if (IsPlaceableSpaceFull())
            {
                if (IsMoveFromPrepareAreaToEmptyBattleArea())
                {
                    SwapCharacters(invisibleCharacter, swappedCharacter);
                    SwapCharacters(invisibleCharacter, uiCharacter);
                    isSwapped = false;
                    return;
                }
            }
        }

        UpdateSynergyService(uiCharacter);
        UpdateCurrentPlacedCharacters();
        SwapCharacters(invisibleCharacter, uiCharacter);
        SetCharacterImage(uiCharacter);

        isSwapped = false;
    }

    /// <summary>
    /// 캐릭터의 성을 업그레이드 하면서 배치된 캐릭터의 수를 업데이트
    /// </summary>
    /// <param name="characterInfo"></param>
    public void CombinationCharacter(CharacterInfo characterInfo)
    {

        bool isFirstCharacter = true;

        foreach (var arranger in arrangers)
        {
            foreach(var uiCharacter in arranger.uiCharacters)
            {
                if (uiCharacter.characterInfo == null)
                    continue;

                if (uiCharacter.characterInfo.Equals(characterInfo))
                {
                    InGameManager.instance.synergySystem.SubCharacterFromCombinations(uiCharacter, isFirstCharacter);
                    uiCharacterArea.SubCurrentPlacedCharacterFromCombinations(uiCharacter, isFirstCharacter);

                    if (isFirstCharacter)
                    {
                        isFirstCharacter = false;
                        uiCharacter.UpgradeStar();
                    }
                    else
                    {
                        uiCharacter.DeleteCharacter();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 현재 배치된 캐릭터들의 시너지 적용
    /// </summary>
    /// <param name="uiCharacter"></param>
    public void UpdateSynergyService(UICharacter uiCharacter)
    {
        var synergySystem = InGameManager.instance.synergySystem;

        if (IsMoveToSell())
        {
            synergySystem.SubCharacter(selledCharacterInfo);
            return;
        }

        if (IsNotChanged())
            return;

        if (IsMoveFromBattleAreaToPrepareArea())
        {
            if (IsReplaceWithEmptySpace())
                synergySystem.SubCharacter(uiCharacter.characterInfo);
            else
            {
                synergySystem.SubCharacter(uiCharacter.characterInfo);
                synergySystem.AddCharacter(swappedCharacter.characterInfo);
            }
        }
        else if (IsMoveFromPrepareAreaToBattleArea())
        {
            if (IsReplaceWithEmptySpace())
                synergySystem.AddCharacter(uiCharacter.characterInfo);
            else
            {
                synergySystem.AddCharacter(uiCharacter.characterInfo);
                synergySystem.SubCharacter(swappedCharacter.characterInfo);
            }
        }
    }

    /// <summary>
    /// 필드에 배치된 캐릭터 수 업데이트.
    /// </summary>
    public void UpdateCurrentPlacedCharacters()
    {
        if (IsMoveToSell())
        {
            uiCharacterArea.SubCurrentPlacedCharacter();
            return;
        }

        if (IsNotChanged())
            return;

        if (IsMoveFromBattleAreaToEmptyPrepareArea())
        {
            uiCharacterArea.SubCurrentPlacedCharacter();
        }
        else if (IsMoveFromPrepareAreaToEmptyBattleArea())
        {
            uiCharacterArea.AddCurrentPlacedCharacter();
        }
    }

    private bool IsMoveFromPrepareAreaToEmptyBattleArea()
    {
        if(IsMoveFromPrepareAreaToBattleArea())
            if(IsReplaceWithEmptySpace())
                return true;

        return false;
    }

    private bool IsMoveFromBattleAreaToEmptyPrepareArea()
    {
        if (IsMoveFromBattleAreaToPrepareArea())
            if (IsReplaceWithEmptySpace())
                return true;

        return false;
    }

    private bool IsReplaceWithEmptySpace()
    {
        return swappedCharacter.character == null ? true : false;
    }

    private bool IsMoveFromPrepareAreaToBattleArea()
    {
        return (invisibleCharacter.GetArea<UICharacterArea>() != null && swappedCharacter.GetArea<UIPrepareArea>() != null) ? true : false;
    }

    private bool IsMoveFromBattleAreaToPrepareArea()
    {
        return (invisibleCharacter.GetArea<UIPrepareArea>() != null && swappedCharacter.GetArea<UICharacterArea>() != null) ? true : false;
    }

    private bool IsNotChanged()
    {
        return invisibleCharacter.GetComponentInParent<UISlot>() == parentWhenBeginDrag ? true : false;
    }

    private bool IsMoveToSell()
    {
        return (invisibleCharacter.GetArea<UICharacterArea>() != null && isSelling) ? true : false;
    }

    private bool IsPlaceableSpaceFull()
    {
        int numOfCurrentPlacedCharacters = uiCharacterArea.numOfCurrentPlacedCharacters;
        int numOfCanPlacedInBattleArea = InGameManager.instance.playerState.numOfCanPlacedInBattleArea;

        return numOfCurrentPlacedCharacters >= numOfCanPlacedInBattleArea ? true : false;
    }

    // 배치된 공간에 따라서 이미지를 바꿔줌
    private void SetCharacterImage(UICharacter uiCharacter)
    {
        if (uiCharacter.character == null)
            return;

        if(uiCharacter.GetArea<UICharacterArea>() != null)
        {
            uiCharacter.SetAnimationImage();
            // 일반 이미지로 변경;
        }
        else if(uiCharacter.GetArea<UIPrepareArea>() != null)
        {
            uiCharacter.SetDefaultImage();
            // 애니메이션 이미지로 변경;
        }
    }
}
