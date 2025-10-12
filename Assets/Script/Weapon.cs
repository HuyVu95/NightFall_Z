using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon : MonoBehaviour
{
        public string weaponName;
        public GameObject weaponPrefab;
        public Sprite weaponIcon;
        public int maxAmmo;
        public float fireRate;
        public float reloadTime;
}
