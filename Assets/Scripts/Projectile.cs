﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SpriteRenderer spriteR;
    public SpriteRenderer splashR;
    public Monster target;
    public Vector2 source;
    public ProjectileData projData;
    private float interval;
    private float elapsedtime;
    private bool exploding;

    private const float explodetime = 100;

    // Use this for initialization
    public void Initialize()
    {
        transform.position = source;
        interval = projData.hitTime - projData.startTime;
        elapsedtime = 0;
        exploding = false;
        spriteR.enabled = true;
        splashR.enabled = false;
        if (interval <= 0) {
            hitTarget();
        }
    }

    // Update is called once per frame
    void Update() {
        if (exploding) {
            elapsedtime += 1000 * Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.25f, elapsedtime / explodetime);
            splashR.color = new Color(splashR.color.r, splashR.color.g, splashR.color.b, alpha);
            if (elapsedtime >= explodetime) {
                Release();
            }
            return;
        }

        if (target == null) { // Target disappeared.
            Release();
            return;
        }

        //int elapsedtime = GameManager.instance.getTime() - projData.startTime;
        elapsedtime += 1000*Time.deltaTime;
        float progress = elapsedtime / interval;
        transform.position = Vector2.Lerp(source, target.transform.position, progress);
        if (progress >= 1) {
            hitTarget();
        }
    }

    private void hitTarget() {
        if (target == null) { Release(); return; }

        if (projData.isView) {
            if (projData.splashRadius == 0) {
                Release();
            } else {
                Explode();
            }
            return;
        }

        if (projData.splashRadius == 0) {
            target.TakeDamage(projData.damage);
            if (projData.stunTime > 0) {
                target.inflictStun(projData.stunTime);
            }
            if (projData.slowTime > 0) {
                target.inflictSlow(projData.slowTime);
            }
            if (projData.DOTdamage > 0) {
                target.inflictDOT(projData.DOTdamage, projData.DOTduration);
            }
            Release();
        } else {
            foreach (Collider2D inrange in Physics2D.OverlapCircleAll(target.transform.position, projData.splashRadius, 1 << 2)) {
                if (inrange.CompareTag("Monster")) {
                    Monster monster = inrange.GetComponent<Monster>();
                    monster.TakeDamage(projData.damage);
                    if (projData.stunTime > 0) {
                        monster.inflictStun(projData.stunTime);
                    }
                    if (projData.slowTime > 0) {
                        monster.inflictSlow(projData.slowTime);
                    }
                    if (projData.DOTdamage > 0) {
                        monster.inflictDOT(projData.DOTdamage, projData.DOTduration);
                    }
                }
            }
            Explode();
        }
    }

    private void Explode() {
        transform.position = target.transform.position;
        exploding = true;
        spriteR.enabled = false;
        splashR.enabled = true;
        splashR.transform.localScale = new Vector2(projData.splashRadius*2, projData.splashRadius*2);
        elapsedtime = 0;
    }

    private void Release()
    {
        GameManager.instance.projectilePool.ReleaseProjectile(this);
    }
}
