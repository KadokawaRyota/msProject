using UnityEngine;



public class TapEffect : MonoBehaviour
{

    [SerializeField]
    ParticleSystem tapEffect;              // タップエフェクト

    [SerializeField]
    Camera TargetCamera;                   // カメラの座標

    public bool EffectFlug = false;
    public Vector3 EffectPos;

    void Update()
    {
        // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        if (EffectFlug)
        {
            tapEffect.transform.position = EffectPos;
            Vector3 pos = TargetCamera.ScreenToWorldPoint(new Vector3(EffectPos.x, EffectPos.y, 1.0f));
            tapEffect.transform.position = pos;
            tapEffect.Emit(2);
          
        }
        EffectFlug = false;

    }

}