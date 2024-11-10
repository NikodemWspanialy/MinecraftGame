#version 330 core

in vec2 textCoord;

out vec4 FragColor;

uniform sampler2D texture0;

void main()
{
    vec4 texColor = texture(texture0, textCoord);
    FragColor = vec4(texColor.rgb, texColor.a * 0.5f);
}