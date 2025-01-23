using System;
using System.Collections.Generic;
using UnityEngine;

public class MainEnvironmentChanger : MonoBehaviour
{
    [SerializeField] private List<Environment> _ground;
    [SerializeField] private List<Environment> _nearMountains;
    [SerializeField] private List<Environment> _farMountains;
    [SerializeField] private List<Environment> _nearClouds;
    [SerializeField] private List<Environment> _farClouds;

    private EnvironmentChanger _groundChanger;
    private EnvironmentChanger _nearMountainsChanger;
    private EnvironmentChanger _farMountainsChanger;
    private EnvironmentChanger _nearCloudsChanger;
    private EnvironmentChanger _farCloudsChanger;

    public event Action GroundChanged;

    private void Awake()
    {
        _groundChanger = new EnvironmentChanger(_ground);
        _nearMountainsChanger = new EnvironmentChanger(_nearMountains);
        _farMountainsChanger = new EnvironmentChanger(_farMountains);
        _nearCloudsChanger = new EnvironmentChanger(_nearClouds);
        _farCloudsChanger = new EnvironmentChanger(_farClouds);
    }

    private void Start()
    {
        _groundChanger.GroundChanged += OnGroundChanged;
    }

    private void OnGroundChanged()
    {
        GroundChanged?.Invoke();
    }
}