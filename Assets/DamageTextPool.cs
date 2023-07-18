using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance { get; private set; }

    public float minFontSize = 5;
    public float maxFontSize = 10;

    public GameObject damageTextPrefab;
    private List<GameObject> damageTextPool;

    void Awake()
    {
        // Singleton pattern to easily access this instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize the list
        damageTextPool = new List<GameObject>();

        // Prepopulate the pool
        for (int i = 0; i < 50; i++)
        {
            GameObject newObj = Instantiate(damageTextPrefab);
            newObj.SetActive(false);
            damageTextPool.Add(newObj);
        }
    }

    public GameObject Get()
    {
        // Find an inactive object
        foreach (var obj in damageTextPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects, create a new one
        GameObject newObj = Instantiate(damageTextPrefab);
        damageTextPool.Add(newObj);
        return newObj;
    }

    public void ShowDamage(float amount, Transform position, float delay = 0.1f)
    {
        // Get an object from the pool
        GameObject damageTextObject = Get();
        Vector3 offset = new Vector2(0, -1);
        damageTextObject.transform.position = position.position + offset;

        // Set the text
        TextMeshPro textMesh = damageTextObject.GetComponent<TextMeshPro>();

        textMesh.fontSize = Mathf.Lerp(minFontSize, maxFontSize, amount / 100);

        textMesh.text = amount.ToString();

        if (amount < 10)
            textMesh.color = Color.white;
        else if (amount < 50)
            textMesh.color = Color.yellow;
        else
            textMesh.color = Color.red;

        // Start the coroutine on this object
        StartCoroutine(DamageTextEffect(damageTextObject));
    }

    IEnumerator DamageTextEffect(GameObject damageTextObject)
    {
        float duration = 0.5f; // Duration of the effect in seconds
        float speed = 1f; // Speed of the upward movement
        float fadeSpeed = 1f / duration; // Speed of the fade-out effect

        // Get the TextMeshProUGUI component
        TextMeshPro textMesh = damageTextObject.GetComponent<TextMeshPro>();

        // Store the original color
        Color originalColor = textMesh.color;
        

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            // Move the text upward
            damageTextObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);

            // Fade out the text
            Color color = textMesh.color;
            color.a = Mathf.Lerp(originalColor.a, 0, t * fadeSpeed);
            textMesh.color = color;

            yield return null;
        }

        // Make sure the text is completely transparent at the end
        Color finalColor = textMesh.color;
        finalColor.a = 0;
        textMesh.color = finalColor;

        // Return the text to the pool
        DamageTextPool.Instance.ReturnToPool(damageTextObject);
        //textMesh.color = originalColor;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}

