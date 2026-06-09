local card = {}

-- 1. 카드 초기화
function card:OnInit(cardBase, owner)
    self.base = cardBase
end

-- 2. 카드 설명 텍스트 동적 치환
function card:GetDescription(cardBase, template)
    -- 기본 데미지 수치와 마스터리 보너스 계산
    local baseDamage = cardBase:GetBaseValue("damage")
    -- 마스터리 업그레이드 단계를 C#에서 쿼리 (예: power_up 마스터리 레벨)
    local powerUpLevel = cardBase:GetMasteryLevel("power_up")
    local extraDamage = powerUpLevel * 3 -- 1업당 3 추가
    local totalDamage = baseDamage + extraDamage

    return (template:gsub("{damage}", tostring(math.floor(totalDamage))))
end

-- 3. 카드 실행 효과
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    local target = cardInfo.target
    
    if target ~= nil then
        local baseDamage = self.base:GetBaseValue("damage")
        local powerUpLevel = self.base:GetMasteryLevel("power_up")
        local totalDamage = baseDamage + (powerUpLevel * 3)

        -- Note: 실제 데미지 적용은 C#의 DamageInfo를 연동하여 처리
        -- local dmgInfo = CS.DamageInfo(caster, target, totalDamage, CS.DamageFlag.Normal)
        -- target:TakeDamage(dmgInfo)
        
        print("[Lua Strike] Execute! Target: " .. target.name .. ", Damage: " .. totalDamage)
    end
end

-- 4. 시전 가능 조건 추가 검증 (필요 시)
function card:CanPlay(cardBase, caster)
    return true
end

return card
