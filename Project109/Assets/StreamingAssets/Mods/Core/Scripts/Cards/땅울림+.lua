local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
end
function card:GetDescription(cardBase, template)
    local baseDamage = cardBase:GetBaseValue("damage")
    local vulnerable = cardBase:GetBaseValue("vulnerable")
    local weak = cardBase:GetBaseValue("weak")
    return (template:gsub("{damage}", tostring(math.floor(baseDamage)))
                    :gsub("{vulnerable}", tostring(math.floor(vulnerable)))
                    :gsub("{weak}", tostring(math.floor(weak))))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    if cardInfo.targets ~= nil then
        local baseDamage = self.base:GetBaseValue("damage")
        local vulnerable = self.base:GetBaseValue("vulnerable")
        local weak = self.base:GetBaseValue("weak")
        for i = 0, cardInfo.targets.Count - 1 do
            local target = cardInfo.targets[i]
            local dmgInfo = EventStructs.DamageInfo(caster, target, baseDamage, EventStructs.DamageFlag.Normal)
            target:TakeDamage(dmgInfo)
            target:ApplyEffect(caster, "Vulnerable", vulnerable, 0)
            target:ApplyEffect(caster, "Weakness", weak, 0)
        end
    end
end
return card