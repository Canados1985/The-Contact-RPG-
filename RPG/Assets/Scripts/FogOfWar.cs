using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

    public GameObject go_fogOfWarPlane;
    public Transform playerTransform;
    public LayerMask m_fogLayer;
    public float f_radius;
    private float f_radiusSqr { get { return f_radius * f_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;

	void Start () {
        Initialize();	
	}
	

	void Update () {
        Ray r = new Ray(transform.position, playerTransform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < m_vertices.Length; i++)
            {
                Vector3 v = go_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < f_radiusSqr)
                {
                    float alpha = Mathf.Min(m_colors[i].a, dist / f_radiusSqr);
                    m_colors[i].a = alpha;
                }
            }

            UpdateColor();
        }
	}

    void Initialize()
    {
        m_mesh = go_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
    }

}
