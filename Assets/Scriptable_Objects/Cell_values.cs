using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cell_values", menuName = "Quiz_game/Cell_values", order = 0)]
public class Cell_values : ScriptableObject 
{
    [SerializeField]
    Sprite _cellSprite;
    public Sprite CellSprite => _cellSprite;

    [SerializeField]
    uint _cellID;
    public uint CellID => _cellID;

}
