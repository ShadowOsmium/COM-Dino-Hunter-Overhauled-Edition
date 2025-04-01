using UnityEngine;

public class TUIUpdatePlayerTextureInfo
{
	public string id = string.Empty;

	public Texture player_texture;

	public TUIUpdatePlayerTextureInfo(string m_id, Texture m_player_texture)
	{
		id = m_id;
		player_texture = m_player_texture;
	}
}
