using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OriginalCube : BaseCube
{
    [SerializeField] private List<DiseSide> _sidesData;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _matInstance;


    private float[,] values = new float[6, 6]
    {
        //some hardcode. I know, texture is trash, but im lazy
        //equals material texture offset to top direction
        //Top     Down    Forward Back    Left    Right   values
        { 0.330f, 0.834f, 0.500f, 0.167f, 0.667f, 1.000f  }, //1
        { 0.834f, 0.330f, 1.000f, 0.667f, 0.167f, 0.500f  }, //2
        { 0.167f, 0.667f, 0.330f, 1.000f, 0.500f, 0.834f  }, //3
        { 0.500f, 1.000f, 0.667f, 0.330f, 0.834f, 0.167f  }, //4
        { 0.667f, 0.167f, 0.834f, 0.500f, 1.000f, 0.330f  }, //5
        { 1.000f, 0.500f, 0.167f, 0.834f, 0.330f, 0.667f  }  //6
    };

    private Dictionary<string, int> NameToIndex = new Dictionary<string, int>() 
    {
        { "Left", 4},
        { "right", 5},
        { "down", 1},
        { "top", 0},
        { "back", 3},
        { "forward", 2},
    };

    /// <summary>
    /// Entry point
    /// </summary>
    public void Configure()
    {
        _matInstance = _renderer.material;
    }



    /// <summary>
    /// Set dots view in dice
    /// </summary>
    /// <param name="topName"></param>
    /// <param name="value"></param>
    public void SetVisualValues(string topName, int value)
    {
        _matInstance.SetFloat("_Offset", values[value - 1, NameToIndex[topName]]);
    }
}

[System.Serializable]
public struct DiseSide
{
    public string Name;
    public TextMeshProUGUI Text;
}

