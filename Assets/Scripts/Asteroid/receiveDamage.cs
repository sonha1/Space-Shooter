using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class receiveDamage : MonoBehaviour
{
    [SerializeField] protected float HP;
    [SerializeField] protected float HPmax;


    private void Reset()
    {
        this.HP = this.HPmax;
    }

    public virtual void Add(float damage)
    {
        this.HP += damage;
        if(this.HP > this.HPmax) this.HP = this.HPmax;
    }

    public virtual void Deduct (float deduct)
    {
        this.HP -= deduct;
        if(this.HP < 0) this.HP = 0;
    }

    public virtual bool IsDead()
    {
        return this.HP <= 0;
    }
}
