using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    [SerializeField] private List<PanelBase> panels = new();

    private Dictionary<string, PanelBase> panelLookup = new();

    private void Awake()
    {
        Instance = this;

        // Auto-build lookup table using GameObject names
        foreach (var panel in panels)
        {
            if (panel != null)
            {
                string key = panel.gameObject.name.ToLowerInvariant();
                if (!panelLookup.ContainsKey(key))
                    panelLookup.Add(key, panel);
                else
                    Debug.LogWarning($"Duplicate panel name: {key}");
            }
        }
    }

    public void OpenPanel(string panelName)
    {
        string key = panelName.ToLowerInvariant();
        if (panelLookup.TryGetValue(key, out var panel))
        {
            OpenPanel(panel);
        }
        else
        {
            Debug.LogWarning($"No panel found with name '{panelName}'");
        }
    }

    public void OpenPanel(int index)
    {
        if (index >= 0 && index < panels.Count)
        {
            OpenPanel(panels[index]);
        }
        else
        {
            Debug.LogWarning($"No panel found at index {index}");
        }
    }

    public void OpenPanel(PanelBase panelToOpen)
    {
        foreach (var panel in panels)
        {
            if (panel == panelToOpen)
            {
                panel.ShowPanel(true);
                Debug.Log("nav manager calls it");
                panel.RefreshPanel();
            }
            else
            {
                panel.ShowPanel(false);
            }
        }
    }
}
