using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterManager character;
    // 인스턴트 이펙트(테이크 데미지, 힐)

    // 시간차 이펙트 (옥, 빌드 UPS)

    // 스테틱 이펙트 (장신구 버프 추가/제거 등)

    [Header("VFX")]
    [SerializeField] GameObject bloodSplatterVFX;


    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
        

    public virtual void ProcessInstantEffects(InstantCharacterEffect effect)
    {
        // 이펙트를 받기
        effect.ProcessEffect(character);
        // 처리 하기
    }

    public void PlayBloodSplatterVFX(Vector3 contactPoint)
    {
        // 만약 우리가 수동적으로 vfx 모델을 배치했을시, 해당 버전을 플레이.
        if (bloodSplatterVFX != null)
        {
            GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
        }
        // 그렇지 않으면, 디폴트 버전을 사용함.
        // 여러 적을 구현할때 반복적으로 인스팩터 사용해야하는 수고로움 방지용.
        else
        {
            GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.Instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
        }
    }
}
