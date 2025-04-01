public class TUISingleIAPInfo
{
	public int id;

	public float money_cost;

	public TUIPriceInfo money_get;

	public int free_count;

	public string money_texture = string.Empty;

	public TUISingleIAPInfo(int m_id, float m_money_cost, int m_money_get_price, UnitType m_money_get_type, string m_money_texture, int m_free_count = 0)
	{
		id = m_id;
		money_cost = m_money_cost;
		if (money_get == null)
		{
			money_get = new TUIPriceInfo();
		}
		money_get.price = m_money_get_price;
		money_get.unit_type = m_money_get_type;
		free_count = m_free_count;
		money_texture = m_money_texture;
	}
}
