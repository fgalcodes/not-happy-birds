using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    public Camera cam;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //guardem posició de la càmera
        var dist = mousePos - Bullet.transform.position; //distància entre el click i la bala
        var ang = Mathf.Atan2(dist.y, dist.x) * 180f / Mathf.PI + offset;
        transform.rotation = Quaternion.Euler(0, 0, ang); //angle que s'ha de rotar

        //Debug.Log(mousePos);

        if(Input.GetMouseButton(0))
        {
            ProjectileSpeed += Time.deltaTime * 4;  //cada frame s'ha de fer 4 cops més gran
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity); //On s'instancia?
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(dist.x, dist.y).normalized * ProjectileSpeed; //quina velocitat ha de tenir la bala? s'ha de fer alguna cosa al vector direcció?
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            transform.localScale.y,
            transform.localScale.z);
    }
}
