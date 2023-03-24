extends Resource
class_name BuildingData

@export var source: int
@export var coords: Vector2i
@export var duration: float

func _init(p_source = 0, p_coords = Vector2i.ZERO, p_duration = 1):
	source = p_source
	coords = p_coords
	duration = p_duration;
