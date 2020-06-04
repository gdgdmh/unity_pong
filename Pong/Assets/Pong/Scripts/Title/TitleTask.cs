using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTask : MonoBehaviour
{
    private Mhl.ISingleTouchActionable touchAction;

    /// <summary>
    /// シーン開始時の処理
    /// </summary>
    private void Start()
    {
        touchAction = new Mhl.SingleTouchActionEditor();        
    }

    /// <summary>
    /// シーンの更新処理
    /// </summary>
    private void Update()
    {
        // タッチの更新
        UpdateTouch();
        // シーン移動するかのチェック
        if (CheckMoveNextScene())
        {
            // メインメニューに移動
            ChangeMainmenuScene();
        }
    }

    /// <summary>
    /// タッチ情報の更新
    /// </summary>
    private void UpdateTouch()
    {
        UnityEngine.Assertions.Assert.IsNotNull(touchAction);
        touchAction.Update();
    }

    /// <summary>
    /// 次のシーンに移動するかチェック
    /// </summary>
    /// <returns>trueなら次のシーンに移動</returns>
    private bool CheckMoveNextScene()
    {
        UnityEngine.Assertions.Assert.IsNotNull(touchAction);
        if (touchAction.IsTouchEnded())
        {
            // 画面のどこかをタッチした
            return true;
        }
        return false;
    }

    private void ChangeMainmenuScene()
    {
        // メインメニューに移動
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
