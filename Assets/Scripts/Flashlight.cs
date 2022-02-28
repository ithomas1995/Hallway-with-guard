using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light m_Light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public bool allowButtonHold;
    public int bulletsPerTap, magazineSize;
    public bool shooting, readyToShoot, reloading;
    public Camera MainCamera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask Enemy;
    int bulletsLeft, bulletsShot;
    public AudioClip shootClip;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource= GetComponent<AudioSource>();
        
    }

    public void PlaySound(AudioClip clip)
{
    audioSource.PlayOneShot(clip);
}

    // Update is called once per frame
    void Update()
    {
        m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness);

        if(drainOverTime == true && m_Light.enabled == true)
        {
            
            if (m_Light.intensity > minBrightness)
            {
                m_Light.intensity -= Time.deltaTime * (drainRate/300);
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            m_Light.enabled = !m_Light.enabled;
        }
        if(Input.GetKeyDown(KeyCode.R) || Input.GetKey("joystick button 2"))
        {
            ReplaceBattery(.3f);
        }

        MyInput();
    }

    public void ReplaceBattery(float amount)
    {
        m_Light.intensity += amount;
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0) && m_Light.enabled;
        else shooting = (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown("joystick button 5"));

        

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

    }

    private void Shoot()
    {
        readyToShoot = false;

        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out rayHit, range, Enemy))
        {
            Debug.Log(rayHit.collider.name);
            PlaySound(shootClip);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<EnemyAi>().TakeDamage(damage);
                    
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);

    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
