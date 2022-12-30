using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    private Button button;
    private EventTrigger eventTrigger;
    public Image targetGraphic;
    public Boolean startButton = false;
    public EventTrigger.Entry onClick;

    private void Awake()
    {
        button = gameObject.AddComponent<Button>();

        button.transition = Selectable.Transition.ColorTint;
        button.targetGraphic = this.targetGraphic;

        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.white;
        colorBlock.highlightedColor = new Color(0.9f, 0.9f, 0.9f);
        colorBlock.pressedColor = new Color(0.8f, 0.8f, 0.8f);
        colorBlock.selectedColor = Color.white;
        colorBlock.disabledColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        colorBlock.fadeDuration = 0.1f;
        button.colors = colorBlock;

        eventTrigger = gameObject.AddComponent<EventTrigger>();
        eventTrigger.triggers.Add(onClick);

        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener(data => StartMenuManager.hoverSound());
        eventTrigger.triggers.Add(hoverEntry);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener(data => StartMenuManager.clickSound(startButton));
        eventTrigger.triggers.Add(clickEntry);
    }

}
