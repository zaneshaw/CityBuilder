extends Resource
class_name BuildingData

@export var layer: int
@export var source: int
@export var coords: Vector2i

func _init(p_layer = null, p_source = null, p_coords = null):
	layer = p_layer
	source = p_source
	coords = p_coords
