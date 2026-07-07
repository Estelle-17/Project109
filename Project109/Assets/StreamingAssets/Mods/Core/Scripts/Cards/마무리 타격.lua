local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
    self.cardsPlayedThisTurn = 0
end
function card:OnTurnStart()
    self.cardsPlayedThisTurn = 0
end
function card:OnAfterUseCard(cardInfo)
    if cardInfo.caster == self.base.owner then
        self.cardsPlayedThisTurn = (self.cardsPlayedThisTurn or 0) + 1
    end
end
function card:GetDescription(cardBase, template)
    local baseDamage = cardBase:GetBaseFormattedValue("damage")
    local bonus = cardBase:GetBaseFormattedValue("bonus")
    return (template:gsub("{damage}", tostring(baseDamage)):gsub("{bonus}", tostring(bonus)))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    if cardInfo.targets ~= nil and cardInfo.targets.Count > 0 then
        local target = cardInfo.targets[0]
        local baseDamage = self.base:GetBaseValue("damage")
        local bonus = self.base:GetBaseValue("bonus")
        local playedCount = self.cardsPlayedThisTurn or 0
        local totalDamage = baseDamage + (playedCount * bonus)
        local dmgInfo = EventStructs.DamageInfo(caster, target, totalDamage, EventStructs.DamageFlag.Normal)
        target:TakeDamage(dmgInfo)
    end
end
return card