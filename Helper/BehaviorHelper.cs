﻿namespace Showdown3.Helper;

public class BehaviorHelper
{
    public static void EnterPhotomode()
    {
        if (!PlayerManager.Instance.currentMaster.flyingCamera.isPhotoMode)
            PlayerManager.Instance.currentMaster.flyingCamera.ToggleFlyingCamera();
    }
}