using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public GUIText ScoreCurrent;
    public GUIText ScoreBest;

    private int _scoreCurrent, _scoreBest;

    public void AddPoints(int points)
    {
        _scoreCurrent += points;
        if (_scoreCurrent > _scoreBest)
        {
            _scoreBest = _scoreCurrent;
        }
    }

    public void ResetPoints()
    {
        _scoreCurrent = 0;
    }

	// Use this for initialization
	void Start ()
	{
	    _scoreCurrent = 0;
	    _scoreBest = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ScoreCurrent.text = "" + _scoreCurrent;
	    ScoreBest.text = "" + _scoreBest;
	}
}
