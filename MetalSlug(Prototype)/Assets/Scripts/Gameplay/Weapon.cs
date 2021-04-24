using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform player;

    private void OnEnable()
    {
        EventManager.onButton += Shoot;
    }

    private void OnDisable()
    {
        EventManager.onButton -= Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.OnShootEvent();
        /*if(Input.GetKey("space")){
            Shoot();
        }*/

        bool playerRight = player.transform.rotation.eulerAngles.y < 180;
       
        if( Input.GetKey("t") && Input.GetKey("h") && playerRight){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * 45);
        } else  if( Input.GetKey("t") && Input.GetKey("f") && !playerRight){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * 135);
        } else if(Input.GetKey("t")){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * 90);  
        } else if(Input.GetKey("g") && Input.GetKey("h") && playerRight){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * -45);  
        } else if(Input.GetKey("g") && Input.GetKey("f") && !playerRight){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * -135);  
        }  else if(Input.GetKey("g")){
            firePoint.transform.rotation = Quaternion.identity;
            firePoint.transform.Rotate (Vector3.forward * -90);  
        }  else {
            if(playerRight){
                firePoint.transform.rotation = Quaternion.identity;
            }else {
                firePoint.transform.rotation = Quaternion.identity;
                firePoint.transform.Rotate (Vector3.forward * 180);
            }
        }
        
    }

    void Shoot(){
        if (Input.GetKeyDown("space"))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //Shoot();
        }
    }
}
