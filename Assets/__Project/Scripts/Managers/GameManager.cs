using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    public float TickPerSecond = 20f;
    public float TicksForDoom = 12000f;
    public float TicksPerDay = 40f;

    float _time;
    long _ticks = 0;

    List<IOnTickHandler> _jobs = new List<IOnTickHandler>();

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time > 1f / TickPerSecond)
        {
            _ticks++;
            _time -= 1f/ TickPerSecond;

            foreach (IOnTickHandler jv in _jobs.ToArray()) jv.OnTick();
        }
    }

    public void Register(IOnTickHandler jv) => _jobs.Add(jv);
    public void Unregister(IOnTickHandler jv) => _jobs.Remove(jv);

    public int GetDaysPast() => Mathf.FloorToInt(_ticks / TicksPerDay);
    public int GetRemainingDays() => Mathf.FloorToInt((TicksForDoom - _ticks) / TicksPerDay);

    public long GetTicks() => _ticks;
    public float GetDoomPercentage() => 1f - (_ticks / TicksForDoom);

    public void AddTicksToDoom(float ticks) => TicksForDoom += ticks;
}