using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : GameManager
{
    #region Public Attributs
    #endregion

    #region Private Attributs
    #endregion

    public override void EndFadeOutLoad()
    {
        switch (GameManager.Instance.nextScene)
        {
            case EnumScene.Menu:
                {
                }
                break;
            case EnumScene.Game:
                {
                }
                break;
        }
    }
}
