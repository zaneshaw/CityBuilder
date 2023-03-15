extends Resource
class_name BuildingData

@export var layer: int
@export var source: int
@export var coords: Vector2i

func _init(p_layer = 0, p_source = 0, p_coords = Vector2i.ZERO):
	layer = p_layer
	source = p_source
	coords = p_coords
