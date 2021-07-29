using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Game_Settings", menuName = "Quiz_game/Game_Settings", order = 0)]
public class Game_Settings : ScriptableObject 
{
    //Кол-во уровней сложности игры
    [SerializeField]
    int _difficultyLevels;
    public int DifficultyLevels => _difficultyLevels;

    //Набор изображений
    [SerializeField]
    List<Sprite> _sriteSet;
    public List<Sprite> SriteSet => _sriteSet;

    //Стартовые значения кол-ва строк и столбцов сетки
    [SerializeField]
    int _gridRowsStart;
    public int GridRowsStart => _gridRowsStart;
    [SerializeField]
    int _gridColsStart;
    public int GridColsStart => _gridColsStart;

    //Кол-во прироста к строкам и столбцам с каждым последующим уровнем сложности
    [SerializeField]
    int _gridRowsIncome;
    public int GridRowsIncome => _gridRowsIncome;
    [SerializeField]
    int _gridColsIncome;
    public int GridColsIncome => _gridColsIncome;
}
