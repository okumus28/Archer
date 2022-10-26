using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    float attackSpeed;
    float _attackSpeed;

    int damage;

    [Range(1,13)]
    int multiArrow = 1;

    Attribiutes atr;

    private void Awake()
    {
        atr = transform.GetComponent<Attribiutes>();
    }

    // Update is called once per frame
    void Update()
    {
        _attackSpeed -= Time.deltaTime;

        if (_attackSpeed <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                multiArrow = atr.multiArrowLevel;
                attackSpeed = 1 - 0.05f * (atr.speedLevel - 1);

                MultiArrow(multiArrow);
                _attackSpeed = attackSpeed;
            }
        }
    }

    void CreateArrowAngle(float angle)
    {
        damage = atr._damage;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z + angle)));
        bullet.GetComponent<Bullet>().damage = damage;
    }

    void MultiArrow(int value)
    {
        switch (value)
        {
            case 1:
                CreateArrowAngle(0);
                break;
            case 2:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                break;
            case 3:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                break;
            case 4:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                CreateArrowAngle(3);
                CreateArrowAngle(-3);
                break;
            case 5:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                CreateArrowAngle(4);
                CreateArrowAngle(-4);
                break;
            case 6:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                CreateArrowAngle(3);
                CreateArrowAngle(-3);
                CreateArrowAngle(5);
                CreateArrowAngle(-5);
                break;
            case 7:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                CreateArrowAngle(4);
                CreateArrowAngle(-4);
                CreateArrowAngle(6);
                CreateArrowAngle(-6);
                break;
            case 8:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                CreateArrowAngle(3);
                CreateArrowAngle(-3);
                CreateArrowAngle(5);
                CreateArrowAngle(-5);
                CreateArrowAngle(7);
                CreateArrowAngle(-7);
                break;
            case 9:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                CreateArrowAngle(4);
                CreateArrowAngle(-4);
                CreateArrowAngle(6);
                CreateArrowAngle(-6);
                CreateArrowAngle(8);
                CreateArrowAngle(-8);
                break;
            case 10:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                CreateArrowAngle(3);
                CreateArrowAngle(-3);
                CreateArrowAngle(5);
                CreateArrowAngle(-5);
                CreateArrowAngle(7);
                CreateArrowAngle(-7);
                CreateArrowAngle(9);
                CreateArrowAngle(-9);
                break;
            case 11:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                CreateArrowAngle(4);
                CreateArrowAngle(-4);
                CreateArrowAngle(6);
                CreateArrowAngle(-6);
                CreateArrowAngle(8);
                CreateArrowAngle(-8);
                CreateArrowAngle(10);
                CreateArrowAngle(-10);
                break;
            case 12:
                CreateArrowAngle(1);
                CreateArrowAngle(-1);
                CreateArrowAngle(3);
                CreateArrowAngle(-3);
                CreateArrowAngle(5);
                CreateArrowAngle(-5);
                CreateArrowAngle(7);
                CreateArrowAngle(-7);
                CreateArrowAngle(9);
                CreateArrowAngle(-9);
                CreateArrowAngle(11);
                CreateArrowAngle(-11);
                break;
            case 13:
                CreateArrowAngle(0);
                CreateArrowAngle(2);
                CreateArrowAngle(-2);
                CreateArrowAngle(4);
                CreateArrowAngle(-4);
                CreateArrowAngle(6);
                CreateArrowAngle(-6);
                CreateArrowAngle(8);
                CreateArrowAngle(-8);
                CreateArrowAngle(10);
                CreateArrowAngle(-10);
                CreateArrowAngle(12);
                CreateArrowAngle(-12);
                break;

            default:
                break;
        }
    }
}
