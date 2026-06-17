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
    caster:TakeShield(caster, shield, 1)
end
return card