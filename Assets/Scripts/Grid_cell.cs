using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;

public class Grid_cell : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    ParticleSystem starParticles;
    public Sprite SpriteImage => _spriteRenderer.sprite;
    int _id;
    public int ID => _id;
    Game_Grid _gridParent;

    void Awake() 
    {
        _gridParent = gameObject.GetComponentInParent<Game_Grid>();
        GameObject _gameObjectTemp = transform.Find("Grid_cell_sprite").gameObject;
        _spriteRenderer = _gameObjectTemp.GetComponent<SpriteRenderer>(); 
        starParticles = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    public void ShootStars()
    {
        starParticles.Play();
    }

    public void Initialize(Sprite sprite, int id) 
    {
        _spriteRenderer.sprite = sprite;
        _id = id;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
