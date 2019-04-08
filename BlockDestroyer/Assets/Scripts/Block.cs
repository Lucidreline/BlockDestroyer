using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    [SerializeField] AudioClip BreakSound;
    [SerializeField] GameObject blockSparklesVFX;
    
    [SerializeField] Sprite[] hitSprites;

    Level level;
    GameStatus gameStats;

    [SerializeField] int timesHit; 

    private void Start()
    {
        gameStats = FindObjectOfType<GameStatus>();
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (gameObject.tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "Breakable")
        {
            HandleHit();

        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + ": Block sprite is missing from array");
        }
        
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(BreakSound, Camera.main.transform.position);
        gameStats.AddToScore();
        Destroy(gameObject);
        level.BlockDestroyed();
        triggerSparklesVFX();
        

    }

    private void triggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
