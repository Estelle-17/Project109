-- Preserve.lua
local Preserve = DefineCardTag("Preserve")

-- 태그가 카드에 부착될 때 호출
function Preserve:OnInit(tagBase, card)
    self.base = tagBase      -- C# CardTag 인스턴스
    self.card = card         -- C# Card 인스턴스
end

-- 캐릭터가 카드를 버릴 때 호출되는 이벤트 (IOnDiscardCard 인터페이스 대응)
function Preserve:OnDiscardCard(info)
    -- info는 EventStructs.CardInfo 타입
    if info.card == self.card then
        -- 카드 플래그에 버림 방지(NoDiscard) 추가
        info:AddFlag(EventStructs.CardFlag.NoDiscard)
    end
end

-- 태그가 해제될 때 호출
function Preserve:OnRemoved()
    -- 리소스 해제 등의 정리 작업
end

return Preserve