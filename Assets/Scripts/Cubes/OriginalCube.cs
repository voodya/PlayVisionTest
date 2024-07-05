using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OriginalCube : BaseCube
{
    [SerializeField] private List<DiseSide> _sidesData;
    private Dictionary<string, TextMeshProUGUI> _sides = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<int, TextMeshProUGUI> _sidesInt = new Dictionary<int, TextMeshProUGUI>();


    /// <summary>
    /// Entry point
    /// </summary>
    public void Configure()
    {
        for (int i = 0; i < _sidesData.Count; i++)
        {
            _sides.Add(_sidesData[i].Name, _sidesData[i].Text);
            _sidesInt.Add(i, _sidesData[i].Text);
        }

    }

    /// <summary>
    /// Set dots view in dice
    /// </summary>
    /// <param name="topName"></param>
    /// <param name="value"></param>
    public void SetVisualValues(string topName, int value)
    {
        //better use Texture, but i'm lazy :)

        foreach (var side in _sides)
        {
            side.Value.text = " ";
        }

        _sides[topName].text = value.ToString();
    }

    public void SetVisualValues(int topId, int value)
    {
        //better use Texture, but i'm lazy :)

        foreach (var side in _sidesInt)
        {
            side.Value.text = " ";
        }

        _sidesInt[topId].text = value.ToString();
    }
}

[System.Serializable]
public struct DiseSide
{
    public string Name;
    public TextMeshProUGUI Text;
}

