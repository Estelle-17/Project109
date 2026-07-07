local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
end
function card:GetDescription(cardBase, template)
    local baseDamage = cardBase:GetBaseFormattedValue("damage")
    local shield = cardBase:GetBaseFormattedValue("shield")
    return (template:gsub("{damage}", tostring(baseDamage)):gsub("{shield}", tostring(shield)))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    if cardInfo.targets ~= nil and cardInfo.targets.Count > 0 then
        local target = cardInfo.targets[0]
        local baseDamage = self.base:GetBaseValue("damage")
        local shield = self.base:GetBaseValue("shield")
        target:TakeDamage(caster, baseDamage)
        caster:TakeShield(caster, shield, 1)
    end
end
return card