using System;
using System.Collections.Generic;
using UnityEngine;

public class MainEnvironmentChanger : MonoBehaviour
{
    [SerializeField] private List<EnvironmentMover> _ground;
    [SerializeField] private List<EnvironmentMover> _nearMountains;
    [SerializeField] private List<EnvironmentMover> _farMountains;
    [SerializeField] private List<EnvironmentMover> _nearClouds;
    [SerializeField] private List<EnvironmentMover> _farClouds;

    private EnvironmentChanger _groundChanger;

    public event Action GroundChanged;

    private void Awake()
    {
        _groundChanger = new EnvironmentChanger(_ground);
        EnvironmentChanger nearMountainsChanger = new EnvironmentChanger(_nearMountains);
        EnvironmentChanger farMountainsChanger = new EnvironmentChanger(_farMountains);
        EnvironmentChanger nearCloudsChanger = new EnvironmentChanger(_nearClouds);
        EnvironmentChanger farCloudsChanger = new EnvironmentChanger(_farClouds);
    }

    private void OnEnable()
    {
        _groundChanger.GroundChanged += OnGroundChanged;
    }

    private void OnDisable()
    {
        _groundChanger.GroundChanged -= OnGroundChanged;
    }

    private void OnGroundChanged()
    {
        GroundChanged?.Invoke();
    }
}