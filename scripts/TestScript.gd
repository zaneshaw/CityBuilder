extends Resource
class_name TestScript

@export var health: int
@export var sub_resource: Resource
@export var strings: PackedStringArray

func _init(p_health = 0, p_sub_resource = null, p_strings = []):
	health = p_health
	sub_resource = p_sub_resource
	strings = p_strings
