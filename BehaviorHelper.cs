namespace Showdown3;

public class BehaviorHelper
{
    public static void EnterPhotomode()
    {
        PlayerManager.Instance.currentMaster.flyingCamera.ToggleFlyingCamera();
    }
}