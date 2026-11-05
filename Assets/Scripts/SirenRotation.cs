using System.Collections;
using UnityEngine;

public class SirenRotation : MonoBehaviour
{
    private Light blueSpotLight;
    private Light bluePointLight;
    private Light redSpotLight;
    private Light redPointLight;

    // Colores definidos en formato Hexadecimal (RGBA)
    private Color redColor = new Color(1f, 0.247f, 0.247f);     // #FF3F3F
    private Color blueColor = new Color(0.086f, 0.31f, 0.984f); // #164FFB

    private void Start()
    {
        // Buscar y extraer los componentes Light por el nombre exacto del GameObject
        blueSpotLight = GetLightByName("Blue_Spot_Light");
        bluePointLight = GetLightByName("Blue_Point_Light");
        redSpotLight = GetLightByName("Red_Spot_Light");
        redPointLight = GetLightByName("Red_Point_Light");

        // Iniciar el bucle de alternancia
        StartCoroutine(ToggleLightsRoutine());
    }

    private Light GetLightByName(string objectName)
    {
        GameObject go = GameObject.Find(objectName);
        if (go != null)
        {
            Light lightComp = go.GetComponent<Light>();
            if (lightComp == null)
            {
                Debug.LogWarning($"El GameObject '{objectName}' se encontró, pero no tiene un componente Light asignado.");
            }
            return lightComp;
        }

        Debug.LogError($"No se pudo encontrar el GameObject llamado '{objectName}' en la escena.");
        return null;
    }

    private IEnumerator ToggleLightsRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while (true)
        {
            // Estado A: Grupo Azul pasa a Rojo | Grupo Rojo pasa a Azul
            SetGroupColor(blueSpotLight, bluePointLight, redColor);
            SetGroupColor(redSpotLight, redPointLight, blueColor);

            yield return waitTime;

            // Estado B: Grupo Azul pasa a Azul | Grupo Rojo pasa a Rojo
            SetGroupColor(blueSpotLight, bluePointLight, blueColor);
            SetGroupColor(redSpotLight, redPointLight, redColor);

            yield return waitTime;
        }
    }

    private void SetGroupColor(Light spot, Light point, Color targetColor)
    {
        if (spot != null) spot.color = targetColor;
        if (point != null) point.color = targetColor;
    }
}
