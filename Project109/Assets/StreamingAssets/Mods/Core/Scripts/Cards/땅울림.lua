local card = {}
function card:OnInit(cardBase, owner)
    self.base = cardBase
end
function card:GetDescription(cardBase, template)
    local baseDamage = cardBase:GetBaseFormattedValue("damage")
    local vulnerable = cardBase:GetBaseFormattedValue("Vulnerable")
    local weak = cardBase:GetBaseFormattedValue("Weak")
    return (template:gsub("{damage}", tostring(baseDamage))
                    :gsub("{Vulnerable}", tostring(vulnerable))
                    :gsub("{Weak}", tostring(weak)))
end
function card:Execute(cardInfo)
    local caster = cardInfo.caster
    if cardInfo.targets ~= nil then
        local baseDamage = self.base:GetBaseValue("damage")
        local vulnerable = self.base:GetBaseValue("Vulnerable")
        local weak = self.base:GetBaseValue("Weak")
        for i = 0, cardInfo.targets.Count - 1 do
            local target = cardInfo.targets[i]
            target:TakeDamage(caster, baseDamage)
            target:TakeEffect(caster, "Vulnerable", vulnerable, 0)
            target:TakeEffect(caster, "Weakness", weak, 0)
        end
    end
end
return card