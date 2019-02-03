using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventHitBuilder : HitBuilder<EventHitBuilder>
{
    private string eventCategory = "";
    private string eventAction = "";
    private string eventLabel = "";
    private long eventValue;

    public string GetEventCategory()
    {
        return eventCategory;
    }

    public EventHitBuilder SetEventCategory(string eventCategory)
    {
        if (eventCategory != null)
            this.eventCategory = eventCategory;

        return this;
    }

    public string GetEventAction()
    {
        return eventAction;
    }

    public EventHitBuilder SetEventAction(string eventAction)
    {
        if (eventAction != null)
            this.eventAction = eventAction;

        return this;
    }

    public string GetEventLabel()
    {
        return eventLabel;
    }

    public EventHitBuilder SetEventLabel(string eventLabel)
    {
        if (eventLabel != null)
            this.eventLabel = eventLabel;

        return this;
    }

    public long GetEventValue()
    {
        return eventValue;
    }

    public EventHitBuilder SetEventValue(long eventValue)
    {
        this.eventValue = eventValue;
        return this;
    }

    public override EventHitBuilder GetThis()
    {
        return this;
    }

    public override EventHitBuilder Validate()
    {
        if (String.IsNullOrEmpty(eventCategory))
        {
            Debug.LogWarning("No event category provided - Event hit cannot be sent.");
            return null;
        }

        if (String.IsNullOrEmpty(eventAction))
        {
            Debug.LogWarning("No event action provided - Event hit cannot be sent.");
            return null;
        }

        return this;
    }
}
