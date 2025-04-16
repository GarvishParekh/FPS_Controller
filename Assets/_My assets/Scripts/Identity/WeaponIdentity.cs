using UnityEngine;

public class WeaponIdentity : MonoBehaviour, IPickable
{
    [SerializeField] private Weapon weapon;
    

    public void OnPick()
    {
        Debug.Log($"Player picked: {weapon.weaponName}");
        ActionManager.OnWeaponPicked?.Invoke(weapon.weaponID, weapon.weaponName);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
