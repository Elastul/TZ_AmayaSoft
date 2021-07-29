using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnObjectiveChange : UnityEvent<string, int>
{
}

[System.Serializable]
public class OnBounceNeeded : UnityEvent<Transform>
{
}

[System.Serializable]
public class OnEaseInBounceNeeded : UnityEvent<Transform>
{
}

public class Game_Grid : MonoBehaviour
{
    [SerializeField] UnityEvent onGameStateChange;
    [SerializeField] UnityEvent onCorrectAnswer;
    [SerializeField] OnObjectiveChange onObjChange;
    [SerializeField] OnBounceNeeded onBounceNeeded;
    [SerializeField] OnEaseInBounceNeeded onEaseInBounceNeeded;
    [SerializeField] Game_Settings _gameSettings;
    [SerializeField] Grid_cell _cell;
    [SerializeField] float _cellOffset;
    [SerializeField] float _gridPosYOffset;
    Transform _gridTransform;
    List<Grid_cell> _allCells;
    List<Sprite> _spriteSet;
    List<Sprite> _spriteSetForAnswers;
    List<Sprite> _knownSprites;
    int _answerID;
    int _stageCounter = 0;

    int _rowsCount;
    int _colsCount;

    void Start()
    {
        _gridTransform = this.gameObject.transform;
        _rowsCount = _gameSettings.GridRowsStart + _gameSettings.GridRowsIncome * _gameSettings.DifficultyLevels - 1;
        _colsCount = _gameSettings.GridColsStart + _gameSettings.GridColsIncome * _gameSettings.DifficultyLevels;
        
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _allCells = new List<Grid_cell>();
        _spriteSet = new List<Sprite>(_gameSettings.SriteSet);
        _knownSprites = new List<Sprite>();
        _stageCounter = 0;

        for (int i = 0; i < _rowsCount; i++)
        {
            for (int j = 0; j < _colsCount; j++)
            {
                Grid_cell _cellTemp;

                _cellTemp = Instantiate(_cell, new Vector3(_gridTransform.position.x + _cellOffset * j, _gridTransform.position.y - _cellOffset * i, 0), Quaternion.Euler(Vector3.zero), _gridTransform);
                _allCells.Add(_cellTemp);
                _cellTemp.gameObject.SetActive(false);
            }
        }

        float gridW = _rowsCount;
        float gridH = _colsCount;

        _gridTransform.position = new Vector3(-gridW / 2 - _cellOffset / 2, gridH / 2 + _cellOffset / 2 - _gridPosYOffset, 0);

        NextStage();
    }

    public void ClearGrid()
    {
        foreach (var cell in _allCells)
        {
            Destroy(cell.gameObject, .1f);
        }

        onGameStateChange.Invoke();

        GenerateGrid();
    }

    void NextStage()
    {
        int _levelRows = _gameSettings.GridRowsStart + _gameSettings.GridRowsIncome * _stageCounter;
        int _levelCols = _gameSettings.GridColsStart + _gameSettings.GridColsIncome * _stageCounter;

        _spriteSetForAnswers = new List<Sprite>();
        _spriteSet = new List<Sprite>(_gameSettings.SriteSet);

        for (int i = 0; i < _levelRows * _levelCols; i++)
        {
            Sprite _spriteTemp = _spriteSet[Random.Range(0, _spriteSet.Count)];

            _allCells[i].gameObject.SetActive(true);
            _allCells[i].Initialize(_spriteTemp, i);
            _spriteSet.Remove(_spriteTemp);

            if(!_knownSprites.Contains(_spriteTemp))
            {                
                _spriteSetForAnswers.Add(_spriteTemp);
            }
            if(_stageCounter == 0)
            {
                onBounceNeeded.Invoke(_allCells[i].transform);
            }
        }

        _stageCounter++;

        ChooseNewAnswer();
    }

    void ChooseNewAnswer()
    {
        onCorrectAnswer.RemoveAllListeners();

        int _randNumb = Random.Range(0, _spriteSetForAnswers.Count);
        Sprite _spriteTemp = _spriteSetForAnswers[_randNumb];
        Grid_cell _cellTemp = _allCells.Find(x => x.SpriteImage.Equals(_spriteTemp));    

        onCorrectAnswer.AddListener(_cellTemp.ShootStars);
        _answerID = _cellTemp.ID;
        _knownSprites.Add(_spriteSetForAnswers[_randNumb]);
        _spriteSetForAnswers.Remove(_spriteSetForAnswers[_randNumb]);

        onObjChange.Invoke("Find " + _spriteTemp.name, _stageCounter);
    }

    public void IsCorrectCell(int id)
    {
        if(id == _answerID)
        {
            onCorrectAnswer.Invoke();
            onBounceNeeded.Invoke(_allCells[id].transform.Find("Grid_cell_sprite").transform);

            if(_stageCounter < _gameSettings.DifficultyLevels)
            {
                NextStage();
            }
            else
            {
                onGameStateChange.Invoke();
                foreach (var _cell in _allCells)
                {
                    _cell.enabled = false;
                }
            }
        }
        else
        {
            onEaseInBounceNeeded.Invoke(_allCells[id].transform.Find("Grid_cell_sprite").transform);
        }
    }
}
