using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShuffleScreen : MonoBehaviour
{
    [SerializeField] private ValueViewHolder _valueViewPfb;
    [SerializeField] private ScrollRect _container;
    [SerializeField] private BonesShuffler _shuffler;
    [SerializeField] private Button _shuffleBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private Button _configureBtn;
    [SerializeField] private List<ValueViewHolder> _fakedValuesView;
    [SerializeField] private ValueViewHolder _diceCount;

    private List<int> FakedValues => _fakedValuesView.ConvertAll(x => x.CurrentValue);

    public void Start()
    {
        _shuffleBtn.onClick.AddListener(Shuffle);
        _configureBtn.onClick.AddListener(Configure);
        _menuBtn.onClick.AddListener(Reload);
        _diceCount.OnValueChanget += RebuildDiceCount;
        _diceCount.Configure();
    }

    private void OnDestroy()
    {
        _diceCount.OnValueChanget -= RebuildDiceCount;
    }

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

    private void RebuildDiceCount(int obj)
    {
        if (_fakedValuesView.Count > obj)
        {
            ValueViewHolder holder = _fakedValuesView[_fakedValuesView.Count - 1];
            _fakedValuesView.Remove(holder);
            Destroy(holder.gameObject);
        }
        else if(_fakedValuesView.Count < obj)
        {
            ValueViewHolder holder = Instantiate(_valueViewPfb, _container.content);
            holder.Configure();
            _fakedValuesView.Add(holder);
        }
    }

    private void Configure()
    {
        if (_diceCount.CurrentValue == 0)
        {
            Debug.LogError("he he, no dice");
            return;
        }
        _menuBtn.gameObject.SetActive(true);
        _configureBtn.gameObject.SetActive(false);
        _shuffleBtn.gameObject.SetActive(true);
        foreach (var view in _fakedValuesView)
        {
            view.gameObject.SetActive(false);
        }
        _diceCount.gameObject.SetActive(false);
        _shuffler.Build(_diceCount.CurrentValue);
    }

    private void Shuffle()
    {
        _shuffler.Shuffle(FakedValues);
    }
}
