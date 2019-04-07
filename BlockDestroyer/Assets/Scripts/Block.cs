using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    [SerializeField] AudioClip BreakSound;

    Level level;
    GameStatus gameStats;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStats = FindObjectOfType<GameStatus>();
        level.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(BreakSound, Camera.main.transform.position);
        gameStats.AddToScore();
        Destroy(gameObject);
        level.BlockDestroyed();
        
        
    }
}
