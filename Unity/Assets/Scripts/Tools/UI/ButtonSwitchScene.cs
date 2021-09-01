using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitchScene : MonoBehaviour
{
    #region Public Attributs
    public EnumScene SceneType = EnumScene.Game;
    #endregion

    #region Private Attributs
    #endregion

    public void LaunchAction()
    {
        GameManager.Instance.SwitchScene(SceneType);
    }
}
