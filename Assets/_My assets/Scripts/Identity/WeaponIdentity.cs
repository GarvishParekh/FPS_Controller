using UnityEngine;

public class WeaponIdentity : MonoBehaviour, IPickable
{
    [SerializeField] private WeaponID myWeaponID;
    [SerializeField] private WeaponData weaponData;

    Weapon myWeapon;

    private void Awake()
    {
        foreach (Weapon weapon in weaponData.weaponDatabase)
        {
            if (weapon.weaponID == myWeaponID)
            {
                myWeapon = weapon;
            }
        }
    }

    public void OnPick()
    {
        //Debug.Log($"Player picked: {myWeapon.weaponName}");
        ActionManager.OnWeaponPicked?.Invoke(myWeapon); 
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    public WeaponID GetWeaponID()
    {
        return myWeaponID;
    }
}
