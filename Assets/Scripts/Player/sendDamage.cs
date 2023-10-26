using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendDamage : MonoBehaviour
{
    [SerializeField] protected float damage = 20;

    public virtual void Send(Transform obj)
    {
        receiveDamage receiveDam = obj.GetComponentInChildren<receiveDamage>();
        if (receiveDam == null) return;
        this.Send(receiveDam);
    }

    public virtual void Send(receiveDamage receiveDam)
    {
        receiveDam.Deduct(this.damage);
        this.DestroyObject();
    }

    protected virtual void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }


}
