using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header ("<b>Scriptable")]
    [SerializeField] WeaponData weaponData;
    [SerializeField] InputData inputData;

    [Header ("<b>Components")]
    [SerializeField] Transform weaponThrowTransform;
    [SerializeField] Transform weaponHolder;
    [SerializeField] List<WeaponIdentity> inHandWeapons = new List<WeaponIdentity>();


    float swayX;
    float swayY;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = weaponHolder.localRotation;
    }

    private void OnEnable()
    {
        ActionManager.OnThrowWeapon += ThrowWeapon;
        ActionManager.OnWeaponPicked += OnWeaponPickup;
    }

    private void OnDisable()
    {
        ActionManager.OnThrowWeapon -= ThrowWeapon;
        ActionManager.OnWeaponPicked -= OnWeaponPickup;
    }

    private void FixedUpdate() => WeaponSway();

    private void Update()
    {
        if (weaponData.equippedWeapon == WeaponID.NULL) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowWeapon(true);
        }
    }

    private void OnWeaponPickup(Weapon weapon)
    {
        weaponData.equippedWeapon = weapon.weaponID;
        weaponData.equippedName = weapon.weaponName;

        foreach (WeaponIdentity inHandWeapon in inHandWeapons)
        {
            if (inHandWeapon.GetWeaponID() == weapon.weaponID)
            {
                inHandWeapon.gameObject.SetActive(true);
            }
            else inHandWeapon.gameObject.SetActive(false);
        }
    }

    private void ThrowWeapon(bool check)
    {
        GameObject weaponPrefab = weaponData.weaponDatabase[(int)weaponData.equippedWeapon].weaponPrefab;
        GameObject spwanedWeapon = Instantiate(weaponPrefab, weaponThrowTransform.position, Quaternion.identity);
        spwanedWeapon.GetComponent<Rigidbody>().velocity = weaponThrowTransform.forward * 5;
        if (check) ActionManager.OnWeaponPicked(weaponData.weaponDatabase[(int)WeaponID.NULL]);
    }

   
    private void WeaponSway()
    {
        if (!weaponData.addSway) return;

        // Invert Y axis for more natural sway
        swayX = Mathf.Clamp(-inputData.yMouse * weaponData.swayAmount, -weaponData.maxSway, weaponData.maxSway);
        swayY = Mathf.Clamp(inputData.xMouse * weaponData.swayAmount, -weaponData.maxSway, weaponData.maxSway);

        // Target rotation relative to original
        Quaternion targetRotation = Quaternion.Euler(swayX, swayY, 0);
        weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, originalRotation * targetRotation, Time.deltaTime * weaponData.smoothAmount);
    }
}
