local effect = {}

-- 1. 객체 생성 직후 호출되는 초기화 함수
function effect:OnInit(effectBase)
    self.base = effectBase
end

-- 2. 전투 중 공격 직전 발생하는 이벤트 (IOnBeforeDealDamage 인터페이스 대응)
-- C#의 ref DamageInfo 매개변수로 들어옵니다.
function effect:OnBeforeDealDamage(damageInfo)
    -- 현재 힘(Strength) 스택만큼 기본 데미지를 증가시킴
    damageInfo.baseDamageAmount = damageInfo.baseDamageAmount + self.base.currentStack
    
    -- 중요: XLua에서 C#의 struct (ref)를 수정할 때는 변경된 값을 return 해줘야 원본에 반영됩니다.
    return damageInfo
end

return effect
