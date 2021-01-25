using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AppViewHitBuilder : HitBuilder<AppViewHitBuilder>
{
    private string screenName = "";

    public string GetScreenName()
    {
        return screenName;
    }

    public AppViewHitBuilder SetScreenName(string screenName)
    {
        if (screenName != null)
            this.screenName = screenName;

        return this;
    }

    public override AppViewHitBuilder GetThis()
    {
        return this;
    }

    public override AppViewHitBuilder Validate()
    {
        if (String.IsNullOrEmpty(screenName))
        {
            Debug.Log("No screen name provided - App View hit cannot be sent.");
            return null;
        }

        return this;
    }
}
