using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private UIAnimator _animator;
    [SerializeField] private UIAnimator _pauseUI;
    [SerializeField] private Upgrader _upgrader;
    [SerializeField] private Button _flyUpgrade;
    [SerializeField] private Button _damageUpgrade;
    [SerializeField] private Button _fireRateUpgrade;
    [SerializeField] private Button _healthUpgrade;
    [SerializeField] private Button _reloadUpgrade;
    [SerializeField] private Button _maxBulletsUpgrade;
    [SerializeField] private Button _toPauseMenu;

    private void OnEnable()
    {
        _upgrader.FlyForceOnMaxLevel += OnFlyForceOnMaxLevel;
        _upgrader.FireRateOnMaxLevel += OnFireRateOnMaxLevel;
        _upgrader.DamageOnMaxLevel += OnDamageOnMaxLevel;
        _upgrader.HealthOnMaxLevel += OnHealthOnMaxLevel;
        _upgrader.ReloadOnMaxLevel += OnReloadOnMaxLevel;
        _upgrader.BulletsOnMaxLevel += OnBulletsOnMaxLevel;
    }

    public void ShowPauseMenu()
    {
        _animator.Hide();
        _pauseUI.Show();
    }

    public void IsHasNotAcceptedLevel()
    {
        if (UpgradeUtils.NotAcceptedPlayerLevel == 0)
            ShowPauseMenu();
    }

    private void OnFlyForceOnMaxLevel()
    {
        _flyUpgrade.gameObject.SetActive(false);
    }

    private void OnFireRateOnMaxLevel()
    {
        _fireRateUpgrade.gameObject.SetActive(false);
    }

    private void OnDamageOnMaxLevel()
    {
        _damageUpgrade.gameObject.SetActive(false);
    }

    private void OnHealthOnMaxLevel()
    {
        _healthUpgrade.gameObject.SetActive(false);
    }

    private void OnReloadOnMaxLevel()
    {
        _reloadUpgrade.gameObject.SetActive(false);
    }

    private void OnBulletsOnMaxLevel()
    {
        _maxBulletsUpgrade.gameObject.SetActive(false);
    }
}