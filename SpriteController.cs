using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite idle;
    public Sprite attack;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetIdle()
    {
        sr.sprite = idle;
    }
    public void SetAttack()
    {
        sr.sprite = attack;
    }
}
