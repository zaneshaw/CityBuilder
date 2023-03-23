extends Resource
class_name BuildingData

@export var source: int
@export var coords: Vector2i

func _init(p_source = 0, p_coords = Vector2i.ZERO):
	source = p_source
	coords = p_coords
