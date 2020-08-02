using System.Collections;
using UnityEngine;

public class Character : Pawn
{
    public Animator animator;
    public Origin origin;
    public CharacterInfo characterInfo;

    public Character()
    {
        pawnType = PawnType.Character;
    }

    public override void Initialize()
    {
        base.Initialize();
        animator = GetComponent<Animator>();

        floatingTextIndex = 0;
    }

    public void SetAbility(CharacterAbilityData characterAbilityData, Origin newOrigin)
    {
        ability = new AbilityData();
        ability.SetAbilityData(characterAbilityData);
        origin = newOrigin;

        ///////////////////////////////////// 룬 능력치 + ///////////////////////////////////////////////
        Rune rune = RuneManager.Instance.GetEquippedRuneOfOrigin(origin);
        if(rune != null)
        {
            ability += rune.runeData.AbilityData;
            Debug.Log("어빌맅  더하기");
        }
        ///////////////////////////////////// ///////////// ///////////////////////////////////////////////



        currentHP = ability.Health;
    }

    protected override IEnumerator Co_AttackAndAnimation()
    {
        //if (IsMeleeUnit())
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        this.gameObject.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
        //        yield return new WaitForEndOfFrame();
        //    }

        //    AttackProcessing();

        //    yield return new WaitForSeconds(0.5f);

        //    for (int i = 0; i < 5; i++)
        //    {
        //        this.gameObject.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
        //        yield return new WaitForEndOfFrame();
        //    }
        //}
        if (animator.runtimeAnimatorController == null)
        {
            for (int i = 0; i < 5; i++)
            {
                this.gameObject.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
                yield return new WaitForEndOfFrame();
            }

            AttackProcessing();

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 5; i++)
            {
                this.gameObject.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            AttackProcessing();
        }

        yield return null;
    }

    public override void RandomAttack()
    {
        var battleStatus = InGameManager.instance.backCanvas.uiMainMenu.uiBattleArea.battleStatus;

        target = battleStatus.GetRandomEnemy();
        Attack(target);
    }

    public override void ResetStat()
    {
        base.ResetStat();
        currentHP = ability.Health;
    }

    public float GetSize()
    {
        return spriteRenderer.transform.localScale.x;
    }

    public void OnHide()
    {
        spriteRenderer.gameObject.SetActive(false);
    }

    public void OnShow()
    {
        spriteRenderer.gameObject.SetActive(true);

    }

    public void SetRunTimeAnimatorController(RuntimeAnimatorController runTimeAnimatorController)
    {
        animator.runtimeAnimatorController = runTimeAnimatorController;
    }

    public void SetCharacterInfo(CharacterInfo newCharacterInfo)
    {
        characterInfo = newCharacterInfo;
    }

    public void RemoveRunTimeAnimatorController()
    {
        animator.runtimeAnimatorController = null;
    }

    public void PlayWinAnimation()
    {
        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("Win", true);
        }
    }

    public override void PlayAttackAnimation()
    {
        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("Attack", true);
        }
    }

    // Win 애니메이션에서 사용함.
    private void WinEnd()
    {
        animator.SetBool("Win", false);
    }

    // Attack 애니메이션에서 사용함.
    private void AttackEnd()
    {
        animator.SetBool("Attack", false);
    }

    public override float GetAttackAnimationLength()
    {
        if (animator == null)
        {
            return 1.0f;
        }

        if (animator.runtimeAnimatorController != null)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "Attack")
                {
                    return ac.animationClips[i].length;
                }
            }
        }
        else
        {
            return 1.0f;
        }

        Debug.LogError("Error GetAttackAnimationLength");
        return -1;
    }

    public bool HasAnimation()
    {
        if (animator != null)
        {
            if (animator.runtimeAnimatorController != null)
            {
                return true;
            }
        }

        return false;
    }

    protected override IEnumerator Co_TakeHitEffect()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            OnIsDead();
        }
    }

    private bool IsMeleeUnit()
    {
        if (origin == Origin.Archer || origin == Origin.Wizard)
            return false;

        return true;
    }
}
