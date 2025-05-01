using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [Header ("<b>Scriptable")]
    [SerializeField] PlayerData playerData;
    [SerializeField] WeaponData weaponData;
    [SerializeField] InputData inputData;

    [Header ("<b>Components")]
    [SerializeField] Transform weaponThrowTransform;
    [SerializeField] Transform weaponHolder;
    [SerializeField] Transform walkSwayHolder;
    [SerializeField] Transform idelSwayHolder;
    [SerializeField] List<WeaponIdentity> inHandWeapons = new List<WeaponIdentity>();

    // weapon sway 
    float swayX;
    float swayY;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = weaponHolder.localRotation;
        weaponData.swayTimer = 0;
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

    private void FixedUpdate()
    {
        WeaponSway();
        WalkingSwayAnimation();
        IdelSway();
    }

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
        swayX = Mathf.Clamp(-inputData.yHead * weaponData.swayAmount, -weaponData.maxSway, weaponData.maxSway);
        swayY = Mathf.Clamp(inputData.xHead * weaponData.swayAmount, -weaponData.maxSway, weaponData.maxSway);

        // Target rotation relative to original
        Quaternion targetRotation = Quaternion.Euler(swayX, swayY, 0);
        weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, originalRotation * targetRotation, Time.deltaTime * weaponData.smoothAmount);
    }

    float sine;
    Vector3 newPos = Vector3.zero;
    private void WalkingSwayAnimation()
    {
        //if (playerData.playerBlocked == PlayerBlocked.IS_BLOCKED) return;
        if (!playerData.isMoving) return;

        weaponData.swayTimer += Time.deltaTime * GetWalkingSwaySpeed();
        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                newPos.z = 0;
                break;
            case SprintingValue.IS_SPRINTING:
                newPos.z = sine/5;
                break;
        }


        sine = Mathf.Sin(weaponData.swayTimer) * weaponData.swayheight;
        newPos.y = sine;
        walkSwayHolder.localPosition = newPos;
    }

    private float GetWalkingSwaySpeed()
    {
        float returnSpeed = 0;
        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                returnSpeed = weaponData.normalSwaySpeed;
                break;
            case SprintingValue.IS_SPRINTING:
                returnSpeed = weaponData.sprintingSwaySpeed;
                break;
        }
        return returnSpeed;
    }

    Vector3 idelSwayNewPos = Vector3.zero;
    float newValueTimer = 2f;
    float newValueTime = 0f;

    float xPos = 0;
    float yPos = 0;
    private void IdelSway()
    {
        if (newValueTime > newValueTimer)
        {
            xPos = Random.Range(-0.01f, 0.01f);
            yPos = Random.Range(-0.04f, 0.04f);
            idelSwayNewPos = new Vector3(xPos, yPos, 0);
            newValueTime = 0;
        }
        else newValueTime += Time.deltaTime;

        idelSwayHolder.localPosition = Vector3.Lerp(idelSwayHolder.localPosition, idelSwayNewPos, 0.15f * Time.deltaTime);

    }
}
