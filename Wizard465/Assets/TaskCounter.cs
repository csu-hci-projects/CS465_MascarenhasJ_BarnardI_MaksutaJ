using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Internal;

public class TaskCounter : MonoBehaviour
{
    private static readonly Vector3 DEFAULT_ROW_OFFSET = new Vector3(0f, 0.2f, 0f);
    private const float DEFAULT_ICON_SPACING = 0.015f;
    private const float DEFAULT_START_X_OFFSET = -0.05f;
    private const float DEFAULT_LABEL_SPACING = 0.02f;

    private const string DEFAULT_LABEL_FONT_NAME = "Arial";
    private const int DEFAULT_LABEL_FONT_SIZE = 24;
    private static readonly UnityEngine.Color DEFAULT_LABEL_COLOR = UnityEngine.Color.white;

    [Header("Icon Prefabs")]
    public GameObject successIconPrefab;
    public GameObject failureIconPrefab;

    [Header("UI Settings")]
    private Transform uiParent;
    public Vector3 rowOffset;
    public float iconSpacing;
    /// <summary>
    /// The starting X position for the icons in the row.
    /// </summary>
    public float startXOffset;
    /// <summary>
    /// The spacing between label and icons
    /// </summary>
    [System.ComponentModel.DefaultValue(DEFAULT_LABEL_SPACING)]
    public float labelSpacing;

    /// <summary>
    /// Settings for the text labels that will be displayed before the icons.
    /// </summary>
    [Header("Text Label Settings")]
    public TMP_FontAsset labelFont;
    /// <summary>
    /// The font size for the text labels.
    /// </summary>
    public int labelFontSize = 24;
    /// <summary>
    /// The color of the text labels.
    /// </summary>
    public UnityEngine.Color labelColor = UnityEngine.Color.white;

    private List<GameObject> displayedIcons;
    
    // Optional: For displaying "Success:" and "Failure:" text
    [Header("Text Labels (Optional)")]
    public GameObject successLabelPrefab;
    public GameObject failureLabelPrefab;
    private GameObject successLabelInstance;
    private GameObject failureLabelInstance;

