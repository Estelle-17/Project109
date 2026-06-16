local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
end
function card:GetDescription(cardBase, template)
    local shield = cardBase:GetBaseValue("shield")
    return (template:gsub("{shield}", tostring(math.floor(shield))))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    local shield = self.base:GetBaseValue("shield")
    local shieldInfo = EventStructs.ShieldInfo(caster, caster, shield, EventStructs.ShieldFlag.Normal)
    caster:TakeShield(shieldInfo, 1)
end
return card