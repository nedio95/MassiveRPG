using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public List<PanelBase> panels; // assign all panels here in Inspector

    public void OpenPanel(PanelBase panelToOpen)
    {
        foreach (var panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        panelToOpen.gameObject.SetActive(true);
        panelToOpen.Refresh();
    }
}
