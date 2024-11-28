shader_type spatial;

uniform sampler2D existing_screen_texture: hint_screen_texture, filter_linear_mipmap;
uniform sampler2D textureExample;

varying vec3 world_position;

void vertex() {
	world_position = (NODE_POSITION_WORLD * VERTEX).xyz;
}

void fragment() {
	vec3 new_color = texture(existing_screen_texture, SCREEN_UV, 0.0).rgb;
	new_color = vec3(1.0) - new_color;
	vec3 textureEx = texture(textureExample, SCREEN_UV * 2.5, 0.0).rgb;
	ALBEDO = new_color * .5 + textureEx * 0.5;;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
