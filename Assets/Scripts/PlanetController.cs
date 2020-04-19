﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject m_sun;
    public float m_height;
    public float m_width;
    public float m_speed;

    public GameObject m_problemSpawner;
    public float m_spawnerRadius;
    public float m_spawnerFrontAngle;
    public float m_spawnerRearAngle;
    public float m_spawnerSpeed;

    public bool m_clockwise;

    Vector3 m_center;
    Vector3 m_direction;
    Vector3 m_position;
    public float m_angle = 90;
    float m_spawnerAngle = 90;
    int m_spawnerDirection = 1; 

    // Start is called before the first frame update
    public void Init()
    {
        m_center = m_sun.transform.position;
        m_position.x = m_center.x + (m_width * Mathf.Cos(m_angle * Mathf.Deg2Rad));
        m_position.y = m_center.y + (m_height * Mathf.Sin(m_angle * Mathf.Deg2Rad));
        m_position.z = 1f;
        this.gameObject.transform.position = m_position;

        if (m_clockwise)
            m_direction = Vector3.right;
        else
            m_direction = Vector3.left;

        m_position = transform.position;
        Vector3 spawnerVec;
        spawnerVec.x = m_position.x + (m_spawnerRadius * Mathf.Cos(m_spawnerAngle * Mathf.Deg2Rad));
        spawnerVec.y = m_position.y + (m_spawnerRadius * Mathf.Sin(m_spawnerAngle * Mathf.Deg2Rad));
        spawnerVec.z = m_position.z;
        m_problemSpawner.transform.position = spawnerVec;
    }

    // Update is called once per frame
    void Update()
    {
        m_angle += m_speed;
        
        m_position.x = m_center.x + (m_width * Mathf.Cos(m_angle * Mathf.Deg2Rad));
        m_position.y = m_center.y + (m_height * Mathf.Sin(m_angle * Mathf.Deg2Rad));
        this.gameObject.transform.position = m_position;

        UpdateSpawner();
    }

    void UpdateSpawner()
    {
        Vector3 spawnerVec = m_problemSpawner.transform.position - m_position;
        Vector3 sunToPlanet = m_position - m_center;

        spawnerVec.Normalize();
        sunToPlanet.Normalize();

        spawnerVec.z = 0;
        sunToPlanet.z = 0;

        float angle = Vector3.SignedAngle(sunToPlanet, spawnerVec, Vector3.forward);
        if (angle <= m_spawnerFrontAngle)
            m_spawnerDirection = 1;
        else if (angle >= m_spawnerRearAngle)
            m_spawnerDirection = -1;

        m_spawnerAngle += m_spawnerDirection*m_spawnerSpeed;

        spawnerVec.x = m_position.x + (m_spawnerRadius * Mathf.Cos(m_spawnerAngle * Mathf.Deg2Rad));
        spawnerVec.y = m_position.y + (m_spawnerRadius * Mathf.Sin(m_spawnerAngle * Mathf.Deg2Rad));
        spawnerVec.z = m_position.z;
        m_problemSpawner.transform.position = spawnerVec;
    }

    private void OnDrawGizmos()
    {
        Vector3 spawnerVec = m_problemSpawner.transform.position - m_position;
        Vector3 sunToPlanet = m_position - m_center;

        spawnerVec.Normalize();
        sunToPlanet.Normalize();

        Gizmos.DrawRay(m_position, spawnerVec);
        Gizmos.DrawRay(m_position, sunToPlanet);
    }
}