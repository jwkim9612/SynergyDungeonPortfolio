using System.Collections;
using UnityEngine;

public class Enemy : Pawn
{
    public Enemy()
    {
        pawnType = PawnType.Enemy;
    }

    public override void Initialize()
    {
        base.Initialize();

        OnIsDead += PlayDeadAnimation;
    }

    public override float GetAttackAnimationLength()
    {
        return EnemyService.ATTACK_ANIMATION_LENGTH;
    }

    public void SetAbility(EnemyData enemyData)
    {
        ability = new AbilityData();
        ability.SetAbilityData(enemyData);

        currentHP = ability.Health;
    }

    protected override IEnumerator Co_AttackAndAnimation()
    {
        for (int i = 0; i < 5; i++)
        {
            this.gameObject.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
            yield return new WaitForEndOfFrame();
        }

        AttackProcessing();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {
            this.gameObject.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
            yield return new WaitForEndOfFrame();
        }
    }

    private void PlayDeadAnimation()
    {
        StartCoroutine(Co_DeadAnimation());
    }

    protected override void PlayTakeHit()
    {
        StartCoroutine(Co_TakeHitEffect());
        StartCoroutine(Co_TakeHitAnimation());
    }

    private IEnumerator Co_DeadAnimation()
    {
        //spriteRenderer.enabled = false;
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.enabled = true;
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.enabled = false;
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.enabled = true;
        //yield return new WaitForSeconds(0.3f);

        float time = 0.0f;
        float fadeTime = 1.0f;

        while(time < fadeTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1.0f - time / fadeTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.enabled = false;

        Destroy(this.gameObject);
        yield break;
    }

    private IEnumerator Co_TakeHitAnimation()
    {
        Vector3 defaultPosition = this.transform.position;
        Vector3 knockBackPosition = defaultPosition + new Vector3(EnemyService.KNOCKBACK_DISTANCE, 0, 0);

        while (this.transform.position.x < knockBackPosition.x)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, knockBackPosition, EnemyService.KNOCKBACK_SPEED);
            yield return new WaitForEndOfFrame();
        }

        while (this.transform.position.x > defaultPosition.x)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, defaultPosition, EnemyService.KNOCKBACK_SPEED);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            OnIsDead();
        }
    }

    protected override IEnumerator Co_TakeHitEffect()
    {
        spriteRenderer.material = MaterialService.whiteMaterial;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = MaterialService.redMaterial;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = MaterialService.whiteMaterial;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = MaterialService.redMaterial;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = MaterialService.whiteMaterial;

        spriteRenderer.material = defaultMaterial;
    }

    public override void RandomAttack()
    {
        var battleStatus = InGameManager.instance.backCanvas.uiMainMenu.uiBattleArea.battleStatus;

        target = battleStatus.GetRandomCharacter();
        Attack(target);
    }
}
