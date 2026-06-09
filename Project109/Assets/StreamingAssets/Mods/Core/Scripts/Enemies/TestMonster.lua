local enemy = {}

-- 1. 초기화
function enemy:OnInit(enemyBase)
    self.base = enemyBase
end

-- 2. AI 행동 패턴 또는 의도 결정 (Intent)
-- NPCUnitController 등에서 활용할 콜백 함수 정의
function enemy:EvaluateNextIntent(controller)
    print("[Lua Enemy] TestMonster의 의도를 평가합니다.")
end

return enemy
