using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RootMotion.FinalIK.HitReaction;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] bool m_cursorVisibility = false;
    [SerializeField] GameObject m_toiletSucker;
    [SerializeField] Animator m_toiletSuckerAnimator;
    [SerializeField] float m_zOffset;
    [SerializeField] ParticleSystem m_toiletParticle;
    float m_startYPos;
    Plane groundPlane;

    [SerializeField] Transform m_upBound;
    [SerializeField] Transform m_lowerBound;
    [SerializeField] Transform m_leftBound;
    [SerializeField] Transform m_rightBound;

    [Space]
    [SerializeField] WhacAMoleManager m_gm;
    [SerializeField] MeshRenderer m_mesh;
    [SerializeField] Color m_transparentColor;
    Color m_startColor;

    private void Awake()
    {
        m_startColor = m_mesh.materials[0].color;
        m_gm.WAM_Idle.AddListener(delegate { SetTransparent(true); });
        m_gm.WAM_Play.AddListener(delegate { SetTransparent(false); });
    }

    void SetTransparent(bool _yesOrNo)
    {
        m_mesh.materials[0].color = (_yesOrNo) ? m_transparentColor : m_startColor;
    }

    private void Start()
    {
        m_startYPos = m_toiletSucker.transform.position.y;
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        Cursor.visible = m_cursorVisibility;
    }

    void Update()
    {
        // Create a ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Declare a variable to store the hit information
        float hitDistance;
        float _mouseX = 0;
        float _mouseZ = 0;

        // Check if the ray intersects with the ground plane
        if (groundPlane.Raycast(ray, out hitDistance))
        {
            // Get the intersection point
            Vector3 hitPoint = ray.GetPoint(hitDistance);

            // Get the x and z coordinates of the hit point
            _mouseX = hitPoint.x;
            _mouseZ = hitPoint.z;
        }

        //Check if the mouse is out of bound
        bool _outOfXBound = (_mouseX < m_leftBound.position.x) || (_mouseX > m_rightBound.position.x);
        bool _outOfZBound = (_mouseZ < m_lowerBound.position.z) || (_mouseZ > m_upBound.position.z);
        Vector3 _newPos = m_toiletSucker.transform.position;

        //Assign new coordinates
        if (!_outOfXBound) _newPos.x = _mouseX;
        if (!_outOfZBound) _newPos.z = _mouseZ + m_zOffset;
        m_toiletSucker.transform.position = new Vector3(_newPos.x, m_startYPos, _newPos.z);

        //Pressing mouse
        if (Input.GetMouseButtonDown(0))
        {
            m_toiletSuckerAnimator.SetTrigger("Press");
            m_toiletParticle.Clear();
            m_toiletParticle.Play();
        }
    }
}
