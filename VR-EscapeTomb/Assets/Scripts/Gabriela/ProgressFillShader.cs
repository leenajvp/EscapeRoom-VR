using UnityEngine;

public class ProgressFillShader : MonoBehaviour
{
    public static ProgressFillShader Instance;

    Material objectMaterial;

    [SerializeField] float progressBar = 1;
    float fillRateValue;
    float progress;
    float progressStep = 0.2f;


    void Start()
    {
        Instance = this;
    }

    public void ChangeValue()
    {
        objectMaterial = GetComponent<Renderer>().material;

        float progressBar = GetComponent<MeshFilter>().mesh.bounds.size.x / 2f;
        objectMaterial.SetFloat("Progress Bar", progressBar);

        fillRateValue = -progress;
        objectMaterial.SetFloat("Fill", -progressBar);


        fillRateValue += progressStep;

        objectMaterial.SetFloat("Fill Rate", fillRateValue);

    }

    /*
     if (fillPod == true)
                                    {
                                        float startTime = Time.time;

                                        float t = (Time.time - startTime) * teleportGap;
                                        Color currentColor = Color.Lerp(startColor, endColor, t);
                                        selectionRender.material.color = currentColor;
                                    }
    */
}
