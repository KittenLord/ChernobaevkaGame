using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float SmoothTime;
    public int PlaneLayer;
    public Transform ShootPoint;
    public Gun Gun;
    public Bullet BulletPrefab;
    public Transform BulletHolder;

    public bool DeactivateMovement = false;
    public bool DeactivateRotation = false;
    public bool DeactivateShooting = false;

    public bool CanInteract = true;

    private Vector3 velocity;

    private bool IsHolding;
    public bool IsReloading;// { get; private set; }

    private int LastShot;
    private float EndReloadTime;
    private float CurrentReloadTime;

    private Rigidbody rb;

    private BulletGenerator bg;
    private MovementUtils mu;



    void Start()
    {
        Gun = new Gun("Gun", 1,         // Ammo
                          false,        // Automatic
                AmmoType.Bullet,        // Ammo Type
                             15,        // Damage
                             0,         // Shooting Speed
                            30,         // Recoil
                             1,         // Max Ammo
                            40,         // Accuracy
                             10,        // Shot Count
                             0,         // Reload Time (in seconds)
                             50         // Bullet speed
        );         


        rb = GetComponent<Rigidbody>();
        bg = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BulletGenerator>();
        mu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MovementUtils>();
    }

    void Update()
    {
        // Movement Detection
        if (!DeactivateMovement)
        {
            var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            mu.MoveInDirection(transform, direction, Speed, ref velocity, SmoothTime);
        }

        // Rotation Detection
        if (!DeactivateRotation)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            Physics.Raycast(ray, out hitinfo, 9999, 1 << PlaneLayer); // 1 << PlaneLayer makes it only react to the object with layer PlaneLayer
            var hitPoint = new Vector3(hitinfo.point.x, transform.position.y, hitinfo.point.z); // Make the target point the same height, as the player, so no falling down will occur

            transform.LookAt(hitPoint);
        }
    }



    private void FixedUpdate() // Executing stuff in FixedUpdate for physics to work correctly
    {
        // Shooting Detection
        if (!DeactivateShooting && Input.GetMouseButton(0))
        {
            if (Gun.Ammo <= 0 && !IsReloading)
            {
                StartReload();
            }
            else
            {
                if ((!IsHolding || Gun.IsAutomatic) && !IsReloading) // If the gun is not automated, simply holding shoot button will not work, you will have to spam
                {
                    if (LastShot >= Gun.ShootingSpeed)
                    {
                        LastShot = 0; // Last shot is the timer for gun's shooting speed - the lower ShootingSpeed parameter is, the faster the gun shoots
                        IsHolding = true;
                        bg.Shoot(Gun, ShootPoint, BulletHolder, rb, new List<string> { "Base", "Player"});
                    }
                }
            }
        }
        else
        {
            IsHolding = false;
        }

        LastShot++; // Shooting speed timer

        if (IsReloading) // Reload timer
        {
            CurrentReloadTime += Time.fixedDeltaTime;
            if (CurrentReloadTime >= EndReloadTime)
            {
                EndReload();
            }
        }
    }


    public void StartReload()
    {
        CurrentReloadTime = Time.time;
        EndReloadTime = CurrentReloadTime + Gun.ReloadTime * Time.fixedDeltaTime * 50; // Converts ReloadTime seconds to fixed frames
        IsReloading = true;
    }

    public void EndReload()
    {
        IsReloading = false;
        Gun.Ammo = Gun.MaxAmmo;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interactable")
        {
            // TODO: Make a "Press [F] to interact" label appear


            //


            // When player presses F and interaction is not locked

            if (Input.GetKey(KeyCode.F) && CanInteract)
            {
                var interactable = other.GetComponent<IInteractable>();
                interactable.Interact();
            }
        }
    }
}
