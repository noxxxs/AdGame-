using TMPro;
using UnityEngine;

public class UnitCounter : MonoBehaviour
{
    public static UnitCounter Instance;
    public TextMeshProUGUI _text;
    public int _unitCounter = 0;

    public float UpdateTime;
    private float _timeElapsed;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeElapsed = 0;
    }

    void Update()
    {
        _timeElapsed += Time.time;

        if (_timeElapsed > UpdateTime)
        {
            UpdateText();
            _timeElapsed = 0;
        }
    }

    void UpdateText()
    {
        _text.text = "Units:" + _unitCounter.ToString();
    }
}