    public Vector2 rowSpacing = new Vector2(0f, -30f); // Vertical spacing between rows


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDefaults();
    }

    private void SetDefaults()
    {
        if (this.rowOffset == null)
        {
            this.rowOffset = DEFAULT_ROW_OFFSET;
        }

        if (this.iconSpacing == 0f)
        {
            this.iconSpacing = DEFAULT_ICON_SPACING;
        }
        if (this.startXOffset == 0f)
        {
            this.startXOffset = DEFAULT_START_X_OFFSET;
        }
        if (this.labelSpacing == 0f)
        {
            this.labelSpacing = DEFAULT_LABEL_SPACING;
        }

        this.displayedIcons = new List<GameObject>();
        
        if (this.labelFontSize <= 0)
        {
            this.labelFontSize = DEFAULT_LABEL_FONT_SIZE;
        }
        if (this.labelColor == null || this.labelColor == UnityEngine.Color.clear)
        {
            this.labelColor = DEFAULT_LABEL_COLOR;
        }
        if (this.labelFont == null)
        {
            UnityEngine.Font systemFont = UnityEngine.Font.CreateDynamicFontFromOSFont(DEFAULT_LABEL_FONT_NAME, this.labelFontSize);
            this.labelFont = TMP_FontAsset.CreateFontAsset(systemFont);
        }
        
        if (this.uiParent == null)
        {
            this.uiParent = gameObject.transform;
            Debug.LogError("UI Parent is not set. Please assign a Transform to the UI Parent field.");
        }
        if (this.successIconPrefab == null || this.failureIconPrefab == null)
        {
            Debug.LogError("Success or Failure icon prefab is not set. Please assign prefabs to the respective fields.");
        }
        if (this.successLabelPrefab != null && this.successLabelPrefab.GetComponent<TextMeshProUGUI>() == null &&
            this.successLabelPrefab.GetComponent<UnityEngine.UI.Text>() == null)
        {
            Debug.LogError("Success label prefab must have a TextMeshProUGUI or UnityEngine.UI.Text component.");
        }
    }

    private GameObject CreateLabel(string name, string text)
    {
        GameObject goLabel = new GameObject(name);
        goLabel.transform.SetParent(uiParent, false);
        TextMeshProUGUI labelTMP = goLabel.AddComponent<TextMeshProUGUI>();
        labelTMP.text = text;
        if (labelFont != null) labelTMP.font = labelFont;
        labelTMP.fontSize = labelFontSize;
        labelTMP.color = labelColor;
        labelTMP.alignment = TextAlignmentOptions.Left;

        return goLabel;
    }

    private int GetSuccessCount()
    {
        int result = 0;
        Game theGame = Game.Instance;
        if (theGame != null && theGame.currentLevel != null)
        {
            result = theGame.currentLevel.SuccessCount;
        }
        return result;
    }

    private int GetFailureCount()
    {
        int result = 0;
        Game theGame = Game.Instance;
        if (theGame != null && theGame.currentLevel != null)
        {
            result = theGame.currentLevel.FailureCount;
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void SetGameObjectPosition(GameObject gameObject, Vector2 positionVector)
    {
        RectTransform childRect = gameObject.GetComponent<RectTransform>();
        if (childRect != null)
        {
            childRect.anchoredPosition = positionVector;
        }
        else
        {
            Debug.LogError(gameObject.name + " does not have a RectTransform.");
        }
    }

    private void UpdateDisplay()
    {
        // Clear existing icons and labels
        foreach (var icon in displayedIcons)
        {
            Destroy(icon);
        }
        displayedIcons.Clear();

        if (successLabelInstance != null) Destroy(successLabelInstance);
        if (failureLabelInstance != null) Destroy(failureLabelInstance);

        if (uiParent == null)
        {
            Debug.LogError("UI Parent (Canvas RectTransform) not assigned.");
            return;
        }

        GameObject successLabelGO = CreateLabel("SuccessLabel", "Success:");
        SetGameObjectPosition(successLabelGO, new Vector2((float)startXOffset, 0f));

        float currentX = (float)startXOffset + (float)labelSpacing;
        for (int i = 0; i < GetSuccessCount(); i++)
        {
            if (successIconPrefab != null)
            {
                GameObject iconInstance = Instantiate(successIconPrefab, uiParent);
                displayedIcons.Add(iconInstance);
                RectTransform iconRect = iconInstance.GetComponent<RectTransform>();
                if (iconRect != null)
                {
                    iconRect.anchoredPosition = new Vector2(currentX, 0f);
                    currentX += (float)iconSpacing;
                }
            }
            else
            {
                Debug.LogWarning("Success Icon Prefab is not assigned.");
            }
        }
        successLabelInstance = successLabelGO;

        GameObject failureLabelGO = CreateLabel("FailureLabel", "Failure:"); // Create the label for failures   
        SetGameObjectPosition(failureLabelGO, new Vector2((float)startXOffset, rowSpacing.y)); // Position it below the success label
        
        currentX = (float)startXOffset + (float)labelSpacing;
        for (int i = 0; i < GetFailureCount(); i++)
        {
            if (failureIconPrefab != null)
            {
                GameObject iconInstance = Instantiate(failureIconPrefab, uiParent);
                displayedIcons.Add(iconInstance);
                RectTransform iconRect = iconInstance.GetComponent<RectTransform>();
                if (iconRect != null)
                {
                    iconRect.anchoredPosition = new Vector2(currentX, rowSpacing.y);
                    currentX += (float)iconSpacing;
                }
            }
            else
            {
                Debug.LogWarning("Failure Icon Prefab is not assigned.");
            }
        }
        failureLabelInstance = failureLabelGO;
    }

}
