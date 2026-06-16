local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
end
function card:GetDescription(cardBase, template)
    local baseDamage = cardBase:GetBaseValue("damage")
    local hits = cardBase:GetBaseValue("hits")
    return (template:gsub("{damage}", tostring(math.floor(baseDamage))):gsub("{hits}", tostring(math.floor(hits))))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    if cardInfo.targets ~= nil and cardInfo.targets.Count > 0 then
        local target = cardInfo.targets[0]
        local baseDamage = self.base:GetBaseValue("damage")
        local hits = self.base:GetBaseValue("hits")
        for i = 1, hits do
            local dmgInfo = EventStructs.DamageInfo(caster, target, baseDamage, EventStructs.DamageFlag.Normal)
            target:TakeDamage(dmgInfo)
        end
    end
end
return card