using TMPro;
using UnityEngine;

public class PlayerBulletsView : MonoBehaviour
{
    private const string Reloading = "Reloading...";

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private PlayerWeapon _playerWeapon;

    private void OnEnable()
    {
        _playerWeapon.Reloaded += OnReload;
        _playerWeapon.BulletsValueChanged += OnBulletsValueChanged;
    }

    private void OnDisable()
    {
        _playerWeapon.Reloaded -= OnReload;
        _playerWeapon.BulletsValueChanged -= OnBulletsValueChanged;
    }

    private void Start()
    {
        ShowFullAmmo();
    }

    private void ShowFullAmmo()
    {
        _text.text = _playerWeapon.CurrentBulletsValue + SignUtils.Slash + _playerWeapon.MaxBulletsValue;
    }

    private void ShowReloading()
    {
        _text.text = Reloading;
    }

    private void OnReload(bool isReloading)
    {
        if (isReloading)
            ShowReloading();
        else
            ShowFullAmmo();
    }

    private void OnBulletsValueChanged(int currentValue)
    {
        _text.text = currentValue + SignUtils.Slash + _playerWeapon.MaxBulletsValue;
    }
}