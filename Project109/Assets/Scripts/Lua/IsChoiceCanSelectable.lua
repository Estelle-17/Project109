function IsChoiceCanSelectable(item, Player, Character, Card, Relic)
    print('ItemType :', item.itemType)
    if item.itemType == "Money" then
        if Player.inGame_Currency > item.value then
            print(Player.inGame_Currency, ' > ', item.value)
            return true
        end
    elseif item.itemType == "MaxHp" then
        if Character.maxHp > item.value then
            return true
        end
    elseif item.itemType == "Hp" then
        if Character.hp > item.value then
            return true
        end
    elseif item.itemType == "MaxStamina" then
        if Character.maxStamina > item.value then
            return true
        end
    elseif item.itemType == "SpecificCard" then
        for i = 0, Card.Count do
            if Card[i].cardName == item.name then
                return true
            end
        end
    elseif item.itemType == "SpecificRelic" then
        for i = 0, Relic.Count do
            if Relic[i].relicName == item.name then
                return true
            end
        end  
    elseif item.itemType == "RandomCard" then
        print('CardCount :', Card.Count)
        if Card.Count ~= 0 then
            return true;
        end
    elseif item.itemType == "RandomRelic" then
        print('RelicCount :', Relic.Count)
        if Relic.Count ~= 0 then
            return true;
        end
    end
    return false
end