using UnityEngine;
using System.Collections;

/// <summary>
/// Handles shooting for multiple weapons.
/// </summary>
public class Shoot : MonoBehaviour
{
    public GUITexture reticleGuiTexture;
    public string fireButton = "Fire1";

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public GameObject bullet;
        public Texture textureReticle;
        public AudioClip noise;
		[System.NonSerialized]
		public GUITexture icon; // hide from the inspector
    }
    public Weapon[] weapons;
    int index = 0;            // which weapon is selected
    bool usingMultiWeaponGUI;
	GameObject weaponIconGui; // just used for organization in the game world

	void Start()
    {
        if (weapons == null || weapons.Length == 0)
        {
            print("Need weapons set for " + this + "!");
        } else
        {
            usingMultiWeaponGUI = weapons.Length > 1;
            if (usingMultiWeaponGUI)
            {
                weaponIconGui = new GameObject("weapon icon GUI");
                float widthSoFar = 0;
                for (int i = 0; i < weapons.Length; ++i)
                {
                    Texture wTex = weapons [i].textureReticle;
					GameObject iconObject = new GameObject("weapon " + (i + 1));
					iconObject.transform.localScale = Vector3.zero; // zero scale uses pixelInset for size
					iconObject.transform.parent = weaponIconGui.transform;
					weapons [i].icon = iconObject.AddComponent<GUITexture>();
					weapons [i].icon.texture = wTex;
					weapons [i].icon.pixelInset = new Rect(widthSoFar, 0, wTex.width / 2, wTex.height / 2);
                    widthSoFar += wTex.width;
                }
            }
            SetWeapon(0);
        }
    }

    public void SetWeapon(int index)
    {
        if (usingMultiWeaponGUI)
        {
            // make the currently used weapon icon smaller
            Texture wTex = weapons[this.index].icon.texture;
			Rect iconArea = weapons[this.index].icon.pixelInset;
			weapons[this.index].icon.pixelInset = new Rect(iconArea.x, iconArea.y, wTex.width / 2, wTex.height / 2);
        }
        // change the currently used weapon
        this.index = index;
        if (reticleGuiTexture != null)
        {
            reticleGuiTexture.texture = weapons [index].textureReticle;
        }
        if (usingMultiWeaponGUI)
        {
            // make the currently used weapon icon bigger
			Texture wTex = weapons[this.index].icon.texture;
			Rect iconArea = weapons[this.index].icon.pixelInset;
			weapons[this.index].icon.pixelInset = new Rect(iconArea.x, iconArea.y, wTex.width, wTex.height);
		}
	}
	
	void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            GameObject go = (GameObject)Instantiate(
                weapons [index].bullet, transform.position, transform.rotation);
            if(weapons[index].noise != null)
            {
                PlaySound.Play(weapons[index].noise, go.transform);
            }
        }
		if(usingMultiWeaponGUI)
		{
	        // 1 through 9 can change the weapon
	        for (int i = 0; i < 9; ++i)
	        {
	            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && weapons.Length > i)
	            {
	                SetWeapon(i);
	            }
	        }
		}
    }
}
