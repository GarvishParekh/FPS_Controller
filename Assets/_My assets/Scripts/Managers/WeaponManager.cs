using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform weaponThrowTransform;

    private void OnEnable()
    {
        ActionManager.OnWeaponPicked += OnWeaponPickup;
        ActionManager.OnThroWeapon += ThrowWeapon;
    }

    private void OnDisable()
    {
        ActionManager.OnWeaponPicked -= OnWeaponPickup;
        ActionManager.OnThroWeapon -= ThrowWeapon;
    }

    private void Update()
    {
        if (weaponData.equippedWeapon == WeaponID.NULL) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowWeapon();
        }
    }

    private void OnWeaponPickup(WeaponID weaponID, string weaponName)
    {
        weaponData.equippedWeapon = weaponID;
        weaponData.equippedName = weaponName;
    }

    private void ThrowWeapon()
    {
        GameObject weaponPrefab = weaponData.weaponDatabase[(int)weaponData.equippedWeapon].weaponPrefab;
        GameObject spwanedWeapon = Instantiate(weaponPrefab, weaponThrowTransform.position, Quaternion.identity);
        spwanedWeapon.GetComponent<Rigidbody>().velocity = weaponThrowTransform.forward * 5;
        ActionManager.OnWeaponPicked(WeaponID.NULL, "Hands");
    }
}
