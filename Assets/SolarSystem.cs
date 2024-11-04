using UnityEngine;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour
{
    public Transform sun;
    public List<PlanetData> planets = new List<PlanetData>();
    public Material[] planetMaterials;

    [System.Serializable]
    public class PlanetData
    {
        public string name;
        public float orbitRadius;
        public float orbitSpeed;
        public float rotationSpeed;
        public float size;
        [HideInInspector] public Transform transform;
    }

    void Start()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            GameObject planetObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planetObj.name = planets[i].name;
            planetObj.transform.localScale = Vector3.one * planets[i].size;
            planets[i].transform = planetObj.transform;
            
            // Apply material to the planet
            Renderer renderer = planetObj.GetComponent<Renderer>();
            if (i < planetMaterials.Length)
            {
                renderer.material = planetMaterials[i];
            }
        }
    }

    void Update()
    {
        foreach (var planet in planets)
        {
            // Calculate orbit position
            float angle = Time.time * planet.orbitSpeed;
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * planet.orbitRadius,
                0,
                Mathf.Sin(angle) * planet.orbitRadius
            );
            planet.transform.position = sun.position + position;

            // Rotate the planet
            planet.transform.Rotate(Vector3.up, planet.rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        if (sun == null) return;
        
        Gizmos.color = Color.yellow;
        foreach (var planet in planets)
        {
            Gizmos.DrawWireSphere(sun.position, planet.orbitRadius);
        }
    }
}