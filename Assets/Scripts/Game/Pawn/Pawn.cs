using Shared.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public delegate void OnAttackDelegate();
    public delegate void OnHitDelegate();
    public delegate void OnIsDeadDelegate();
    public OnAttackDelegate OnAttack { get; set; }
    public OnHitDelegate OnHit { get; set; }
    public OnIsDeadDelegate OnIsDead { get; set; }

    public string pawnName { get; set; }
    public PawnType pawnType { get; set; }
    public bool isDead { get; set; }
    public AbilityData ability;
    protected long currentHP;
    public SpriteRenderer spriteRenderer;
    public Material defaultMaterial;

    protected Pawn target;

    public List<UIFloatingText> uiFloatingTextList { get; set; } = null;
    protected int floatingTextIndex;

    public virtual void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        OnHit += PlayTakeHit;
    }

    // 공격
    public void Attack(Pawn target)
    {
        if (target == null)
        {
            Debug.Log("target is null");
            return;
        }

        StartCoroutine(Co_AttackAndAnimation());

        //InGameManager.instance.battleLogService.AddBattleLog(name + "(이)가 " + target.name + "(이)에게 " + finalDamage + "데미지를 입혔습니다.");
    }

    // 랜덤 공격
    public virtual void RandomAttack()
    {
    }

    /// <summary>
    /// 데미지를 받는 함수
    /// </summary>
    /// <param name="damage">받은 데미지</param>
    /// <returns>최종적으로 입은 데미지</returns>
    public long TakeDamage(long damage, bool isCritical)
    {
        long finalDamage;

        if (isCritical)
        {
            finalDamage = Mathf.Clamp((int)(damage * 2) - (int)(ability.Defence), 1, (int)damage);
            PlayCriticalHitText(finalDamage);
        }
        else
        {
            finalDamage = Mathf.Clamp((int)damage - (int)(ability.Defence), 1, (int)damage);
            PlayHitText(finalDamage);
        }

        currentHP = Mathf.Clamp((int)(currentHP - finalDamage), 0, (int)currentHP);
        OnHit();

        if (currentHP <= 0)
        {
            isDead = true;
        }

        return finalDamage;
    }

    // 스텟 리셋
    public virtual void ResetStat()
    {
        isDead = false;
    }

    // HP퍼센트를 반환
    public float GetHPRatio()
    {
        return currentHP / (float)ability.Health;
    }

    // 공격이 성공했는지를 반환 (회피율 계산)
    protected bool GetAttackSuccessful(Pawn target)
    {
        long currentAccuracy = ability.Accuracy - target.ability.Evasion;
        long randomAccuracyNum = RandomService.GetRandomLong();

        if (currentAccuracy <= randomAccuracyNum)
            return false;
        else
            return true;
    }

    // 크리티컬이 발동 됐는지를 반환
    protected bool IsCriticalAttack()
    {
        long currentCritical = ability.Critical;
        long randomCriticalNum = RandomService.GetRandomLong();

        if (currentCritical <= randomCriticalNum)
            return false;
        else
            return true;
    }

    // 사이즈 셋팅
    public void SetSize(float size)
    {
        spriteRenderer.transform.localScale = new Vector3(size, size, size);
    }

    // 이미지 셋팅
    public void SetImage(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // 이름 셋팅
    public void SetName(string name)
    {
        pawnName = name;
    }
    
    // 플로팅 텍스트 셋팅
    public void SetUIFloatingTextList(List<UIFloatingText> uiFloatingTextList)
    {
        this.uiFloatingTextList = uiFloatingTextList;
    }

    // 플로팅 텍스트 초기화
    public void InitializeUIFloatingTextList()
    {
        if (uiFloatingTextList != null)
        {
            foreach(var uiFloatingText in uiFloatingTextList)
            {
                uiFloatingText.Initialize();
            }
        }
    }

    // 공격 애니메이션 시간을 반환
    public virtual float GetAttackAnimationLength()
    {
        return 0.0f;
    }

    // 공격 애니메이션 실행
    public virtual void PlayAttackAnimation()
    {
    }

    // 공격이 성공했는지 확인후 타겟에 데미지를 주는 공격 처리
    public void AttackProcessing()
    {
        if (GetAttackSuccessful(target))
        {
            if (IsCriticalAttack())
                target.TakeDamage(ability.Attack, true);
            else
                target.TakeDamage(ability.Attack, false);
        }
        else
            target.PlayMissText();
    }

    // 기본 데미지 플로팅 텍스트 실행
    private void PlayHitText(float damage)
    {
        uiFloatingTextList[floatingTextIndex].SetText(damage.ToString(), Color.red);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.DEFAULT_DAMAGE_FONT_SIZE);
        PlayFloatingText();
    }

    // 크리티컬 플로팅 텍스트 실행
    private void PlayCriticalHitText(float damage)
    {
        Color orange = new Color(1.0f, 0.64f, 0.0f);
        uiFloatingTextList[floatingTextIndex].SetText(damage.ToString(), orange);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.CRITICAL_DAMAGE_FONT_SIZE);
        PlayFloatingText();
    }

    // Miss 플로팅 텍스트 실행
    public void PlayMissText()
    {
        uiFloatingTextList[floatingTextIndex].SetText("Miss", Color.gray);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.MISS_FONT_SIZE);
        PlayFloatingText();
    }

    // 플로팅 텍스트 실행
    private void PlayFloatingText()
    {
        uiFloatingTextList[floatingTextIndex].Play();
        ++floatingTextIndex;

        if (floatingTextIndex >= uiFloatingTextList.Count)
            floatingTextIndex = 0;
    }

    // 데미지를 받았을 때 효과
    protected virtual void PlayTakeHit()
    {
        StartCoroutine(Co_TakeHitEffect());
    }

    // 공격과 애니메이션 실행
    protected virtual IEnumerator Co_AttackAndAnimation()
    {
        yield return new WaitForEndOfFrame();
    }

    // 데미지를 받았을 때 효과
    protected virtual IEnumerator Co_TakeHitEffect()
    {
        yield return new WaitForEndOfFrame();
    }

    // 공격 대상을 리턴
    public Pawn GetTarget()
    {
        return target;
    }
}
