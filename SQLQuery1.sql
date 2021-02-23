Select Pet.Name, PetType.PetTypeName From PetType
inner Join Pet ON Pet.TypeId = PetType.Id
Where Pet.TypeId = 2