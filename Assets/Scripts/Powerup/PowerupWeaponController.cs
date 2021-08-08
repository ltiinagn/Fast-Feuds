using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWeaponController : MonoBehaviour
{
    public GameConstants gameConstants;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UsePowerup() {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
